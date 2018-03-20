using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelect : Interaction
{
    public override void Deselect()
    {
        ActionsManager.instance.ClearButtons();
    }

    public override void Select()
    {
        ActionsManager.instance.ClearButtons();
        foreach (var ab in GetComponents<ActionBehavior>())
        {
            ActionsManager.instance.AddButton(
                ab.ButtonPic,
                ab.GetClickAction());
        }
    }
}
