using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
namespace Game
{


    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button hostButton;
        [SerializeField] private Button joinButton;
        [SerializeField] private GameObject optionPanel;
        [SerializeField] private Button optionButton;
        [SerializeField] private Button closeOptionButton;
        [SerializeField] private Button submitCodeButton;
        [SerializeField] private TMP_InputField codeInputField;
        [SerializeField] private TextMeshProUGUI codeText;

        public AudioSource buttonClickSound;
        public AudioSource testButtonClickSound;

        void Awake()
        {

        }

        // Start is called before the first frame update

        public void StartSingleplay()
        {
            buttonClickSound.Play();
            SceneManager.LoadScene("SingleStageSelect");
        }
        public void ExitGame()
        {
            buttonClickSound.Play();
            Application.Quit();
        }

        public void OpenOption()
        {
            buttonClickSound.Play();
            optionPanel.SetActive(true);
        }

        public void CloseOption()
        {
            buttonClickSound.Play();
            optionPanel.SetActive(false);
        }

        public void optionTestButtonClick()
        {
            testButtonClickSound.Play();
        }
        public void StartMultiplay()
        {
            buttonClickSound.Play();
            hostButton.gameObject.SetActive(true);
            joinButton.gameObject.SetActive(true);
            // SceneManager.LoadScene("Lobby");

        }

        // public async void OnHostClicked()
        // {
        //     bool succeeded = await GameLobbyManager.Instance.CreateLobby();
        //     buttonClickSound.Play();
        //     if (succeeded)
        //     {
        //         SceneManager.LoadSceneAsync("Lobby");
        //     }

        // }

        // public void OnJoinClicked()
        // {
        //     codeInputField.gameObject.SetActive(true);
        //     submitCodeButton.gameObject.SetActive(true);
        //     buttonClickSound.Play();
        // }

        // public async void OnSubmitCodeClicked()
        // {
        //     string code = codeText.text;
        //     code = code.Substring(0, code.Length - 1);
        //     buttonClickSound.Play();
        //     bool succeeded = await GameLobbyManager.Instance.JoinLobby(code);
        //     if (succeeded)
        //     {
        //         SceneManager.LoadSceneAsync("Lobby");
        //     }
        // }
    }
}