using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursosFollow : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    //public GameObject Player;
    public float zposition=-5f;
    public bool spelling=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (spelling)
        {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = zposition;
            transform.position = mouseWorldPosition;
        }
       
    }
}
