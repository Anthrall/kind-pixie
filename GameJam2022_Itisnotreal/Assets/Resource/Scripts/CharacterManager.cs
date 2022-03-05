using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public GameObject Characters;
    public bool ChildsActive = true;
    // Start is called before the first frame update
    void Start()
    {
        Characters = GameObject.FindGameObjectWithTag("Character");
        //Characters.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        //Active();
    }

    

    private void Awake()
    {
        /*   if (instance == null)
           {
               instance = this;
               //Active();
           }
           else
           {
               Destroy(gameObject);
               return;
           }

           DontDestroyOnLoad(transform.gameObject);*/
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            string idle = child.name + "_idle";
            var anim = child.GetComponent<Animator>();

            anim.SetBool("fear", false);
            anim.SetBool("happy", false);
            anim.SetBool("human", false);
            anim.SetBool("plant", false);
            anim.SetBool("weather", false);
            anim.Play(idle);
            child.GetComponent<charactersetting>().isResult = false;
            child.GetComponent<charactersetting>().result = ResultSpell.Neutral;

        }
    }
}
