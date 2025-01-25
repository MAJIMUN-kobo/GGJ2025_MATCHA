using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIText_Score : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text uiText;

    void Start()
    {
        // TextMeshProオブジェクトの初期設定
        if (this.uiText != null)
        {
            // テキストの位置を右上端に配置
            RectTransform rectTransform = this.uiText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(1, 1); // アンカーを右上に設定
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1); // ピボットを右上に設定
            rectTransform.sizeDelta = new Vector2(500, 200); // テキスト領域の幅と高さを設定
            rectTransform.anchoredPosition = new Vector2(-10, -10); // 右上から少し内側に配置

            // テキストの内容とスタイルを設定
            this.uiText.text = "READY";
            this.uiText.fontSize = 64; // フォントサイズを設定
            this.uiText.alignment = TextAlignmentOptions.TopRight; // テキストを右上に揃える
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScore(JudgeManager.SCORE score) {
        uiText.text = score.ToString();
    }
}
