using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class respawnChildScript : NetworkBehaviour
{

    // Start is called before the first frame update
    public float deactivationTime = 10;
    public float deac = 0;
    void Start()
    {
        deac = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
