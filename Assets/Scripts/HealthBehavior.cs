using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class HealthBehavior : NetworkBehaviour
{
    public float maxHealth;
    public float currentHealth;
    protected Rigidbody rb;

    //divides damage by this number, is used to keep the health numbers sane even with high mass and speed
    public float damageFactor = 1000; //reccomand 1000

    //following are used for invincibility
    public float Idamage;
    public float Irespawn;

    private float idam;
    private float ires;

    public GameObject respawnMaster;

    public bool canRespawn = true;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();

        idam = 0;
        ires = Irespawn;
        respawnMaster = GameObject.Find("respawnMaster");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && canRespawn)
        {
            respawn();
        }
        if(idam > 0)
        {
            idam -= Time.deltaTime;
        }
        if(ires > 0)
        {
            ires -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        //checks if the player collides with the objects
        if (collision.gameObject.tag == "DoesDamage" && ires <= 0 && idam <= 0)
        {
            currentHealth = currentHealth - calculateDamage(collision);
            idam = Idamage;
        }

    }
    
    float calculateDamage(Collision collision)
    {
        Rigidbody rb2 = collision.collider.GetComponent<Rigidbody>();
        float damage = 0;
        if (rb2 != null)
        {
            Vector3 f1 = rb.mass * rb.velocity;
            Vector3 f2 = rb2.mass * rb2.velocity;
            Vector3 deltaForce = f1 - f2;
            damage = deltaForce.magnitude;

        }
        else
        {
            Vector3 force = rb.mass * rb.velocity;
            damage = force.magnitude;
        }
        Vector3 pos = calculatePosition(collision);
        float localFactor = localDamageFactor(pos);

        return damage*localFactor/damageFactor;
    }

    Vector3 calculatePosition(Collision collision)
    {
        ContactPoint[] contacts = new ContactPoint[50];
        int length = collision.GetContacts(contacts);
        float avgX = 0, avgY = 0, avgZ = 0;
        for(int i = 0; i<length; i++)
        {
            avgX += contacts[i].point.x / length;
            avgY += contacts[i].point.y / length;
            avgZ += contacts[i].point.z / length;

        }
        return new Vector3(avgX, avgY, avgZ); 
    }

    float localDamageFactor(Vector3 pos)
    {
        Vector3 extants = GetComponent<Renderer>().bounds.size;
        float localFactor = 1;

        float widthpos = 2 / 3, heightPos = 2 / 3, depthPos = 2 / 3;
        float sideDamage = 2, heightDamage = 2, frontDamage = 1 / 2, backDamage = 1;
        if (pos.x > extants.x * widthpos / 2 || pos.x < -1 * extants.x * widthpos / 2)
        {
            localFactor *= sideDamage;
        }
        if (pos.y > extants.y * heightPos / 2 || pos.y < -1 * extants.y * heightPos / 2)
        {
            localFactor *= heightDamage;
        }
        if (pos.z > extants.z * depthPos / 2)
        {
            localFactor *= frontDamage;
        }
        if (pos.z < -1*extants.z * depthPos / 2)
        {
            localFactor *= backDamage;
        }
        return localFactor;
    }

    public void respawn()
    {
        Vector3 respoint = respawnMaster.GetComponent<respawnScript>().respawn();

        currentHealth = maxHealth;
        ires = Irespawn;

        gameObject.transform.position = respoint;
    }

}
