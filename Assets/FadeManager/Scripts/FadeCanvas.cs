using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    [Header("Component Settings")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasScaler _scaler;
    [SerializeField] private UnityEngine.Camera _uiCamera;

    [Header("Fade Settings")]
    [SerializeField] private Image _fadeImage;


    public Image image
    {
        get { return _fadeImage; }
        set { _fadeImage = value; }
    }

    public Vector2 referenceResolution
    {
        get { return _scaler.referenceResolution; }
        set { _scaler.referenceResolution = value; }
    }


    public void Initialize()
    {
        referenceResolution = new Vector2(Screen.width, Screen.height);
        
        RectTransform fadeTransform = _fadeImage.GetComponent<RectTransform>();
        fadeTransform.sizeDelta = referenceResolution;

        UniversalAdditionalCameraData cameraData = UnityEngine.Camera.main.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(_uiCamera);
    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Initialize();
    }
}
