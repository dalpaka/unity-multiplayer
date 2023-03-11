using UnityEngine;

public class Console : MonoBehaviour
{
    private string logText = "";
    private bool isShowing = false;

    private void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        logText += logString + "\n";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isShowing = !isShowing;
        }
    }

    private void OnGUI()
    {
        if (!isShowing)
        {
            return;
        }

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Debug Console");

        GUI.Label(new Rect(20, 40, Screen.width - 40, Screen.height - 60), logText);
    }
}

