using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float elapsedTime = 0f;
    private bool isRunning = false;
    [SerializeField] Text text;

    public void StartStopwatch()
    {
        isRunning = true;
        Debug.Log("Stopwatch started");
    }

    public void StopStopwatch()
    {
        isRunning = false;
        Debug.Log("Stopwatch stopped");
    }

    public void ResetStopwatch()
    {
        elapsedTime = 0f;
        Debug.Log("Stopwatch reset");
    }

    void Update()
    {
        text.text = elapsedTime.ToString();
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log($"Elapsed Time: {elapsedTime:F2} seconds");
        }
    }

    // 경과 시간 가져오기
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
