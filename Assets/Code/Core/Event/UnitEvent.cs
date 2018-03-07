using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitEvent<TEventSystem> : BaseEventData {
    public UnitEvent(EventSystem eventSystem) : base(eventSystem)
    {
    }

    public override bool used
    {
        get
        {
            return base.used;
        }
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void Use()
    {
        base.Use();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
