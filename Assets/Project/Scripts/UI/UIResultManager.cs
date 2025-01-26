using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultManager : MonoBehaviour
{
    public Image result;
    public Sprite result_Amazing;
    public Sprite result_Grate;
    public Sprite result_Good;
    public Sprite result_NotGood;


    // Start is called before the first frame update
    void Start()
    {
        this.result.enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResult(JudgeManager.SCORE score) {
        this.result.enabled = true;
        switch (score) {
            case JudgeManager.SCORE.AMAZING:
                this.result.sprite = result_Amazing;
                break;
            case JudgeManager.SCORE.GRATE:
                this.result.sprite = result_Grate;
                break;
            case JudgeManager.SCORE.GOOD:
                this.result.sprite = result_Good;
                break;
            case JudgeManager.SCORE.NOT_GOOD:
                this.result.sprite = result_NotGood;
                break;
        }   
    }

    public void HideResult() {
        this.result.enabled = false;
    }
}
