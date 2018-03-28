using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalTrigger : MonoBehaviour {

    [SerializeField]
    private ExperimentalSO expSO;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShoutOut();
        }
	}
    public void ShoutOut()
    {
        Debug.Log("ExperimentalSO : ShoutOut()" + expSO.DeveloperDescription);
    }

}
