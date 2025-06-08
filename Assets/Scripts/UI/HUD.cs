using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Drive Player;
    public timer Timer;
    public HUDBar HealthBar;
    public HUDBar BoostBar;
    public Text TimerText;
    public Text SpeedText;

    // Start is called before the first frame update
    void Start()
    {
        // Only display the HUD for the local player
        if (!Player.isLocalPlayer) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var playerHealth = Player.GetComponent<HealthBehavior>();
        BoostBar.FillLevel = Player.nitro / Player.Stats.BoostTime;
        HealthBar.FillLevel = playerHealth.currentHealth / playerHealth.maxHealth;
        TimerText.text = Timer != null ? Timer.TimeText : "0:00";
        var mps = Player.GetComponent<Rigidbody>().velocity.magnitude;
        var mph = mps * 2.23694f;
        SpeedText.text = Mathf.Round(mph) + " mph";
    }
}
