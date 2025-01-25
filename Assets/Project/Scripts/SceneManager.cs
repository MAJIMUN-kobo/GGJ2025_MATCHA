using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    [SerializeField] private FadeCanvas _fadeCanvas;

    public void ChangeScene(string sceneName, float delay = 0)
    {
        FadeOut(delay, () => { 
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName).completed += ( data ) => { 
                _fadeCanvas.Initialize(); 
                FadeIn(delay);
            };
        });
    }
    
    public void FadeIn(float t, Action callback = default)
    {
        Image fadeCurtain = _fadeCanvas.image;
        Color c = fadeCurtain.color;
        c.a = 1;
        fadeCurtain.color = c;

        FadeManager.FadeImage(fadeCurtain, 0, t, callback);
    }


    public void FadeOut(float t, Action callback = default)
    {
        Image fadeCurtain = _fadeCanvas.image;
        Color c = fadeCurtain.color;
        c.a = 0;
        fadeCurtain.color = c;

        FadeManager.FadeImage(fadeCurtain, 1, t, callback );
    }


    private void CreateFade()
    {
        FadeCanvas canvas = Resources.Load<FadeCanvas>("Prefabs/FadeCanvas");
        _fadeCanvas = Instantiate<FadeCanvas>(canvas);
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        CreateFade();

        ChangeScene("GameScene", 3);    // ‚Æ‚è‚Ü‘JˆÚ
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
