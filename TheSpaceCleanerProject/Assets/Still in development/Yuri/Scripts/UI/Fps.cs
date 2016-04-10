using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    float deltaTime = 0.0f;
     
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        Text textUI = GetComponent<Text>();
        textUI.text = calculateFps()+ "\r\n" + PlayerMovement.testDirections;
    }

    public string calculateFps()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        return text;

    }
}