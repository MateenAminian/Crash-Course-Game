using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public HealthBehavior spawn;
    float respawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawn = GetComponent<HealthBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Respawn") && !Input.GetButton("Accelerate"))
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer > 5f)
            {
                // get random number
                /*int rand = Random.Range(0, spawn.spawnpoints.Length);
                transform.position = spawn.spawnpoints[rand].transform.position;
                transform.rotation = spawn.spawnpoints[rand].transform.rotation;*/
                spawn.respawn();
                respawnTimer = 0f;
            }
        } else
        {
            respawnTimer = 0;
        }
    }
}
