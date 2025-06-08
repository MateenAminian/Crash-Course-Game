using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceOnImpact : MonoBehaviour
{
    public float Health;
    public float Damage = 1f;

    void OnCollisionEnter(Collision collision)
    {

        //checks if the player collides with the objects
        if(collision.gameObject.name == "Player")
        {
            //object takes damage based on collision force
            //makes the damage a whole number(rounded down)
            Health -= Damage * Mathf.Ceil(collision.relativeVelocity.magnitude);
        }

        //destroys the cube when health reaches 0
        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
        
        
    }
}
