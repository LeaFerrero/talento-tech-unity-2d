using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StopwatchTimer : MonoBehaviour
{
    private float elapsedTime = 0f;
    private bool isRunning = true;
    [SerializeField] private TextMeshProUGUI minutesText;
    [SerializeField] private TextMeshProUGUI secondsText;
    public int Minutes { get; private set; }
    public int Seconds { get; private set; }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            elapsedTime += Time.deltaTime;
            Minutes = (int)(elapsedTime / 60);
            Seconds = (int)(elapsedTime % 60);

            minutesText.text = Minutes.ToString("00");
            secondsText.text = Seconds.ToString("00");
        }
    }
}
