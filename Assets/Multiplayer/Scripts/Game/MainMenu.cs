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

        // Start is called before the first frame update

        public void StartSingleplay()
        {
            SceneManager.LoadScene("SingleGame");
            buttonClickSound.Play();
        }
        public void ExitGame()
        {
            Application.Quit();
            buttonClickSound.Play();
        }

        public void OpenOption()
        {
            optionPanel.SetActive(true);
            buttonClickSound.Play();
        }

        public void CloseOption()
        {
            optionPanel.SetActive(false);
            buttonClickSound.Play();
        }

        public void optionTestButtonClick()
        {
            testButtonClickSound.Play();
        }
        public void StartMultiplay()
        {
            hostButton.gameObject.SetActive(true);
            joinButton.gameObject.SetActive(true);
            buttonClickSound.Play();
            // SceneManager.LoadScene("Lobby");

        }

        public async void OnHostClicked()
        {
            bool succeeded = await GameLobbyManager.Instance.CreateLobby();
            buttonClickSound.Play();
            if (succeeded)
            {
                SceneManager.LoadSceneAsync("Lobby");
            }

        }

        public void OnJoinClicked()
        {
            codeInputField.gameObject.SetActive(true);
            submitCodeButton.gameObject.SetActive(true);
            buttonClickSound.Play();
        }

        public async void OnSubmitCodeClicked()
        {
            string code = codeText.text;
            code = code.Substring(0, code.Length - 1);
            buttonClickSound.Play();
            bool succeeded = await GameLobbyManager.Instance.JoinLobby(code);
            if (succeeded)
            {
                SceneManager.LoadSceneAsync("Lobby");
            }
        }
    }
}