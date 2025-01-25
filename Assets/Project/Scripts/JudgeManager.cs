using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    private List<Vector2> mousePositions;

    // Start is called before the first frame update
    void Start()
    {
        this.mousePositions = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Positions: -------------");
        foreach (Vector2 pos in this.mousePositions)
        {
            Debug.Log($"Vector2: {pos}");
        }
    }

    public void setMousePosition (Vector2 position) {
        if (this.mousePositions.Count >= 10) {
            this.mousePositions.RemoveAt(0);
        }
        this.mousePositions.Add(position);
    }
}
