using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    private float totalTime = 0;
    private float currentTime;
    private float startIntersitiy;
    public static CameraAction Instance { get; private set; }
    private CinemachineBasicMultiChannelPerlin ChannelPerlin;
    private CinemachineVirtualCamera virtualCam;
    private void Awake()
    {
        Instance = this;
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        ChannelPerlin = virtualCam.GetCinemachineComponent
            <CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeCamera(float intensitiy, float time)
    {
        ChannelPerlin.m_AmplitudeGain = intensitiy;
        ChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0, intensitiy, time);

        totalTime = currentTime = time;
        startIntersitiy = intensitiy;
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0) currentTime = 0;
            ChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startIntersitiy, 0, 1 - currentTime / totalTime);
        }
    }

    /* private IEnumerator ShakeCo(float time)
     {
         yield return new WaitForSeconds(time);
         ChannelPerlin.m_AmplitudeGain = 0;

     }*/
}
