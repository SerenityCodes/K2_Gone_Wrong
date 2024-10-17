using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace.UI
{
    public class MainMenuInteraction : MonoBehaviour
    {
        public UIDocument mainMenuDocument;

        private VisualElement _mainMenuRoot;
        private VisualElement _joinGameMenuRoot;
        private VisualElement _multiplayerMenuRoot;
        private VisualElement _hostMenuRoot;
        
        // Main Menu Buttons
        private Button _singleplayerButton;
        private Button _multiplayerButton;
        private Button _settingsButton;
        private Button _mainMenuQuitButton;
        // Join Menu Buttons/Fields
        private Button _joinMenuQuitButton;
        private Button _joinGameMenuButton;
        private TextField _joinIpField;
        private TextField _joinPortField;
        // Multiplayer Menu Buttons
        private TextField _playerNameField;
        private Button _joinMenuGameButton;
        private Button _hostMenuGameButton;
        private Button _multiplayerMenuBackButton;
        // Host Menu Buttons
        private TextField _hostPortField;
        private Button _hostGameButton;
        private Button _hostMenuBackButton;
        
        private void Start()
        {
            var root = mainMenuDocument.rootVisualElement;
            _mainMenuRoot = root.Q<VisualElement>("MainMenu");
            _joinGameMenuRoot = root.Q<VisualElement>("JoinGame");
            _multiplayerMenuRoot = root.Q<VisualElement>("MultiplayerMenu");
            _hostMenuRoot = root.Q<VisualElement>("HostSettings");
            // Main Menu Button Registration
            {
                _singleplayerButton = _mainMenuRoot.Q<Button>("SingleplayerButton");
                _singleplayerButton.clicked += OnSinglePlayerClick;
                _multiplayerButton = _mainMenuRoot.Q<Button>("MultiplayerButton");
                _multiplayerButton.clicked += OnMultiplayerClick;
                _settingsButton = _mainMenuRoot.Q<Button>("SettingsButton");
                _settingsButton.clicked += OnSettingsClicked;
                _mainMenuQuitButton = _mainMenuRoot.Q<Button>("QuitButton");
                _mainMenuQuitButton.clicked += OnQuitButtonClick;
            }
            // Multiplayer Menu Registration
            {
                _joinMenuGameButton = _multiplayerMenuRoot.Q<Button>("JoinGameButton");
                _joinMenuGameButton.clicked += OnMultiplayerJoinButtonClick;
                _hostMenuGameButton = _multiplayerMenuRoot.Q<Button>("HostGameButton");
                _hostMenuGameButton.clicked += OnMultiplayerHostButtonClick;
                _multiplayerMenuBackButton = _multiplayerMenuRoot.Q<Button>("BackButton");
                _multiplayerMenuBackButton.clicked += OnMultiplayerBackButtonClick;
            }
            // Join Menu Field Registration
            {
                _joinMenuQuitButton = _joinGameMenuRoot.Q<Button>("BackButton");
                _joinMenuQuitButton.clicked += OnJoinBackButtonClick;
                _joinGameMenuButton = _joinGameMenuRoot.Q<Button>("JoinGameButton");
                _joinGameMenuButton.clicked += OnJoinButtonClick;
                _joinIpField = _joinGameMenuRoot.Q<TextField>("HostIP");
                _joinPortField = _joinGameMenuRoot.Q<TextField>("HostPort");
            }
            // Host Menu Registration
            {
                _hostGameButton = _hostMenuRoot.Q<Button>("HostButton");
                _hostGameButton.clicked += OnHostGameButtonClick;
                _hostMenuBackButton = _hostMenuRoot.Q<Button>("BackButton");
                _hostMenuBackButton.clicked += OnHostMenuBackButtonClick;
            }
        }

        private void LoadLevelScene()
        {
            
        }

        private void OnSinglePlayerClick()
        {
            _mainMenuRoot.visible = false;
            LoadLevelScene();
        }

        private void OnMultiplayerClick()
        {
            _mainMenuRoot.visible = false;
            _multiplayerMenuRoot.visible = true;
        }

        private void OnSettingsClicked()
        {
            Debug.Log("OnSettingsClicked");
        }

        private void OnQuitButtonClick()
        {
            Application.Quit();
        }

        private void OnMultiplayerJoinButtonClick()
        {
            _multiplayerMenuRoot.visible = false;
            _joinGameMenuRoot.visible = true;
        }

        private void OnMultiplayerHostButtonClick()
        {
            _hostMenuRoot.visible = true;
            _multiplayerMenuRoot.visible = false;
        }

        private void OnJoinButtonClick()
        {
            ushort port = Convert.ToUInt16(_joinPortField.value);
            string ip = _joinIpField.value;
            // Make a connection
            // ...
        }

        private void OnMultiplayerBackButtonClick()
        {
            _multiplayerMenuRoot.visible = false;
            _mainMenuRoot.visible = true;
        }

        private void OnHostGameButtonClick()
        {
            _hostMenuRoot.visible = false;
            ushort port = Convert.ToUInt16(_hostPortField.value);
            // Load Scene and start game
        }

        private void OnHostMenuBackButtonClick()
        {
            _hostMenuRoot.visible = false;
            _multiplayerMenuRoot.visible = true;
        }

        private void OnJoinBackButtonClick()
        {
            _joinGameMenuRoot.visible = false;
            _multiplayerMenuRoot.visible = true;
        }
    }
}