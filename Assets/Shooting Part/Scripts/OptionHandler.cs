using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class OptionHandler : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    // [SerializeField] private Button optionButton;
    [SerializeField] private Button closeOptionButton;
    [SerializeField] private Button retryStageButton;
    [SerializeField] private Button retryStageSelectButton;
    [SerializeField] private Button retryMainButton;

    public AudioSource buttonClickSound;
    public AudioSource testButtonClickSound;

    public bool isOptionOn = false;

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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void optionTestButtonClick()
    {
        testButtonClickSound.Play();
    }

}