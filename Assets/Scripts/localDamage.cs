using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localDamage : MonoBehaviour
{
    protected Rigidbody rb;
    private ParticleSystem system;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        var go = new GameObject("Particle System");
        system = go.AddComponent<ParticleSystem>();
        //go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
        var emitParams = new ParticleSystem.EmitParams();
        system.time = 1;
        var main = system.main;
        main.playOnAwake = false;
        system.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        List<ContactPoint> ContactList = new List<ContactPoint>();
        collision.GetContacts(ContactList);
        foreach (var contact in ContactList)
        {
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position = contact.point;
            emitParams.velocity = new Vector3(0.0f, 0.0f, -2.0f);
            system.Emit(emitParams, 1);
        }

    }
}
