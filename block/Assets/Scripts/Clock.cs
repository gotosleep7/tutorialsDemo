using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{

    float hourRadius = -30, minuteRadius = -6, secondRadius = -6;

    [SerializeField]
    Transform hour, minute, second;

    private void Awake()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan now = DateTime.Now.TimeOfDay;
        hour.localRotation = Quaternion.Euler(0.0f, 0.0f, hourRadius * (float)now.TotalHours);
        minute.localRotation = Quaternion.Euler(0.0f, 0.0f, minuteRadius * (float)now.TotalMinutes);
        second.localRotation = Quaternion.Euler(0.0f, 0.0f, secondRadius * (float)now.TotalSeconds);
    }
}
