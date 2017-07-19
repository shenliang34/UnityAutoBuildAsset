using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    
    [Header("header")]
    public bool isTest;
    [Range(0,100)]
    public float range = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(isTest);
        Debug.Log(range);
	}
}
