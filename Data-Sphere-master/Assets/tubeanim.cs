﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tubeanim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.Rotate(0, 1 * Time.deltaTime, 0);
    }
}
