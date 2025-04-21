using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    void Awake()
    {
        if (!PlayerPrefs.HasKey("Initialization"))
        {
            PlayerPrefs.SetInt("Initialization", 1);

            //解析度設定
            Screen.SetResolution(Screen.currentResolution.width,Screen.currentResolution.height,true);
            PlayerPrefs.SetInt("FullScreen", 1);
            
            //音效設定
            PlayerPrefs.SetFloat("MainVolume", 1);
        }
    }
}
