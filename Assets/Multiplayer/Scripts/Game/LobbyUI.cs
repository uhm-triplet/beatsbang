using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BeatsBang.Core.Data;
using Game.Events;

namespace Game
{


    public class LobbyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lobbyCodeText;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _readyButton;
        [SerializeField] private Image _mapImage;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private TextMeshProUGUI _mapName;
        [SerializeField] private TextMeshProUGUI _songName;
        [SerializeField] private MapSelectionData _mapSelectionData;

        private int _currentMapIndex = 0;

        private void OnEnable()
        {
            if (GameLobbyManager.Instance.IsHost)
            {
                _leftButton.onClick.AddListener(OnLeftButtonClicked);
                _rightButton.onClick.AddListener(OnRightButtonClicked);
                _startButton.onClick.AddListener(OnStartButtonClicked);
                Events.LobbyEvents.OnLobbyReady += OnLobbyReady;
            }
            _readyButton.onClick.AddListener(OnReadyPressed);


            LobbyEvents.OnLobbyUpdated += OnLobbyUpdated;
        }

        private void OnDisable()
        {

            _leftButton.onClick.RemoveAllListeners();
            _rightButton.onClick.RemoveAllListeners();
            _readyButton.onClick.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();

            Events.LobbyEvents.OnLobbyReady -= OnLobbyReady;
            LobbyEvents.OnLobbyUpdated -= OnLobbyUpdated;

        }

        void Start()
        {
            _lobbyCodeText.text = $"Lobby Code: {GameLobbyManager.Instance.GetLobbyCode()}";

            if (!GameLobbyManager.Instance.IsHost)
            {
                _leftButton.gameObject.SetActive(false);
                _rightButton.gameObject.SetActive(false);
            }
            else
            {
                GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex, _mapSelectionData.Maps[_currentMapIndex].SceneName);

            }
        }

        private async void OnLeftButtonClicked()
        {
            if (_currentMapIndex - 1 >= 0)
            {
                _currentMapIndex--;
            }
            else
            {
                _currentMapIndex = _mapSelectionData.Maps.Count - 1;
            }

            UpdateMap();
            GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex, _mapSelectionData.Maps[_currentMapIndex].SceneName);
        }

        private async void OnRightButtonClicked()
        {
            if (_currentMapIndex + 1 < _mapSelectionData.Maps.Count)
            {
                _currentMapIndex++;
            }
            else
            {
                _currentMapIndex = 0;
            }

            UpdateMap();
            GameLobbyManager.Instance.SetSelectedMap(_currentMapIndex, _mapSelectionData.Maps[_currentMapIndex].SceneName);
        }

        public async void OnReadyPressed()
        {
            bool succeed = await GameLobbyManager.Instance.SetPlayerReady();
            if (succeed)
            {
                _readyButton.gameObject.SetActive(false);
            }
        }

        private void UpdateMap()
        {
            _mapImage.color = _mapSelectionData.Maps[_currentMapIndex].MapThunbnail;
            _mapName.text = _mapSelectionData.Maps[_currentMapIndex].MapName;
            _songName.text = _mapSelectionData.Maps[_currentMapIndex].SongName;
        }

        private void OnLobbyUpdated()
        {
            _currentMapIndex = GameLobbyManager.Instance.GetMapIndex();
            UpdateMap();
        }

        private void OnLobbyReady()
        {
            _startButton.gameObject.SetActive(true);
        }


        private async void OnStartButtonClicked()
        {
            await GameLobbyManager.Instance.StartGame();
        }


    }
}
