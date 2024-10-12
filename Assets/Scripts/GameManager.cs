using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public GameObject PlayerPickupSocket;
        public GameObject Player;

        private bool _playerHasItem;

        public GameObject PowerGameObject;
        public GameObject COExtractorGameObject;
        public Material PowerOffTexture;
        public Material PowerOnTexture;

        public GameObject COCanisterPrefab;
        public GameObject CO2CanisterPrefab;

        public GameObject CO2ContainerObject;
        public GameObject OxygenTankObject;
        
        // Game State
        private bool _isPowerActive;
        private bool _isCo2Deposited;
        private bool _isCoDeposited;
        private bool _isPlayerDead;
        
        // Audio Sources
        public AudioClip BackgroundAudioSource;
        public AudioClip Co2DepositedAudioSource;
        public AudioClip CoExtractedAudioSource;
        public AudioClip CoDepositedAudioSource;
        public AudioClip PowerAudioSource;
        public AudioClip WinAudioSource;
        
        // UI Elements
        public UIDocument winMenuDocument;
        public UIDocument deathMenuDocument;
        public UIDocument settingsMenuDocument;
        private Button _startButton;
        private Button _settingsButton;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            _isPowerActive = false;
            _isCo2Deposited = false;
            _isCoDeposited = false;
            CO2ContainerObject.GetComponent<CrateInteraction>().ChangeMessage();
            var source = Player.GetComponent<AudioSource>();
            source.clip = BackgroundAudioSource;
            source.loop = true;
            source.Play();
        }

        public void TurnOnPower()
        {
            PowerGameObject.GetComponent<Renderer>().material = PowerOnTexture;
            _isPowerActive = true;
            var source = PowerGameObject.GetComponent<AudioSource>();
            source.clip = PowerAudioSource;
            source.loop = false;
            source.Play();
            var interactable = PowerGameObject.GetComponent<Interactable>();
            interactable.message = "Power Enabled!";
            interactable.showKeyBind = false;
            COExtractorGameObject.GetComponent<GeneratorInteract>().ChangeMessage();
            CO2ContainerObject.GetComponent<CrateInteraction>().ChangeMessage();
            CheckWin();
        }

        public void GivePlayerCOCanister()
        {
            if (_playerHasItem || _isCoDeposited || !_isPowerActive)
            {
                return;
            }
            _isCoDeposited = true;
            Instantiate(COCanisterPrefab, PlayerPickupSocket.transform);
            PlayAudio(Player, CoExtractedAudioSource);
            _playerHasItem = true;
        }

        public void GivePlayerCO2Canister()
        {
            if (_playerHasItem || _isCo2Deposited || !_isPowerActive)
            {
                // Play failure noise
                return;
            }
            _isCo2Deposited = true;
            Instantiate(CO2CanisterPrefab, PlayerPickupSocket.transform);
            PlayAudio(Player, CoExtractedAudioSource);
            _playerHasItem = true;
        }

        public void KillPlayer()
        {
            if (!_isPlayerDead)
            {
                _isPlayerDead = true;
            }
        }

        public void PlayAudio(GameObject sourceObject, AudioClip clip)
        {
            var source = sourceObject.GetComponent<AudioSource>();
            source.clip = clip;
            source.loop = false;
            source.Play();
        }

        public void DepositCanister()
        {
            if (!_playerHasItem) return;
            _playerHasItem = false;
            PlayAudio(Player, CoDepositedAudioSource);
            Destroy(PlayerPickupSocket.transform.GetChild(0).gameObject);
            CheckWin();
        }

        public bool IsPowerActive()
        {
            return _isPowerActive;
        }

        public void CheckWin()
        {
            if (_isCoDeposited && _isCo2Deposited && _isPowerActive)
            {
                Destroy(OxygenTankObject);
                PlayAudio(Player, WinAudioSource);
            }
        }
    }
}