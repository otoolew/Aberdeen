using Core.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager> {

    public Collider MapCollider;
    public List<TeamSetupDefinition> Teams = new List<TeamSetupDefinition>();
    public Vector3? ScreenPointToMapPosition(Vector2 point)
    {
        var ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hit;
        if (!MapCollider.Raycast(ray, out hit, Mathf.Infinity))
            return null;

        return hit.point;
    }

    public bool IsGameObjectSafeToPlace(GameObject go)
    {
        var verts = go.GetComponentInChildren<MeshFilter>().mesh.vertices;

        var obstacles = GameObject.FindObjectsOfType<UnityEngine.AI.NavMeshObstacle>();
        var cols = new List<Collider>();
        foreach (var o in obstacles)
        {
            if (o.gameObject != go)
            {
                cols.Add(o.gameObject.GetComponent<Collider>());
            }
        }

        foreach (var v in verts)
        {
            NavMeshHit hit;
            var vReal = go.transform.TransformPoint(v);
            NavMesh.SamplePosition(vReal, out hit, 20, NavMesh.AllAreas);

            bool onXAxis = Mathf.Abs(hit.position.x - vReal.x) < 0.5f;
            bool onZAxis = Mathf.Abs(hit.position.z - vReal.z) < 0.5f;
            bool hitCollider = cols.Any(c => c.bounds.Contains(vReal));

            if (!onXAxis || !onZAxis || hitCollider)
            {
                return false;
            }
        }

        return true;
    }

    // Use this for initialization
    void Start()
    {
        foreach (var p in Teams)
        {
            foreach (var u in p.StartingUnits)
            {
                var go = (GameObject)GameObject.Instantiate(u, p.Location.position, p.Location.rotation);

                var player = go.AddComponent<Team>();
                player.Info = p;

                if (Team.Default == null) Team.Default = p;
                go.AddComponent<RightClickNavigation>();
                go.AddComponent<ActionSelect>();
                
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
