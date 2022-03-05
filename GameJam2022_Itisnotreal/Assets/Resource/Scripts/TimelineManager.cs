using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public bool fix = false;
    public bool endgame = false;
    public Animator playerAnim;
    public RuntimeAnimatorController playerContr;
    public PlayableDirector director;
    private GameObject[] backgrounds;

    // Start is called before the first frame update
    void OnEnable()
    {
        playerContr = playerAnim.runtimeAnimatorController;
        playerAnim.runtimeAnimatorController = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(director.state != PlayState.Playing && !fix)
        {
            fix = true;
            if (!endgame)
            {
                FindObjectOfType<player_movement>().IsStartPlaying = true;
                playerAnim.runtimeAnimatorController = playerContr;
                FindObjectOfType<AudioManager>().CutsceneIsEnd = true;

                backgrounds = GameObject.FindGameObjectsWithTag("Parallax");
                foreach(GameObject b in backgrounds)
                {
                    var parallax = b.GetComponent<Parallax>();
                    parallax.CutsceneIsEnd = true;
                }
                //this.gameObject.SetActive(false);
            }
        }
    }
}
