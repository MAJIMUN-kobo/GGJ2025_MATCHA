using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class GameManager : MonoBehaviour
{
    enum GAME_STATE {
        INIT = 0,
        PLAYING,
        END,
    }
    private GAME_STATE state = GAME_STATE.INIT;

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
        switch (this.state) {
            case GAME_STATE.INIT:
                this.Init();
                break;
            case GAME_STATE.PLAYING:
                this.Playing();
                break;
            case GAME_STATE.END:
                this.End();
                break;
        }
    }

    private void Init() {
        this.judgeManager.Reset();
        this.timeManager.Reset();
        this.state = GAME_STATE.PLAYING;
    }

    private void Playing() {
        // for debug text : mouse position
        Vector2 position = this.deviceManager.GetMousePositionNormalized();
        this.uIManager.GetComponent<UIText_MousePosition>().ShowPosition(position);
        this.judgeManager.SetMousePosition(position);

        
        // for degug text : judgement
        this.judgeManager.Scoring();
        JudgeManager.SCORE score = this.judgeManager.GetScore();
        this.uIManager.GetComponent<UIText_Score>().ShowScore(score);
        //Debug.Log("Score:" + score.ToString());

        // for debug text : timer
        int remainingTime = this.timeManager.GetRemainingTime();
        this.uIManager.GetComponent<UIText_Timer>().ShowTimer(remainingTime);
        if (remainingTime <= 0) {
            this.state = GAME_STATE.END;
        }
    }

    private void End() {
        Debug.Log("Final Score: " + this.judgeManager.GetFinalScore().ToString());
    }
}
