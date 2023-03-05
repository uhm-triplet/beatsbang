using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private Button backMainButton;
    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;
    [SerializeField] private Button stage4Button;
    [SerializeField] private GameObject stage1Canvas;
    [SerializeField] private GameObject stage2Canvas;
    [SerializeField] private GameObject stage3Canvas;
    [SerializeField] private GameObject stage4Canvas;
    [SerializeField] private Button stage1StartButton;
    [SerializeField] private Button stage2StartButton;
    [SerializeField] private Button stage3StartButton;
    [SerializeField] private Button stage4StartButton;
    [SerializeField] private TextMeshProUGUI stage1ScoreText;
    [SerializeField] private TextMeshProUGUI stage2ScoreText;
    [SerializeField] private TextMeshProUGUI stage3ScoreText;
    [SerializeField] private TextMeshProUGUI stage4ScoreText;

    private int stage1Score;
    private int stage2Score;
    private int stage3Score;
    private int stage4Score;

    public AudioSource buttonClickSound;


    void Awake()
    {
        // _leftButton.onClick.AddListener(OnLeftButtonClicked);
        backMainButton.onClick.AddListener(ReturnMainMenu);
        DisableButton();
        UpdateScore();

    }

    void UpdateScore()
    {
        stage1ScoreText.text = PlayerPrefs.GetInt("Stage1Clear") == 1 ? "Best Score : \n" + PlayerPrefs.GetInt("Stage1BestScore").ToString() : "Stage Unclear";
        stage2ScoreText.text = PlayerPrefs.GetInt("Stage2Clear") == 1 ? "Best Score : \n" + PlayerPrefs.GetInt("Stage2BestScore").ToString() : "Stage Unclear";
        stage3ScoreText.text = PlayerPrefs.GetInt("Stage3Clear") == 1 ? "Best Score : \n" + PlayerPrefs.GetInt("Stage3BestScore").ToString() : "Stage Unclear";
        stage4ScoreText.text = PlayerPrefs.GetInt("Stage4Clear") == 1 ? "Best Score : \n" + PlayerPrefs.GetInt("Stage4BestScore").ToString() : "Stage Unclear";
    }

    void DisableButton()
    {
        if (PlayerPrefs.GetInt("Stage1Clear") != 1)
        {
            stage2Button.interactable = false;
            stage2Button.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;

        }
        if (PlayerPrefs.GetInt("Stage2Clear") != 1)
        {
            stage3Button.interactable = false;
            stage3Button.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;


        }
        if (PlayerPrefs.GetInt("Stage3Clear") != 1)
        {
            stage4Button.interactable = false;
            stage4Button.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }

    }

    public void ReturnMainMenu()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene("Main");
    }

    private void HideIfClickedOutside(GameObject panel)
    {
        if (Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                Input.mousePosition))
        {
            panel.SetActive(false);
            buttonClickSound.Play();
        }

    }

    public void StageClick(int stage)
    {
        buttonClickSound.Play();
        switch (stage)
        {
            case 1:
                stage1Canvas.SetActive(true);
                break;

            case 2:
                stage2Canvas.SetActive(true);

                break;

            case 3:
                stage3Canvas.SetActive(true);

                break;

            case 4:
                stage4Canvas.SetActive(true);

                break;

            default:
                break;
        }
    }

    public void StageStart(int stage)
    {
        buttonClickSound.Play();
        SceneManager.LoadScene($"Stage{stage}");
    }
    // Update is called once per frame
    void Update()
    {
        HideIfClickedOutside(stage1Canvas);
        HideIfClickedOutside(stage2Canvas);
        HideIfClickedOutside(stage3Canvas);
        HideIfClickedOutside(stage4Canvas);
    }
}
