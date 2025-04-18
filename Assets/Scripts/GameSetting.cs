using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    [Header("音量設定")]
    public AudioMixer audioMixer;
    [SerializeField] Slider mainVolumeSlider;
    [SerializeField] TMP_Text mainVolumeText;


    [Header("解析度設定")]
    public TMP_Dropdown resolutionDP;
    public Toggle fullToggle;
    Resolution[] resolutions;
    List<Resolution> fliterResolution;
    // Start is called before the first frame update
    void Start()
    {
        SetResolutionInfo();
        LoadAudioVolume();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetMainVolume() //設定主音量
    {
        float volume = mainVolumeSlider.value;
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
        mainVolumeText.text = $"{Mathf.RoundToInt(volume * 100)}";
        PlayerPrefs.SetFloat("MainVolume", volume);
    }

    public void LoadAudioVolume() //讀取
    {
        mainVolumeSlider.value = PlayerPrefs.GetFloat("MainVolume");
        SetMainVolume();
    }

    //--------------------------解析度設定--------------------------//
    public void SetFullScreen(bool isFullScreen) //設定全螢幕
    {
        Screen.fullScreen = isFullScreen;
        if (!isFullScreen)
        {
            PlayerPrefs.SetInt("FullScreen", 0);
        }
        else
        {
            PlayerPrefs.SetInt("FullScreen", 1);
        }
    }
    //設定解析度，會用在解析度的下拉選單
    void SetResolution(int i)
    {
        Resolution resolution = fliterResolution[i];
        bool isFull;
        if (PlayerPrefs.GetInt("FullScreen") == 1) isFull = true; else isFull = false;
        Screen.SetResolution(resolution.width, resolution.height, isFull);

        //儲存解析度
        PlayerPrefs.SetInt("ScreenResolutionX", resolution.width);
        PlayerPrefs.SetInt("ScreenResolutionY", resolution.height);
    }
    // 設定解析度內容
    void SetResolutionInfo()
    {
        //取得所有解析度,並過濾
        fliterResolution = new List<Resolution>();
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++) //過濾與螢幕相同赫茲的解析度
        {
            float value1 = Mathf.Floor((float)resolutions[i].refreshRateRatio.value);
            float value2 = Mathf.Floor((float)Screen.currentResolution.refreshRateRatio.value);
            if (value1 == value2)
            {
                fliterResolution.Add(resolutions[i]);

            }
        }

        //接解析度轉換成string格式，並新增近DP裡面
        int currnetIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < fliterResolution.Count; i++)
        {
            string option = $"{fliterResolution[i].width}x{fliterResolution[i].height}";
            options.Add(option);

            if (fliterResolution[i].width == PlayerPrefs.GetInt("ScreenResolutionX")
                 && fliterResolution[i].height == PlayerPrefs.GetInt("ScreenResolutionY"))
            {
                currnetIndex = i;
            }
        }
        resolutionDP.ClearOptions();
        resolutionDP.AddOptions(options);
        resolutionDP.onValueChanged.RemoveListener(SetResolution);
        resolutionDP.value = currnetIndex;
        resolutionDP.onValueChanged.AddListener(SetResolution);
        resolutionDP.RefreshShownValue();

        //設定isFull 的Toggle
        if (PlayerPrefs.GetInt("FullScreen") == 1) fullToggle.isOn = true; else fullToggle.isOn = false;
    }
    //--------------------------解析度設定--------------------------//

}
