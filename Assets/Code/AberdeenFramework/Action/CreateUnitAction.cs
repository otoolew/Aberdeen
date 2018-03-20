using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateUnitAction : ActionBehavior
{
    public GameObject Prefab;

    public override Action GetClickAction()
    {
        return delegate () {

            var go = (GameObject)GameObject.Instantiate(
                         Prefab,
                         transform.position,
                         Quaternion.identity);
            //go.AddComponent<Player>().Info = player;
            //go.AddComponent<RightClickNavigation>();
            //go.AddComponent<ActionSelect>();
            //player.Credits -= Cost;
        };
    }
}
