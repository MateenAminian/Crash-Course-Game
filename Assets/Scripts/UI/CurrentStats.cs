using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentStats : MonoBehaviour
{
    public Text Mass;
    public Text Torque;
    public Text TopSpeed;
    public Text BoostForce;
    public Text Drift;

    // Update is called once per frame
    void Update()
    {
        var stats = GetComponent<Drive>().Stats;
        Mass.text = stats.Mass.ToString();
        Torque.text = stats.Torque.ToString();
        TopSpeed.text = stats.TopSpeed.ToString();
        BoostForce.text = stats.BoostForce.ToString();
        Drift.text = stats.Drift.ToString();
    }
}
