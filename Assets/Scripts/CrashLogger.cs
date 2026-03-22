using UnityEngine;
using TMPro;

public class CrashLogger : MonoBehaviour
{
    private TextMeshProUGUI debugText;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.logMessageReceived += HandleLog;

        CreateUI();

        debugText.text = "Logger Started\n"; // test
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            debugText.text += "\n" + logString;
        }
    }

    void CreateUI()
    {
        GameObject canvasObj = new GameObject("DebugCanvas");
        DontDestroyOnLoad(canvasObj);

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999;

        GameObject textObj = new GameObject("DebugText");
        textObj.transform.SetParent(canvasObj.transform);

        debugText = textObj.AddComponent<TextMeshProUGUI>();

        debugText.fontSize = 40;
        debugText.color = Color.red;

        RectTransform rect = debugText.rectTransform;
        rect.sizeDelta = new Vector2(2000, 1000);
        rect.anchoredPosition = new Vector2(0, -100);
    }
}
