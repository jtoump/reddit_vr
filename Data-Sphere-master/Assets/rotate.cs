using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {


    private Quaternion from;
    private Quaternion to;
    private float speed = 0.1f;
    public Transform transf;
    
	// Use this for initialization
	void Start () {


       

       
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x,Mathf.PingPong(Time.time*0.01f,2), transform.position.z);
        transform.Rotate(0, 1* Time.deltaTime,0); //rotates 50 degrees per second around z axis
    }
}
