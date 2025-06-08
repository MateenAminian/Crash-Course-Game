using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{

    public Transform[] spawnpoints;
    // Start is called before the first frame update
    void Start()
    {
        spawnpoints = GetComponentsInChildren<Transform>();
    }
}
