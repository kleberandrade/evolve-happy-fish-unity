using System.IO;
using UnityEngine;

public class Timelapsed : MonoBehaviour
{
    public string m_Filename = "figure";
    public float m_RepeatTime = 1.0f;

    private void Start()
    {
        Directory.CreateDirectory($"{Application.dataPath}/../Save");
        InvokeRepeating("Screenshot", 0.0f, m_RepeatTime);
    }

    private void Screenshot()
    {
        ScreenCapture.CaptureScreenshot($"{Application.dataPath}/../Save/{m_Filename}_{Time.time:000000}.png");
    }
}