using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ChasenController : MonoBehaviour
{
    [SerializeField] private float _snapAngle = 30;
    [SerializeField] private float _snapSpeed = 10;

    private JudgeManager judgeManager;
    private float _snapTime = 0;


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
        

        if(score == JudgeManager.SCORE.NOT_GOOD || score == JudgeManager.SCORE.READY) 
        {
            _snapTime = 0;
            return;
        }

        _snapTime += _snapSpeed * Time.deltaTime;

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

        Vector3 angles = this.transform.eulerAngles;
        angles.x += Mathf.Sin(_snapTime) * _snapAngle;
        angles.x = Mathf.Clamp(angles.x, -_snapAngle, _snapAngle);

        this.transform.eulerAngles = angles;
    }
}
