using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeLeft;//variable de tiempo
    private bool timerOn; //Un bool que usaremos para chequear
    private int timeShow;

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Se acabo el tiempo");
                timeLeft = 0;
                timerOn = false;
            }

            timeShow = (int)timeLeft;
            Debug.Log("Tiempo actual: " + timeShow);
        }
    }
}
