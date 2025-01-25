using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMousePosition : MonoBehaviour
{
    public TMP_Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 2, 0);

        // オブジェクトを X軸方向に90度回転
        transform.rotation = Quaternion.Euler(90, 0, 0);

        // UIのテキストを左端に配置
        if (uiText != null)
        {
            RectTransform rectTransform = uiText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1); 
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1); 
            rectTransform.anchoredPosition = new Vector2(10, -10); 
        }
        else
        {
            Debug.LogError("UI Textが設定されていません！");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPosition(Vector2 position) {
        uiText.text = position.ToString();
    }
}
