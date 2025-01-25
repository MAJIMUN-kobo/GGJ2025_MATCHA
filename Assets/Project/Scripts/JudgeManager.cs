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
        NOT_GOOD = 1,
        GOOD = 2,
        GRATE = 3,
    }
    private List<SCORE> scores = new List<SCORE>();

    private int LIST_SIZE = 30;
    private int MULTIPLIER = 1000;

    private int JUDGEMENT_THRESHOLD = 10;

    private List<Vector2> mousePositions;

    private float JUDGEMENT_PERIOD = 1;
    private float judgeStartTime;


    // Start is called before the first frame update
    void Start()
    {
        this.mousePositions = new List<Vector2>();
        this.judgeStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - this.judgeStartTime;
        if (elapsedTime >= JUDGEMENT_PERIOD) {
            this.scores.Add(this.Judge());
            this.judgeStartTime = Time.time;
        }

        /*
        Debug.Log("Positions: -------------");
        foreach (Vector2 pos in this.mousePositions)
        {
            Debug.Log($"Vector2: {pos}");
        }
        */
    }

    public void setMousePosition (Vector2 position) {
        if (this.mousePositions.Count >= LIST_SIZE) {
            this.mousePositions.RemoveAt(0);
        }
        this.mousePositions.Add(position);
    }

    private SCORE Judge () {
        if (this.mousePositions.Count >= LIST_SIZE) {
            Vector2 variance = this.CalculateVariance(this.mousePositions);
            //Debug.Log($"Variance: X: {variance.x}, Y: {variance.y}");

            // Judge!
            if (variance.x > JUDGEMENT_THRESHOLD && variance.y > JUDGEMENT_THRESHOLD) {
                return SCORE.GRATE;
            }
            else if (variance.x > JUDGEMENT_THRESHOLD || variance.y > JUDGEMENT_THRESHOLD) {
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
        int x = Mathf.RoundToInt(sumOfSquares.x / data.Count * this.MULTIPLIER);
        int y = Mathf.RoundToInt(sumOfSquares.y / data.Count * this.MULTIPLIER);

        return new Vector2(x, y);
    }

    public SCORE getScore() {
        if (this.scores.Count > 0) {
            return this.scores[this.scores.Count - 1];
        }
        return SCORE.READY;
    }

    public SCORE getFinalScore () {
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
