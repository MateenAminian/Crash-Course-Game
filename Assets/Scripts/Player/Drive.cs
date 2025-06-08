using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Drive : NetworkBehaviour
{
    public WheelCollider[] WC;
    public GameObject[] Wheels;

    public float CurrentSpeed = 400;
    public VehicleStats Stats {
        get => _stats;
        set {
            handleStatsChange(value);
            _stats = value;
        }
    }
    [SerializeField]
    public VehicleStats BaseStats;

    public Text Destruction;
    // float score;
    public Text highscore;
    public Text SpeedText;

    public GameObject StatsDisplay;
    private Camera cam;

    public Vector3 CenterOfMass;
    protected Rigidbody rb;


    bool drifting = false;
    bool driftBoost = false;
    float driftBoostTimer = 0f;
    float driftingTimer = 0f;

    private bool _isBraking = false;
    private bool flipped = false;

    private interface INitroState {
        void Update(Drive drive);
    }
    private class NitroStateCharging : INitroState {
        public void Update(Drive drive) {
            if (drive.nitro < drive.Stats.BoostTime) {
                drive.nitro += Mathf.Min(
                        Time.deltaTime,
                        drive.Stats.BoostTime - drive.nitro);
            } else {
                drive._nitroState = new NitroStateCharged();
            }
        }
    }
    private class NitroStateCharged : INitroState {
        public void Update(Drive drive) {
            if (drive.nitro > 0f && Input.GetButton("Boost"))
            {
                drive.nitro -= Mathf.Min(Time.deltaTime, drive.nitro);
                drive.nitroBoost = true;
            }
            else
            {
                drive.nitroBoost = false;
                drive._nitroState = new NitroStateCharging();
            }
        }
    }
    private INitroState _nitroState = new NitroStateCharged();
    public float nitro = 10f;
    public bool nitroBoost = false;

    private VehicleStats _stats;
    private HashSet<IPowerup> _powerups;

    private void handleStatsChange(VehicleStats stats) {
        foreach (WheelCollider wheel in WC)
        {
            WheelFrictionCurve curve = wheel.forwardFriction;
            wheel.forwardFriction = curve;
            curve.asymptoteValue = stats.ForwardFriction;
            curve.stiffness = stats.Drift;
            wheel.forwardFriction = curve;
            curve.asymptoteValue = stats.SidewaysFriction;
            curve.stiffness = stats.Drift;
            wheel.sidewaysFriction = curve;
        }
        rb.mass = stats.Mass;

        // Give nitro if the boost time changed
        if (_stats == null || stats.BoostTime != _stats.BoostTime) {
            nitro = stats.BoostTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //sets the center of mass to bottom of car
        //makes it so car does not flip over easily
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CenterOfMass;

        //retrievs the high score
        // highscore.text = PlayerPrefs.GetFloat("Highscore",0).ToString();

        // Set the stats of the vehicle, causing a stats change
        Stats = BaseStats;

        // set camera
        cam = this.transform.GetChild(0).GetComponent<Camera>();
        // assign camera 
        if (isLocalPlayer)
        {
            //cam = this.transform.GetChild(0).GetComponent<Camera>();
        } else
        {
            //cam.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            if (cam) {
                cam.enabled = false;
            }
            return;
        }

        float vertical_input = Input.GetAxis("Accelerate");
        float horizontal_input = Input.GetAxis("Horizontal");
        Move(vertical_input, horizontal_input);
        Drift();
        // Handle nitro
        _nitroState.Update(this);

        // ShowStats();

        // FlipCar();
        //gets the local velocity of vehicle and displays it as current speed
        var localVelocity = rb.velocity.magnitude;
        cam.fieldOfView = 60 + localVelocity;
        //commented out because code is bugging out
        //SpeedText.text = localVelocity.ToString();
    }

    void Move(float acceleration, float steer)
    {
        if (driftBoostTimer > 1)
        {
            driftBoostTimer = 0;
            driftBoost = false;
            print("No more boosting");
        }
        else if (driftBoost && driftBoostTimer < 1)
        {
            driftBoostTimer += Time.deltaTime;
            print("Boosting");
        }

        acceleration = driftBoost || nitroBoost ?
            Mathf.Clamp(acceleration, -1, 1) * 10 :
            Mathf.Clamp(acceleration, -1, 1) * 4;
        steer = Mathf.Clamp(steer, -1, 1) * Stats.Torque;
        float turn = acceleration * CurrentSpeed;


        //Rotates all the wheels
        for(int i=0; i<4; i++)
        {
            // WC[i].motorTorque = turn;
            // Turn the front 2 tires
            if(i < 2)
            {
                WC[i].steerAngle = steer;
            } else
            {
                WC[i].motorTorque = turn;
            }

            Quaternion rotate;
            Vector3 position;
            WC[i].GetWorldPose(out position, out rotate);
            Wheels[i].transform.position = position;
            Wheels[i].transform.rotation = rotate;

            //brakes on the car
            WC[i].brakeTorque = _isBraking ? Stats.Brake : 0;
        }


        //Sets Top Speed
        if(rb.velocity.magnitude > Stats.TopSpeed)
        {
            for(int i=0; i<4; i++)
            {
                WC[i].motorTorque = Stats.TopSpeed;
            }
        }

        //brakes activates brake when space/B(xbox) is pushed
        if (Input.GetButton("Brake"))
        {
            _isBraking = true;
        }
        //when space is not pushed set brakes back to 0
        else
        {
            _isBraking = false;
        }
    }

    void Drift() 
    {
        // Pull the handbrake, the back two wheels have huge brake force
        WC[2].brakeTorque = WC[3].brakeTorque = (Input.GetButton("Drift")) ? 100000f : 0f;

        for (int i = 2; i < 4; i++)
        {
            // Tire stiffness becomes less than 1 so that the car can slide
            var localVelocity = rb.velocity.magnitude;
            WheelFrictionCurve forwardFriction = WC[i].forwardFriction;
            forwardFriction.stiffness = Input.GetButton("Drift") ? Mathf.SmoothDamp(forwardFriction.stiffness, .9f, ref localVelocity, Time.deltaTime * 1) : Stats.Drift;
            WC[i].forwardFriction = forwardFriction;

            WheelFrictionCurve sidewaysFriction = WC[i].sidewaysFriction;
            sidewaysFriction.stiffness = Input.GetButton("Drift") ? Mathf.SmoothDamp(sidewaysFriction.stiffness, .9f, ref localVelocity, Time.deltaTime * 1) : Stats.Drift;
            WC[i].sidewaysFriction = sidewaysFriction;
        }

        // At the end of a sufficiently long drift, start a drift boost timer
        if (Input.GetButton("Drift"))
        {
            if (drifting)
            {
                driftingTimer += Time.deltaTime;
            } 
            else
            {
                drifting = true;
            }
        }
        else
        {
            if (driftingTimer > 0.5f)
                driftBoost = true;
            drifting = false;
            driftingTimer = 0;
        }
    }

    /*public void FlipCar()
    {
        if (!WC[0].isGrounded && !WC[1].isGrounded &&
            !WC[2].isGrounded && !WC[3].isGrounded)
        {
            print("ungrounded");
            if (Input.GetButton("Flip"))
            {
                print("flip");
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }*/
}
