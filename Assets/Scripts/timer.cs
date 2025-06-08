using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float startTime;
    private float currentTime;
    public bool timerIsRunning = true;
    public string secondMap;

    public string TimeText { get; private set; }
    // Start is called before the first frame update

    void Start()
    {
        currentTime = startTime;
        //preloading scene to save time later
        GetComponent<menuDriver>().load(secondMap);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(currentTime / 60);
                float seconds = Mathf.FloorToInt(currentTime % 60);
                TimeText = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                currentTime = 0;
                GetComponent<menuDriver>().changeScene(secondMap);
                
            }
        }
    }
}
