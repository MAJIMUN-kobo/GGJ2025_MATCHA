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
        Vector2 position = this.deviceManager.getMousePositionNormalized();
        this.uIManager.GetComponent<UIText_MousePosition>().showPosition(position);
        this.judgeManager.setMousePosition(position);

        
        // for degug text : judgement
        JudgeManager.SCORE score = this.judgeManager.getScore();
        this.uIManager.GetComponent<UIText_Score>().showScore(score);
        //Debug.Log("Score:" + score.ToString());

        // for debug text : timer
        int remainingTime = this.timeManager.getRemainingTime();
        this.uIManager.GetComponent<UIText_Timer>().showTimer(remainingTime);
    }
}
