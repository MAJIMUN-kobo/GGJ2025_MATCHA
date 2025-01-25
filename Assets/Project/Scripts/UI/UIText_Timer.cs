using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIText_Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text uiText;

    void Start()
    {
        if (this.uiText != null)
        {
            // テキストの位置を中央上に配置
            RectTransform rectTransform = this.uiText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 1); // アンカーを中央上に設定
            rectTransform.anchorMax = new Vector2(0.5f, 1);
            rectTransform.pivot = new Vector2(0.5f, 1); // ピボットを中央上に設定
            rectTransform.anchoredPosition = new Vector2(0, -10); // 中央上から少し下に配置
            rectTransform.sizeDelta = new Vector2(500, 200); // テキスト領域の幅と高さを設定

            // テキストの内容とスタイルを設定
            this.uiText.text = "";
            this.uiText.fontSize = 64; // フォントサイズを64に設定
            this.uiText.alignment = TextAlignmentOptions.Top;
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

    public void ShowTimer(int time) {
        uiText.text = time.ToString();
    }
}
