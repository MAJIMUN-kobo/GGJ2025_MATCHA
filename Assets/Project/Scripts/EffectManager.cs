using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EffectManager : MonoBehaviour
{
    public ParticleSystem sakuraEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSakuraEffect(JudgeManager.SCORE score) {
        var emission = this.sakuraEffect.emission;
        if (score == JudgeManager.SCORE.AMAZING) {
            emission.rateOverTime = 20f;
        } else if (score == JudgeManager.SCORE.GRATE) {
            emission.rateOverTime = 10f;
        } else if (score == JudgeManager.SCORE.GOOD) {
            emission.rateOverTime = 5f;
        }
        else {
            emission.rateOverTime = 1f;
        }
    }

    public void PlaySakuraEffect() {
        this.sakuraEffect.Play();
    }

    public void ClearSakuraEffect() {
        this.sakuraEffect.Stop();        
        this.sakuraEffect.Clear();        
    }
}
