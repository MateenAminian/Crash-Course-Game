using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    public Transform target;
    public float distance = 15f;
    public float height = 4f;
    public float damping = 0.9f;
    public bool smoothRotation = true;
    public bool followBehind = true;
    public float rotationDamping = 4.0f;
    public Vector2 turn;
    public float sensitivity = .5f;
    private Vector3 rotateValue;

    private void Start()
    {
        //target = this.transform.GetParent().GetChild(1).GetComponent<Transform>();;
    }

    


    void FixedUpdate()
    {
        if (Input.GetAxis("Camera Vertical") == 0 && Input.GetAxis("Camera Horizontal") == 0 && !Input.GetMouseButton(1))
        {
            Vector3 wantedPosition;
            if (followBehind)
                wantedPosition = target.TransformPoint(0, height, -distance);
            else
                wantedPosition = target.TransformPoint(0, height, distance);

            transform.position = Vector3.Lerp(transform.position, wantedPosition, damping);

            if (smoothRotation)
            {
                Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }
            else transform.LookAt(target, target.up);
            turn.x = 0;
        }
        else
        {
            // This is if we want camera to rotate around car
            /* turn.y += Input.GetAxis("Camera Vertical");
            turn.y = Mathf.Clamp(turn.y, 0, 90);
            turn.x += Input.GetAxis("Camera Horizontal") / 20;
            Vector3 wantedPosition = target.TransformPoint(distance * Mathf.Cos(turn.x), turn.y, distance * Mathf.Sin(-turn.x));
            transform.position = Vector3.Lerp(transform.position, wantedPosition, damping);
            
            Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);*/

            // Camera is stationary in respect to car
            
            Vector3 wantedPosition = target.TransformPoint(0, height, -distance);
            transform.position = Vector3.Lerp(transform.position, wantedPosition, damping);

            turn.x = Input.GetAxis("Camera Horizontal") * 4f;
            turn.y = Input.GetAxis("Camera Vertical") * 1.5f;

            if (Input.GetMouseButton(1))
            {
                turn.x = Input.GetAxis("Mouse X") * 3f;
                turn.y = Input.GetAxis("Mouse Y") * 1.5f;
            }

            rotateValue = new Vector3(turn.y, -turn.x, 0);
            transform.localEulerAngles = transform.localEulerAngles - rotateValue;
            if (transform.localEulerAngles.y > 135 && transform.localEulerAngles.y < 180)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 135, transform.localEulerAngles.z);
            } else if (transform.localEulerAngles.y > 180 && transform.localEulerAngles.y < 225)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 225, transform.localEulerAngles.z);
            }
            if (transform.localEulerAngles.x > 30 && transform.localEulerAngles.x < 180)
            {
                transform.localEulerAngles = new Vector3(30, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
            else if (transform.localEulerAngles.x > 180 && transform.localEulerAngles.x < 335)
            {
                transform.localEulerAngles = new Vector3(335, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }
    }


    /*public float pLerp = .02f;
    public float rLerp = .01f;


    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rLerp);

    }*/
}
