using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_movement : MonoBehaviour
{
    public Animator anim;
    public float speedX = 12f;
    public float speedY = 5f;
    Transform tf;
    public bool IsStartPlaying, IsOutside=true;
    

    [SerializeField]
    float leftLimit, rightLimit, bottomLimit, upperLimit;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsStartPlaying)
        {
            if (Input.GetKey(KeyCode.D))
            {
                //transform.Rotate(new Vector3(0, 180, 0));
                tf.localRotation = Quaternion.Euler(0, 180, 0);
                var move = Time.deltaTime * speedX;
                tf.position -= tf.right * move;
                anim.SetFloat("speed", Mathf.Abs(move));
            }
            if (Input.GetKey(KeyCode.A))
            {
                tf.localRotation = Quaternion.Euler(0, 0, 0);
                var move = Time.deltaTime * speedX;
                tf.position -= tf.right * move;
                anim.SetFloat("speed", Mathf.Abs(move));
                //transform.Rotate(new Vector3(0,0,0));
            }
            if (Input.GetKey(KeyCode.W))
            {
                var move = speedY * Time.deltaTime;
                tf.position += new Vector3(0, move, 0);
                anim.SetFloat("speed", Mathf.Abs(move * 10));
            }
            if (Input.GetKey(KeyCode.S))
            {
                var move = speedY * Time.deltaTime;
                tf.position -= new Vector3(0, move, 0);
                anim.SetFloat("speed", Mathf.Abs(move * 10));
            }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                anim.SetFloat("speed", 0);
            }

            if (Input.GetKey(KeyCode.Escape))
            {
               
                Application.Quit();
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                                             Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
                                             transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.E))
        {
        //    anim.SetBool("isspelling", true);

        }
    }

    public void EndSpelling()
    {
        anim.SetBool("isspelling", false);
    }
}
