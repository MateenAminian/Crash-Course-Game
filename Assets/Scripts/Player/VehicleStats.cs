using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VehicleStats
{
    public float BoostForce = 200;
    public float BoostTime = 10;
    public float Brake = 0;
    public float TopSpeed = 400;
    public float Torque = 30;
    public float Mass = 1000;
    public float ForwardFriction = 1;
    public float SidewaysFriction = 0.6f;
    public float Drift = 0.9f;

    public VehicleStats()
    {
    }

    public VehicleStats(VehicleStats copy)
    {
        BoostTime = copy.BoostTime;
        BoostForce = copy.BoostForce;
        Brake = copy.Brake;
        TopSpeed = copy.TopSpeed;
        Torque = copy.Torque;
        Mass = copy.Mass;
        ForwardFriction = copy.ForwardFriction;
        SidewaysFriction = copy.SidewaysFriction;
    }

    public static VehicleStats operator +(VehicleStats a, VehicleStats b)
    {
        var result = new VehicleStats();
        result.BoostTime = a.BoostTime + b.BoostTime;
        result.BoostForce = a.BoostForce + b.BoostForce;
        result.Brake = a.Brake + b.Brake;
        result.TopSpeed = a.TopSpeed + b.TopSpeed;
        result.Torque = a.Torque + b.Torque;
        result.Mass = a.Mass + b.Mass;
        result.ForwardFriction = a.ForwardFriction + b.ForwardFriction;
        result.SidewaysFriction = a.SidewaysFriction + b.SidewaysFriction;
        return result;
    }

    public static VehicleStats operator -(VehicleStats a)
    {
        var result = new VehicleStats();
        result.BoostTime = -a.BoostTime;
        result.BoostForce = -a.BoostForce;
        result.Brake = -a.Brake;
        result.TopSpeed = -a.TopSpeed;
        result.Torque = -a.Torque;
        result.Mass = -a.Mass;
        result.ForwardFriction = -a.ForwardFriction;
        result.SidewaysFriction = -a.SidewaysFriction;
        return result;
    }

    public static VehicleStats operator -(VehicleStats a, VehicleStats b)
    {
        return a + (-b);
    }
}
