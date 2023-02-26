using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class OptionHandler : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MouseSensitivityPref = "MouseSensitivityPref";
    private int firstPlayInt;
    public Slider mouseSensitivitySlider;
    private float mouseSensitivityFloat;

    [SerializeField] private GameObject optionPanel;
    // [SerializeField] private Button optionButton;
    [SerializeField] private Button closeOptionButton;
    [SerializeField] private Button retryStageButton;
    [SerializeField] private Button returnStageSelectButton;
    [SerializeField] private Button returnMainButton;

    public AudioSource buttonClickSound;
    public AudioSource testButtonClickSound;

    public bool isOptionOn = false;

    public SoundManager soundManager;

    public PlayerAim playerAim;

    void Awake()
    {
        retryStageButton.onClick.AddListener(RetryStage);
        returnStageSelectButton.onClick.AddListener(ReturnStageSelect);
        returnMainButton.onClick.AddListener(ReturnMain);

        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            mouseSensitivityFloat = 0.3f;
            mouseSensitivitySlider.value = mouseSensitivityFloat;
            PlayerPrefs.SetFloat(MouseSensitivityPref, mouseSensitivityFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            mouseSensitivityFloat = PlayerPrefs.GetFloat(MouseSensitivityPref);
            mouseSensitivitySlider.value = mouseSensitivityFloat;

        }
        UpdateMouseSensitivity();
    }
    void Update()
    {
        ToggleOption();
    }

    void ToggleOption()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOptionOn)
            {
                isOptionOn = true;
                OpenOption();
            }
            else if (isOptionOn)
            {
                isOptionOn = false;
                CloseOption();
            }
        }
    }

    public void OpenOption()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        optionPanel.SetActive(true);
        buttonClickSound.Play();
    }

    public void CloseOption()
    {
        optionPanel.SetActive(false);
        buttonClickSound.Play();
        soundManager.SaveSoundSettings();
        SaveMouseSensitivitySettings();
        isOptionOn = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void optionTestButtonClick()
    {
        testButtonClickSound.Play();
    }

    public void RetryStage()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnStageSelect()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene("SingleStageSelect");
    }

    public void ReturnMain()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene("Main");
    }

    public void SaveMouseSensitivitySettings()
    {
        PlayerPrefs.SetFloat(MouseSensitivityPref, mouseSensitivitySlider.value);
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveMouseSensitivitySettings();
        }
    }

    public void UpdateMouseSensitivity()
    {
        playerAim.xAxis.m_MaxSpeed = mouseSensitivitySlider.value * 1000;
        playerAim.yAxis.m_MaxSpeed = mouseSensitivitySlider.value * 1000;
    }

}