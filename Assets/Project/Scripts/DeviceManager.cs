using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    private Vector2 normalizedMousePosition = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateMouse();
    }

    private void UpdateMouse(){
        Vector3 mousePosition = Input.mousePosition;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        this.normalizedMousePosition = new Vector2(mousePosition.x / screenWidth, mousePosition.y / screenHeight);
    }

    public Vector2 GetMousePositionNormalized() {
        return this.normalizedMousePosition;
    }
}
