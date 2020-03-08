using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Interfaces;
using TMPro;

namespace Assets.Scripts.Managers
{
    public class UIManager : IUpdatable
    {
        IUpdateManager _updateManager;
        IControlManager _controlManager;
        IPlatformManager _platformManager;
        IObjectStorage _objectStorage;

        IDictionary<string, Button> _buttons { get; set; }

        GameObject _mainMenu { get; set; }
        GameObject _optionsMenu { get; set; }
        GameObject _gameOverMenu;

        TextMeshProUGUI _currentScore { get; set; }
        TextMeshProUGUI _totalScore;

        public UIManager(IUpdateManager updateManager, IControlManager controlManager, IPlatformManager platformManager, IObjectStorage objectStorage)
        {
            _updateManager = updateManager;
            _controlManager = controlManager;
            _platformManager = platformManager;
            _objectStorage = objectStorage;

            _updateManager.AddUpdatable(this);

            _buttons = new Dictionary<string, Button>();

            _buttons.Add(Constants.playBatton, GameObject.Find(Constants.playBatton).GetComponent<Button>());
            _buttons.Add(Constants.restartButton, GameObject.Find(Constants.restartButton).GetComponent<Button>());
            _buttons.Add(Constants.continueButton, GameObject.Find(Constants.continueButton).GetComponent<Button>());
            _buttons.Add(Constants.optionsButton, GameObject.Find(Constants.optionsButton).GetComponent<Button>());
            _buttons.Add(Constants.menuButton, GameObject.Find(Constants.menuButton).GetComponent<Button>());
            _buttons.Add(Constants.exitButton, GameObject.Find(Constants.exitButton).GetComponent<Button>());
            _buttons.Add(Constants.WASDControl, GameObject.Find(Constants.WASDControl).GetComponent<Button>());
            _buttons.Add(Constants.arrayControl, GameObject.Find(Constants.arrayControl).GetComponent<Button>());
            _buttons.Add(Constants.backButton, GameObject.Find(Constants.backButton).GetComponent<Button>());
                
            _mainMenu = GameObject.Find(Constants.mainMenu);
            _mainMenu.SetActive(false);
            _optionsMenu = GameObject.Find(Constants.optionsMenu);
            _optionsMenu.SetActive(false);
            _currentScore = GameObject.Find(Constants.currentScore).GetComponent<TextMeshProUGUI>();
            _currentScore.gameObject.SetActive(false);
            _gameOverMenu = GameObject.Find(Constants.gameOverMenu);
            _totalScore = GameObject.Find(Constants.totalScore).GetComponent<TextMeshProUGUI>();
            _gameOverMenu.SetActive(false);

            _buttons[Constants.playBatton].onClick.AddListener(delegate () { StartLevel(); });
            _buttons[Constants.restartButton].onClick.AddListener(delegate () { StartLevel(); });
            _buttons[Constants.continueButton].onClick.AddListener(delegate () { ContinueLevel(); });
            _buttons[Constants.optionsButton].onClick.AddListener(delegate () { Open_optionsMenu(); });
            _buttons[Constants.exitButton].onClick.AddListener(delegate () { QuitApplication(); });
            _buttons[Constants.WASDControl].onClick.AddListener(delegate () { SelectWASDControl(); });
            _buttons[Constants.arrayControl].onClick.AddListener(delegate () { SelectArrayControl(); });
            _buttons[Constants.backButton].onClick.AddListener(delegate () { BackTo_mainMenu(); });
            _buttons[Constants.menuButton].onClick.AddListener(delegate () { Open_mainMenu(); });
        }

        public void ShowMainMenu()
        {
            _mainMenu.SetActive(true);

            _optionsMenu.SetActive(false);
            _gameOverMenu.SetActive(false);
            _currentScore.gameObject.SetActive(false);
            _buttons[Constants.menuButton].gameObject.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);
            _buttons[Constants.continueButton].gameObject.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);
        }
        void StartLevel()
        {
            _mainMenu.SetActive(false);
            _gameOverMenu.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);

            _currentScore.gameObject.SetActive(true);
            _currentScore.SetText(Constants.currentScoreText + _controlManager.CurrentScore);
            _buttons[Constants.menuButton].gameObject.SetActive(true);

            _updateManager.CustomStart();
            _controlManager.Initialization();
            _platformManager.StartGenerate();
        }
        void Open_optionsMenu()
        {
            _mainMenu.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);

            _optionsMenu.SetActive(true);
            _buttons[Constants.backButton].gameObject.SetActive(true);
        }
        void QuitApplication()
        {
            Application.Quit();
        }
        void BackTo_mainMenu()
        {
            _optionsMenu.SetActive(false);
            _gameOverMenu.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);

            _mainMenu.SetActive(true);
        }
        void Open_mainMenu()
        {
            _buttons[Constants.playBatton].gameObject.SetActive(false);
            _buttons[Constants.menuButton].gameObject.SetActive(false);

            _mainMenu.SetActive(true);
            _buttons[Constants.restartButton].gameObject.SetActive(true);
            _buttons[Constants.continueButton].gameObject.SetActive(true);

            _updateManager.Stop();
        }

        void ContinueLevel()
        {
            _mainMenu.SetActive(false);
            _buttons[Constants.restartButton].gameObject.SetActive(false);

            _buttons[Constants.menuButton].gameObject.SetActive(true);

            _updateManager.CustomStart();
        }
        void SelectWASDControl()
        {
            _optionsMenu.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);

            _mainMenu.SetActive(true);

            _controlManager.CurrentControlType = ControlType.WASDControl;
        }
        void SelectArrayControl()
        {
            _optionsMenu.SetActive(false);
            _buttons[Constants.backButton].gameObject.SetActive(false);

            _mainMenu.SetActive(true);

            _controlManager.CurrentControlType = ControlType.ArrayControl;
        }
        void UpdateScore()
        {
            _currentScore.SetText(Constants.currentScoreText + _controlManager.CurrentScore);
        }
        void Show_gameOverMenu()
        {
            _gameOverMenu.SetActive(true);
            _buttons[Constants.backButton].gameObject.SetActive(true);

            _totalScore.SetText(Constants.totalScoreText + _controlManager.CurrentScore);
        }
        public void CustomUpdate()
        {
            if(_objectStorage.LowerTrigger.IsTouching(_objectStorage.Player.PlayerCollider2D))
            {
                _buttons[Constants.menuButton].gameObject.SetActive(false);
                Show_gameOverMenu();
                _updateManager.Stop();
                _currentScore.gameObject.SetActive(false);
                _buttons[Constants.restartButton].gameObject.SetActive(true);
                _objectStorage.Player.PlayerGameObject.SetActive(false);
            }
            UpdateScore();
        }
    }
}
