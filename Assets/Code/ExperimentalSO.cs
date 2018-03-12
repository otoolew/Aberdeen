using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class ExperimentalSO : ScriptableObject {
    public int ID;
    public int HP;

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public UnityEvent particleEvent;


    public void ShoutOut(ExperimentalTrigger et)
    {
        et.ShoutOut();   
    }
    public void EventFire()
    {
        particleEvent.Invoke();
    }
    
}
