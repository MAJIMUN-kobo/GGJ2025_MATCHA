using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIText_Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public const string NUMBER_RESOURCES_PATH = "Sprites/UI/Numbers/";

    public TMP_Text uiText;

    public int TimeDigits = 2;
    public Image uiNumberTemp;

    private Image[] uiDigits;
    private Sprite[] numbers;
    
    void Start()
    {
        // 数字テクスチャの一括読込
        LoadNumberResources();

        if (this.uiText != null)
        {
            // テキストの位置を中央上に配置
            RectTransform rectTransform = this.uiText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 1); // アンカーを中央上に設定
            rectTransform.anchorMax = new Vector2(0.5f, 1);
            rectTransform.pivot = new Vector2(0.5f, 1); // ピボットを中央上に設定
            rectTransform.anchoredPosition = new Vector2(0, -10); // 中央上から少し下に配置
            rectTransform.sizeDelta = new Vector2(500, 200); // テキスト領域の幅と高さを設定

            CreateUI(rectTransform);
            

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
        SetTimerImage(time);
        uiText.text = time.ToString();
    }

    /// <summary>
    /// スプライトの一括読み込み
    /// </summary>
    private void LoadNumberResources()
    {
        this.numbers = Resources.LoadAll<Sprite>(NUMBER_RESOURCES_PATH);
        // Debug.Log($"Loaded numbers texture >> { this.numbers.Length }");
    }


    /// <summary>
    /// 桁数分のImageを生成
    /// </summary>
    /// <param name="parent">親オブジェクト</param>
    private void CreateUI(Transform parent = default)
    {
        if(this.uiNumberTemp == null)
        {
            Debug.LogError("数字を表示するImageが設定されていません。Inspectorで設定を行ってください。");
            return;
        }

        this.uiDigits = new Image[TimeDigits];
        for(int i = 0; i < TimeDigits; i++)
        { 
            this.uiDigits[i] = Instantiate(uiNumberTemp, parent);
            this.uiDigits[i].rectTransform.anchorMin = new Vector2(0.5f, 1f);
            this.uiDigits[i].rectTransform.anchorMax = new Vector2(0.5f, 1f);
            this.uiDigits[i].rectTransform.pivot = new Vector2(0.5f, 1f);
            this.uiDigits[i].rectTransform.anchoredPosition = new Vector2(0f, -10f);
            this.uiDigits[i].rectTransform.sizeDelta = new Vector2(50, 50);

            // 親オブジェクトを基準に配置
            float w = this.uiDigits[i].rectTransform.sizeDelta.x;
            this.uiDigits[i].transform.localPosition = new Vector2((i * w) - ((TimeDigits * w) / 4) , 0);
        }
    }


    private void SetTimerImage(int time)
    {
        string numberStr = time.ToString($"D{ TimeDigits }");

        try
        {
            for(int i = 0; i < TimeDigits; i++)
            {
                int digit = int.Parse(numberStr[i].ToString());
                uiDigits[i].sprite = numbers[digit];
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError($"数字のテクスチャが読み込まれていない可能性があります。 \n{ e }");
        }
    }
}
