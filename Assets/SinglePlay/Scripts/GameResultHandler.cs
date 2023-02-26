using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameResultHandler : MonoBehaviour
{
    [SerializeField] private Button nextStageButton;
    [SerializeField] private Button retryStageButton;
    [SerializeField] private Button returnStageSelectButton;
    [SerializeField] private Button returnMainButton;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [SerializeField] private TextMeshProUGUI scoreText;

    public AudioSource buttonClickSound;
    public SoundManager soundManager;
    public GameManager gameManager;

    void Awake()
    {
        if (nextStageButton != null)
            nextStageButton.onClick.AddListener(NextStage);
        retryStageButton.onClick.AddListener(RetryStage);
        returnStageSelectButton.onClick.AddListener(ReturnStageSelect);
        returnMainButton.onClick.AddListener(ReturnMain);
    }

    public void NextStage()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene($"Stage{gameManager.stage + 1}");
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
}
