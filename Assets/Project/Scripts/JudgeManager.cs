using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;

public class JudgeManager : MonoBehaviour
{
    public enum SCORE {
        READY = 0,
        NOT_GOOD,
        GOOD,
        GRATE,
    }
    private List<SCORE> scores = new List<SCORE>();

    // for Judge Timing ----------------
    private float JUDGE_PERIOD = 1;
    private float judgeStartTime;
    // ---------------------------------


    // for Mouse Judge -------------------------
    private List<Vector2> mousePositions;
    private int JUDGE_MOUSE_LIST_SIZE = 30;
    private int JUDGE_MOUSE_MULTIPLIER = 1000;
    private int JUDGE_MOUSE_THRESHOLD = 10;
    // -----------------------------------------


    // for Chasen Judge ------------------------

    //------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        this.mousePositions = new List<Vector2>();
        this.judgeStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // add a judged score every certain time
        float elapsedTime = Time.time - this.judgeStartTime;
        if (elapsedTime >= JUDGE_PERIOD) {
            this.scores.Add(this.Judge());
            this.judgeStartTime = Time.time;
        }
    }

    public void SetMousePosition (Vector2 position) {
        if (this.mousePositions.Count >= JUDGE_MOUSE_LIST_SIZE) {
            this.mousePositions.RemoveAt(0);
        }
        this.mousePositions.Add(position);
    }

    private SCORE Judge () {
        SCORE mouseScore = this.JudgeMouse();
        SCORE chasenScore = this.JudgeChasen();

        // a judged score is a larger one 
        return (mouseScore > chasenScore) ? mouseScore : chasenScore;
    }

    private SCORE JudgeMouse() {
        if (this.mousePositions.Count >= JUDGE_MOUSE_LIST_SIZE) {
            Vector2 variance = this.CalculateVariance(this.mousePositions);
            //Debug.Log($"Variance: X: {variance.x}, Y: {variance.y}");

            // Judge!
            if (variance.x > JUDGE_MOUSE_THRESHOLD && variance.y > JUDGE_MOUSE_THRESHOLD) {
                return SCORE.GRATE;
            }
            else if (variance.x > JUDGE_MOUSE_THRESHOLD || variance.y > JUDGE_MOUSE_THRESHOLD) {
                return SCORE.GOOD;
            }
            else {
                return SCORE.NOT_GOOD;
            }
        }
        return SCORE.READY; // Not Judge Yet.
    }

    private Vector2 CalculateVariance(List<Vector2> data)
    {
        if (data == null || data.Count == 0)
        {
            Debug.LogError("The data list is empty or null.");
            return Vector2.zero;
        }

        // 平均を計算
        Vector2 mean = Vector2.zero;
        foreach (Vector2 value in data)
        {
            mean += value;
        }
        mean /= data.Count;

        // 平均との差の二乗の和を計算
        Vector2 sumOfSquares = Vector2.zero;
        foreach (Vector2 value in data)
        {
            sumOfSquares.x += Mathf.Pow(value.x - mean.x, 2);
            sumOfSquares.y += Mathf.Pow(value.y - mean.y, 2);
        }

        // 分散を計算
        int x = Mathf.RoundToInt(sumOfSquares.x / data.Count * this.JUDGE_MOUSE_MULTIPLIER);
        int y = Mathf.RoundToInt(sumOfSquares.y / data.Count * this.JUDGE_MOUSE_MULTIPLIER);

        return new Vector2(x, y);
    }

    private SCORE JudgeChasen() {
        return SCORE.READY;
    }


    public SCORE GetScore() {
        if (this.scores.Count > 0) {
            return this.scores[this.scores.Count - 1];
        }
        return SCORE.READY;
    }

    public SCORE GetFinalScore () {
        if (this.scores == null || this.scores.Count == 0)
        {
            Debug.LogError("The enum list is empty or null.");
            return default; // デフォルト値を返す（MyEnum.ValueA）
        }

        // 出現回数をカウントして最頻値を取得
        var frequency = this.scores
            .GroupBy(x => x)                      // 各要素をグループ化
            .OrderByDescending(g => g.Count())    // 出現回数で降順に並べる
            .FirstOrDefault();                    // 最頻値のグループを取得

        return frequency.Key; // 最頻値を返す
    }

    public void Reset() {
        this.scores.Clear();
    }
}
