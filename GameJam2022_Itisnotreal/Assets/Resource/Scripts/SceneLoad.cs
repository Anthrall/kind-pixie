using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndGame()
    { 
        FindObjectOfType<TimelineManager>().endgame = true;
        FindObjectOfType<AudioManager>().StopEveryone();
        
        SceneManager.LoadScene(2);
    }
}
