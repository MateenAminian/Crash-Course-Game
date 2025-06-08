using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleStatsPowerup : MonoBehaviour, IPowerup
{
    public AudioClip sound;
    public VehicleStats AddedStats;
    public void ApplyTo(Drive drive)
    {
        drive.Stats = drive.Stats + AddedStats;
    }

    public void RemoveFrom(Drive drive)
    {
        drive.Stats = drive.Stats - AddedStats;
    }

    private void OnTriggerEnter(Collider other) {
        var drive = other.GetComponentInParent<Drive>();
        if (drive) {
            ApplyTo(drive);
            Destroy(gameObject);
            // TODO add particle effects.
        }
        AudioSource.PlayClipAtPoint(sound, transform.position);
    }
}
