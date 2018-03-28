using System.Collections.Generic;
using UnityEngine;

public class TeamSetupDefinition {

    public string Name;

    public Transform Location;

    public List<GameObject> StartingUnits = new List<GameObject>();

    private List<GameObject> activeUnits = new List<GameObject>();

    public List<GameObject> ActiveUnits { get { return activeUnits; } }

}
