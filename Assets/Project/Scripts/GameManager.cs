using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class GameManager : MonoBehaviour
{
    public Canvas uIManager;
    private DeviceManager deviceManager;

    // Start is called before the first frame update
    void Start()
    {
        this.deviceManager = this.GetComponent<DeviceManager>();            
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = this.deviceManager.getMousePositionNormalized();
        this.uIManager.GetComponent<UIMousePosition>().showPosition(position);
    }
}
