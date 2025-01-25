using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/**********************************
 * フェードアニメーション
 **********************************/
public class FadeManager : MonoBehaviour
{
    public static async void FadeImage(Image target, float a, float t, Action callback = default)
    {
        if(target == null)
        {
            Debug.LogError("FadeManager: 'target not found.'");
        }

        float deltaTime = 0;

        Color c = target.color;
        float alpha = c.a;

        while ( deltaTime <= t || target == null )
        {
            #if UNITY_EDITOR
            if (!EditorApplication.isPlaying) return;
            #endif

            deltaTime += Time.deltaTime;

            c.a = Mathf.Lerp(alpha, a, deltaTime / t);
            
            target.color = c;

            //Debug.Log($"Fade:deltaTime = { deltaTime }, alpah = { c.a.ToString("F2") }, time = { (deltaTime / t * 100).ToString("F2") }%");

            await Task.Yield();
        }

        if(callback != null) callback();
    }
}
