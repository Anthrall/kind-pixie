using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    private DataHolder Holder;
    public Text GameScore, Result;
    private bool IsCoroutineExecuting;

    // Start is called before the first frame update
    void Start()
    {
        GameScore.text = "";
        Result.text = "";
        StartCoroutine("WaitASecond");
    }

    IEnumerator WaitASecond()
    {
        if (IsCoroutineExecuting)
            yield break;
        IsCoroutineExecuting = true;
        yield return new WaitForSeconds(1.0f);

        Holder = FindObjectOfType<DataHolder>().GetComponent<DataHolder>();

        string textscore = "You have\n ";
        if (Holder.good_end != 0)
        {
            textscore += Holder.good_end + " Good things\n";
        }
        if (Holder.bad_end != 0)
        {
            textscore += Holder.bad_end + " Bad things";
        }
        GameScore.text = textscore;

        string ResultIs = "";
        if (Holder.good_end == Holder.characters)
        {
            ResultIs = "You turned out to be a very kind pixie :)";
            FindObjectOfType<AudioManagerForEndScene>().Play("GoodEnd");
        }
        if (Holder.bad_end == Holder.characters)
        {
            ResultIs = "You were no better than your sister. Typical pixie...";
            FindObjectOfType<AudioManagerForEndScene>().Play("BadEnd");
        }
        if (Holder.good_end != 0 && Holder.good_end < Holder.bad_end)
        {
            ResultIs = "Well, you are making progress in becoming a good pixie.";
            FindObjectOfType<AudioManagerForEndScene>().Play("Neutral");
        }
        if (Holder.bad_end != 0 && Holder.bad_end < Holder.good_end)
        {
            ResultIs = "A little more was not enough to absolute kindness. Come on again.";
            FindObjectOfType<AudioManagerForEndScene>().Play("Neutral");
        }
        if (Holder.bad_end == 0 && Holder.good_end == 0)
        {
            ResultIs = "How the hell did you get here?";

        }
        if (Holder.bad_end == Holder.good_end)
        {
            ResultIs = "You're a fan of balance, I see. OK";
        }
        Result.text = ResultIs;

        IsCoroutineExecuting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            Holder.bad_end = 0;
            Holder.good_end = 0;
            Holder.characters = 0;
            //FindObjectOfType<AudioManager>().Play("Theme");
            //FindObjectOfType<AudioManager>().Play("Effect");
            /*GameObject ch = GameObject.FindGameObjectWithTag("Character");
            for(int i = 0; i < ch.transform.childCount; i++)
            {
                var child = ch.transform.GetChild(i);
                string idle = child.name + "_idle";
                var anim = child.GetComponent<Animator>();
                
                anim.SetBool("fear",false);
                anim.SetBool("happy", false);
                anim.SetBool("human", false);
                anim.SetBool("plant", false);
                anim.SetBool("weather", false);
                anim.Play(idle);
                child.GetComponent<charactersetting>().isResult = false;
                child.GetComponent<charactersetting>().result = ResultSpell.Neutral;

            }*/
              
            SceneManager.LoadScene(1);
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Holder.bad_end = 0;
            Holder.good_end = 0;
            Holder.characters = 0;
            Application.Quit();
        }
    }
}
