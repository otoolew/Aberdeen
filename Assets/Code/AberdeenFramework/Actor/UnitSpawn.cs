using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour {

    public GameObject enemyPrefab;

    private void Start()
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
