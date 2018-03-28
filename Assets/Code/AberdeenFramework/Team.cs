using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {

    public TeamSetupDefinition Info;

    public static TeamSetupDefinition Default;

    void Start()
    {
        Info.ActiveUnits.Add(this.gameObject);
    }

    void OnDestroy()
    {
        Info.ActiveUnits.Remove(this.gameObject);
    }
}
