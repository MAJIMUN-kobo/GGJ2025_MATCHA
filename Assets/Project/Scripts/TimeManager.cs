using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float startTime = 0;
    private float elapsedTime = 0;

    private float GAME_PERIOD = 5;

    // Start is called before the first frame update
    void Start()
    {
        this.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        this.elapsedTime = Time.time - this.startTime;
        if (this.elapsedTime >= GAME_PERIOD) {
           this.Reset();     
        }
    }

    public void Reset() {
        this.startTime = Time.time;
    }

    public int getRemainingTime () {
        return Mathf.RoundToInt(GAME_PERIOD - this.elapsedTime);
    }
}
