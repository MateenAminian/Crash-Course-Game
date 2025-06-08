using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clouds : MonoBehaviour
{
    Vector3 pos;
    public float speed;
    public float maxX;
    public GameObject gameObject;
    public float teleportCloud;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
        if(gameObject.transform.position.x > maxX)
        {
            //debug.log("ban");
            gameObject.transform.Translate(-maxX * teleportCloud * Time.deltaTime, 0, 0);
        }
    }
}
