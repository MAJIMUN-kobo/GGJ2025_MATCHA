using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ChasenController : MonoBehaviour
{
    private JudgeManager judgeManager;


    public void Init(JudgeManager judge)
    {
        this.judgeManager = judge;
    }

    void Start()
    {
        this.judgeManager = GameObject.FindObjectOfType<JudgeManager>();
    }

    void Update()
    {
        Snap();
    }


    void Snap()
    {
        if (this.judgeManager == null) return;

        JudgeManager.SCORE score = this.judgeManager.GetScore();
        Debug.Log($"SnapScore = { score }");

        switch(score)
        {
            case JudgeManager.SCORE.NOT_GOOD:
                break;

            case JudgeManager.SCORE.GOOD:
                SoundManager.Instance.PlaySE("Screw");
                break;

            case JudgeManager.SCORE.GRATE:
                SoundManager.Instance.PlaySE("Screw");
                break;

            case JudgeManager.SCORE.AMAZING:
                SoundManager.Instance.PlaySE("Screw");
                break;

            case JudgeManager.SCORE.READY:
                break;
        }
    }
}
