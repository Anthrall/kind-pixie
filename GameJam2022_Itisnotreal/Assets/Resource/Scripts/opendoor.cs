using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : MonoBehaviour
{

    public bool PlayerWantsEnter, OnOutside;
    public float insidezone_z;
    public GameObject Zone_inside, Player, Camera, Cursos;

    public GameObject[] colliders_inside, colliders_outside;
    

    private void Start()
    {
        insidezone_z = Zone_inside.transform.localPosition.z-2;

        foreach (GameObject c in colliders_inside)
        {
            c.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && PlayerWantsEnter)
        {
            if (!OnOutside)
            {
                Player.gameObject.transform.position = new Vector3(Player.gameObject.transform.position.x,
                                                                   Player.gameObject.transform.position.y,
                                                                   -25f);
                Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, -35f);
                Cursos.GetComponent<CursosFollow>().zposition = -30f;
                OnOutside = true;
                
                    Player.GetComponent<player_movement>().IsOutside = false;

                foreach(GameObject c in colliders_inside)
                {
                    c.GetComponent<BoxCollider2D>().enabled = true;
                }
                foreach (GameObject s in colliders_outside)
                {
                    s.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else
            {
                Player.gameObject.transform.position = new Vector3(Player.gameObject.transform.position.x,
                                                                   Player.gameObject.transform.position.y,
                                                                   0);
                Camera.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, -10f);
                Cursos.GetComponent<CursosFollow>().zposition = -5f;
                OnOutside = false;
                
                    Player.GetComponent<player_movement>().IsOutside = true;
                

                foreach (GameObject c in colliders_inside)
                {
                    c.GetComponent<BoxCollider2D>().enabled = false;
                }
                foreach (GameObject s in colliders_outside)
                {
                    s.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerWantsEnter = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerWantsEnter = false;
        
    }

    /*private void OnTriggerEnter2D(Collider other)
    {
        
    }

    private void OnTriggerExit2D(Collider other)
    {
        
    }*/


}
