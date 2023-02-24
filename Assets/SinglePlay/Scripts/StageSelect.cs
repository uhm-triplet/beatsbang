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


    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main");
        buttonClickSound.Play();
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
    // Update is called once per frame
    void Update()
    {
        HideIfClickedOutside(stage1Canvas);
        HideIfClickedOutside(stage2Canvas);
        HideIfClickedOutside(stage3Canvas);
        HideIfClickedOutside(stage4Canvas);
    }
}
