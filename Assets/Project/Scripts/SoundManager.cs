using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource resultAudioSource; // AudioSource コンポーネントをアタッチ
    public AudioClip congratulations;   // 再生する音声ファイルをアタッチ

    // Start is called before the first frame update
    void Start()
    {
        this.resultAudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCongratulations () {
        this.resultAudioSource.PlayOneShot(congratulations);
    }
}
