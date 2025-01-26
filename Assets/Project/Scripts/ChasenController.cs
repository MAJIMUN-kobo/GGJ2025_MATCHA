using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ChasenController : MonoBehaviour
{
    [SerializeField] private float _snapAngle = 30;
    [SerializeField] private float _snapSpeed = 10;

    private JudgeManager judgeManager;

    private GameManager gameManager;

    private float _snapTime = 0;

    private float _delayTime = 0;

    public void Init(JudgeManager judge)
    {
        this.judgeManager = judge;
    }

    void Start()
    {
        this.judgeManager = GameObject.FindObjectOfType<JudgeManager>();
        this.gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Snap();
    }


    void Snap()
    {
        if (this.judgeManager == null) return;

        if (this.gameManager.GetState() == GameManager.GAME_STATE.END) return;

        JudgeManager.SCORE score = this.judgeManager.GetScore();
        
        _delayTime += Time.deltaTime;

        if(score == JudgeManager.SCORE.NOT_GOOD || score == JudgeManager.SCORE.READY) 
        {
            _snapTime = 0;
            _delayTime = 0;
            return;
        }

        _snapTime += _snapSpeed * Time.deltaTime;

        if(_delayTime >= 0.3f)
        {
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

            _delayTime = 0;
        }

        Vector3 angles = this.transform.eulerAngles;
        angles.x += Mathf.Sin(_snapTime) * _snapAngle;
        angles.x = Mathf.Clamp(angles.x, -_snapAngle, _snapAngle);

        this.transform.eulerAngles = angles;
    }
}
