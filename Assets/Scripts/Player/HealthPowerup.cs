using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour, IPowerup
{
    public float AddedHealth;
    public AudioClip sound;
    public void ApplyTo(Drive drive)
    {
        var playerHealth = drive.GetComponent<HealthBehavior>();
        if (AddedHealth > 0) {
            playerHealth.maxHealth += AddedHealth;
            playerHealth.currentHealth += AddedHealth;
        } else {
            playerHealth.maxHealth += AddedHealth;
            playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth, playerHealth.maxHealth);
        }
    }

    public void RemoveFrom(Drive drive)
    {
        var playerHealth = drive.GetComponent<HealthBehavior>();
        playerHealth.maxHealth -= AddedHealth;
        playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth, playerHealth.maxHealth);
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
