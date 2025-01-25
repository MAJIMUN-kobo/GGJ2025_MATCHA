using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class GameManager : MonoBehaviour
{
    public Canvas uIManager;
    private DeviceManager deviceManager;

    private JudgeManager judgeManager;

    private TimeManager timeManager;
    

    // Start is called before the first frame update
    void Start()
    {
        this.deviceManager = this.GetComponent<DeviceManager>();
        this.judgeManager = this.GetComponent<JudgeManager>();            
        this.timeManager = this.GetComponent<TimeManager>();            
    }

    // Update is called once per frame
    void Update()
    {
        // for debug text : mouse position
        Vector2 position = this.deviceManager.GetMousePositionNormalized();
        this.uIManager.GetComponent<UIText_MousePosition>().ShowPosition(position);
        this.judgeManager.SetMousePosition(position);

        
        // for degug text : judgement
        JudgeManager.SCORE score = this.judgeManager.GetScore();
        this.uIManager.GetComponent<UIText_Score>().ShowScore(score);
        //Debug.Log("Score:" + score.ToString());

        // for debug text : timer
        int remainingTime = this.timeManager.GetRemainingTime();
        this.uIManager.GetComponent<UIText_Timer>().ShowTimer(remainingTime);
        if (remainingTime <= 0) {
            Debug.Log("Final Score: " + this.judgeManager.GetFinalScore().ToString());
            this.judgeManager.Reset();
            this.timeManager.Reset();
        }
    }
}
