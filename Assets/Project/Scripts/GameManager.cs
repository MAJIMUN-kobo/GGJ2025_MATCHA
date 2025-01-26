using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;

public class GameManager : MonoBehaviour
{
   public  enum GAME_STATE {
        INIT = 0,
        PLAYING,
        END,
    }
    private GAME_STATE state = GAME_STATE.INIT;

    public Canvas uIManager;
    private DeviceManager deviceManager;

    private JudgeManager judgeManager;

    private TimeManager timeManager;

    private EffectManager effectManager;

    private UIResultManager uIResultManager;

    public SEManager sEManager;

    public GameObject chasenObj;
    

    // Start is called before the first frame update
    void Start()
    {
        this.deviceManager = this.GetComponent<DeviceManager>();
        this.judgeManager = this.GetComponent<JudgeManager>();            
        this.timeManager = this.GetComponent<TimeManager>();            
        this.effectManager = this.GetComponent<EffectManager>();
        this.uIResultManager = this.GetComponent<UIResultManager>();
        this.sEManager = this.GetComponent<SEManager>();
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
        this.effectManager.PlaySakuraEffect();
        this.uIResultManager.HideResult();
        this.state = GAME_STATE.PLAYING;
        this.chasenObj.SetActive(true);
    }

    private void Playing() {
        // for debug text : mouse position
        Vector2 mousePosition = this.deviceManager.GetMousePositionNormalized();
        Vector2 chasenPosition = this.deviceManager.GetChasenPositionNormalized();
        this.uIManager.GetComponent<UIText_MousePosition>().ShowPosition(chasenPosition);
        this.judgeManager.SetMousePosition(mousePosition);
        this.judgeManager.SetChasenPosition(chasenPosition);

        
        this.judgeManager.Scoring();
        JudgeManager.SCORE score = this.judgeManager.GetScore();
        this.effectManager.UpdateSakuraEffect(score);
        this.uIManager.GetComponent<UIText_Score>().ShowScore(score);
        //Debug.Log("Score:" + score.ToString());

        // for debug text : timer
        int remainingTime = this.timeManager.GetRemainingTime();
        this.uIManager.GetComponent<UIText_Timer>().ShowTimer(remainingTime);
        if (remainingTime <= 0) {
            this.sEManager.PlayCongratulations();
            this.chasenObj.SetActive(false);
            this.state = GAME_STATE.END;
        }
    }
    


    private void End() {
        this.effectManager.ClearSakuraEffect();
        JudgeManager.SCORE score = this.judgeManager.GetFinalScore();
        this.uIResultManager.ShowResult(score);
        //Debug.Log("Final Score: " + this.judgeManager.GetFinalScore().ToString());

        if (Input.GetKeyDown(KeyCode.Space)) {
            this.state = GAME_STATE.INIT;
        }
    }

    public GAME_STATE GetState(){
        return this.state;
    }
}
