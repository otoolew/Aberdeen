using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBrain : MonoBehaviour {
    public string Name { get; set; }    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ShoutOut()
    {
        Debug.Log("OUCH!");
    }

}
