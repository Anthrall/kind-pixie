using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class SpellsManager : MonoBehaviour
{
    public static SpellsManager instance;
    public Illusions[] illu;
    public GameObject[] Characters;
    public int charascterSpelling = 0;
    public int bad_end = 0, good_end = 0;
    public GameObject fSpell, sSpell, cursor, player;
    private Camera mainCamera;
    public int how=1;
    public bool readyToSpell, Spelling;
    private bool IsCoroutineExecuting;

    public GameObject timeline, SceneManage;
    

    //var playscene = timeline.GetComponent<PlayableDirector>();
    //playscene.Play(end);

    private void Start()
    {
        cursor.GetComponent<CursosFollow>().spelling = false;
        cursor.transform.GetChild(0).gameObject.SetActive(false);
        
        mainCamera = Camera.main;
        
    }

    public void EndThatDay()
    {
        FindObjectOfType<player_movement>().IsStartPlaying = false;
        timeline.GetComponent<TimelineManager>().endgame = true;
        FindObjectOfType<CharacterManager>().ChildsActive = false;
        FindObjectOfType<DataHolder>().bad_end = bad_end;
        FindObjectOfType<DataHolder>().good_end = good_end;
        FindObjectOfType<DataHolder>().characters = Characters.Length;

        SceneManage.GetComponent<SceneLoad>().EndGame();
        
    }

    private void Update()
    {
        if (fSpell != null && sSpell != null)
        {
            readyToSpell = true;
            cursor.transform.GetChild(0).gameObject.SetActive(true);
            cursor.GetComponent<CursosFollow>().spelling = true;
            fSpell.transform.GetChild(0).GetComponent<SpellItemManager>().isSpellRigth = true;
            fSpell.transform.GetChild(0).GetComponent<SpellItemManager>().isChoosing = false;
            fSpell.transform.GetChild(0).GetComponent<SpellItemManager>().SpellReady();
            sSpell.transform.GetChild(0).GetComponent<SpellItemManager>().isSpellRigth = true;
            sSpell.transform.GetChild(0).GetComponent<SpellItemManager>().isChoosing = false;
            sSpell.transform.GetChild(0).GetComponent<SpellItemManager>().SpellReady();
            if(!Spelling)
                FindObjectOfType<AudioManager>().Play("SpellIsReady");
            Spelling = true;

        }
        else
        {
            readyToSpell = false;
            //cursor.GetComponent<CursosFollow>().spelling = false;
            //cursor.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && readyToSpell)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null)
            {
                foreach (GameObject ch in Characters)
                {
                    if (ch == hit.collider.gameObject)
                    {
                        player.GetComponent<Animator>().SetBool("isspelling", true);
                        //Debug.Log("Spelling to " + hit.collider.name);

                        CastSpell(fSpell.transform.GetChild(0).GetComponent<SpellItemManager>().spell.SpellName,
                                  sSpell.transform.GetChild(0).GetComponent<SpellItemManager>().spell.SpellName,
                                  hit.collider.gameObject);
                        break;
                    }
                }
            }
            else
            {
                CancelSpell();
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            cursor = GameObject.FindGameObjectWithTag("Cursor");
        }
    }

    private void FixedUpdate()
    {
        if((bad_end+good_end) == Characters.Length)
        {
            StartCoroutine("WaittoEnd");
        }
    }

    IEnumerator WaittoEnd()
    {
        if (IsCoroutineExecuting)
            yield break;
        IsCoroutineExecuting = true;
        yield return new WaitForSeconds(3.0f);

        EndThatDay();

        IsCoroutineExecuting = false;
    }

    public void CancelSpell()
    {
        
        fSpell.transform.GetChild(0).GetComponent<SpellItemManager>().isSpellRigth = false;
        fSpell.transform.GetChild(0).GetComponent<SpellItemManager>().EndSpell();
        fSpell = null;
        sSpell.transform.GetChild(0).GetComponent<SpellItemManager>().isSpellRigth = false;
        sSpell.transform.GetChild(0).GetComponent<SpellItemManager>().EndSpell();
        sSpell = null;
        readyToSpell = false;
        cursor.GetComponent<CursosFollow>().spelling = false;
        cursor.transform.GetChild(0).gameObject.SetActive(false);
        
        Spelling = false;
    }

    public void CastSpell(SpellsNames fSpell, SpellsNames sSpell, GameObject character)
    {
        foreach (Illusions l in illu)
        {
            //Debug.Log("Choosing spells: " + fSpell.ToString() + " and " + sSpell.ToString());
            if((fSpell == l.FirstSpell.SpellName && sSpell == l.SecondSpell.SpellName) || (sSpell == l.FirstSpell.SpellName && fSpell == l.SecondSpell.SpellName))
            {
                //Debug.Log("Result spell is " + l.name +" : " + l.Description);
                foreach (Character ch in l.characters)
                {
                    if(ch.name == character.GetComponent<charactersetting>().CharacterName)
                    {
                        if (!character.GetComponent<charactersetting>().isResult)
                        {
                            //Debug.Log("For " + ch.name + " result is " + ch.result);
                            if (ch.result == ResultSpell.Bad || ch.result == ResultSpell.Good)
                            {
                                string fs = fSpell.ToString().ToLower();
                                string ss = sSpell.ToString().ToLower();
                                character.GetComponent<Animator>().SetBool(fs, true);
                                character.GetComponent<Animator>().SetBool(ss, true);
                                character.GetComponent<charactersetting>().isResult = true;
                                character.GetComponent<charactersetting>().result = ch.result;

                                if(ch.result == ResultSpell.Bad)
                                {
                                    FindObjectOfType<AudioManager>().Play("BadEnd");
                                    bad_end += 1;
                                }
                                else
                                {
                                    if(ch.result == ResultSpell.Good)
                                    {
                                        FindObjectOfType<AudioManager>().Play("GoodEnd");
                                        good_end += 1;
                                    }
                                }
                            }
                            else
                                if(ch.result == ResultSpell.Neutral)
                            {
                                character.GetComponent<Animator>().SetBool("neutral", true);
                            }
                        }
                        break;
                    }
                    
                }
                break;
            }
        }
        CancelSpell();
    }

    public void ChooseSpells(GameObject spell, int how)
    {//spell.transform.GetChild(0).GetComponent<SpellItemManager>().isChoosing = false;
        if(how==1)
        {
            fSpell = spell;
        }
        if(how==2)
        {
            sSpell = spell;
            
        }
        
    }

    private void Awake()
    {
       /* if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(transform.gameObject);*/
    }
}
