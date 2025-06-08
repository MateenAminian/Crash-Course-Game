using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class respawnScript : NetworkBehaviour
{

    private List<GameObject> unUseableRespawnPoints;
    private List<GameObject> useableRespawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        useableRespawnPoints = new List<GameObject>();
        unUseableRespawnPoints = new List<GameObject>();
        foreach (Transform child in transform)
        {
            useableRespawnPoints.Add(child.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i< unUseableRespawnPoints.Count; i++)
        {
            respawnChildScript scr = unUseableRespawnPoints[i].GetComponent<respawnChildScript>();
            scr.deac -= Time.deltaTime;
            if(scr.deac <= 0)
            {
                useableRespawnPoints.Add(unUseableRespawnPoints[i]);
                unUseableRespawnPoints.Remove(unUseableRespawnPoints[i]);
                break; //this means only one respawn point becomes available each frame, but more importantly it stops the script from breaking entirely
            }
        }
    }

    public Vector3 respawn() //returns world point to respawn at in world space
    {
        int min = 0;
        int max = useableRespawnPoints.Count - 1;
        int ran = Random.Range(min, max);

        GameObject point = useableRespawnPoints[ran];

        unUseableRespawnPoints.Add(point);
        useableRespawnPoints.Remove(point);

        respawnChildScript scr = point.GetComponent<respawnChildScript>();
        scr.deac = scr.deactivationTime;

        return point.GetComponent<Transform>().TransformPoint(new Vector3 (0,0,0));
    }
}
