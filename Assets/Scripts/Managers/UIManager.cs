using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class UIManager
    {
        IUpdateManager _updateManager;
        IControlManager _controlManager;
        IPlatformManager _platformManager;

        public IDictionary<string, Button> Buttons { get; set; }
        public GameObject MainMenu { get; set; }
        public GameObject OptionsMenu { get; set; }

        public UIManager(IUpdateManager updateManager, IControlManager controlManager, IPlatformManager platformManager)
        {
            _updateManager = updateManager;
            _controlManager = controlManager;
            _platformManager = platformManager;

            Buttons = new Dictionary<string, Button>();
            Buttons.Add("PlayButton", GameObject.Find("PlayButton").GetComponent<Button>());
            Buttons.Add("RestartButton", GameObject.Find("RestartButton").GetComponent<Button>());
            Buttons["RestartButton"].gameObject.SetActive(false);
            Buttons.Add("ContinueButton", GameObject.Find("ContinueButton").GetComponent<Button>());
            Buttons["ContinueButton"].gameObject.SetActive(false);
            Buttons.Add("OptionsButton", GameObject.Find("OptionsButton").GetComponent<Button>());

            Buttons.Add("ExitButton", GameObject.Find("ExitButton").GetComponent<Button>());

            Buttons.Add("WASDControl", GameObject.Find("WASDControl").GetComponent<Button>());
            Buttons.Add("ArrayControl", GameObject.Find("ArrayControl").GetComponent<Button>());
            Buttons.Add("BackButton", GameObject.Find("BackButton").GetComponent<Button>());
                
            Buttons.Add("MenuButton", GameObject.Find("MenuButton").GetComponent<Button>());
            Buttons["MenuButton"].gameObject.SetActive(false);

            MainMenu = GameObject.Find("MainMenu");
            OptionsMenu = GameObject.Find("OptionsMenu");
            OptionsMenu.SetActive(false);

            Buttons["PlayButton"].onClick.AddListener(delegate () { StartLevel(); });
            Buttons["RestartButton"].onClick.AddListener(delegate () { RestartLevel(); });
            Buttons["ContinueButton"].onClick.AddListener(delegate () { ContinueLevel(); });
            Buttons["OptionsButton"].onClick.AddListener(delegate () { OpenOptionsMenu(); });
            Buttons["ExitButton"].onClick.AddListener(delegate () { QuitApplication(); });
            Buttons["WASDControl"].onClick.AddListener(delegate () { SelectWASDControl(); });
            Buttons["ArrayControl"].onClick.AddListener(delegate () { SelectArrayControl(); });
            Buttons["BackButton"].onClick.AddListener(delegate () { BackToMainMenu(); });
            Buttons["MenuButton"].onClick.AddListener(delegate () { OpenMainMenu(); });

        }

        void StartLevel()
        {
            MainMenu.SetActive(false);
            _updateManager.CustomStart();
            _controlManager.Initialization();
            _platformManager.StartGenerate();
            Buttons["MenuButton"].gameObject.SetActive(true);
        }
        void OpenOptionsMenu()
        {
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(true);
        }
        void QuitApplication()
        {
            Application.Quit();
        }
        void BackToMainMenu()
        {
            OptionsMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
        void OpenMainMenu()
        {
            _updateManager.Stop();
            MainMenu.SetActive(true);
            Buttons["PlayButton"].gameObject.SetActive(false);
            Buttons["MenuButton"].gameObject.SetActive(false);
            Buttons["RestartButton"].gameObject.SetActive(true);
            Buttons["ContinueButton"].gameObject.SetActive(true);
        }
        void RestartLevel()
        {
            MainMenu.SetActive(false);
            Buttons["MenuButton"].gameObject.SetActive(true);
            _updateManager.Continue();
            _controlManager.Initialization();
            _platformManager.StartGenerate();
            _controlManager.ResetPlayerPosition();
        }
        void ContinueLevel()
        {
            _updateManager.Continue();
            MainMenu.SetActive(false);
            Buttons["MenuButton"].gameObject.SetActive(true);
        }
        void SelectWASDControl()
        {
            _controlManager.CurrentControlType = Constants.ControlType.WASDControl;
            OptionsMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
        void SelectArrayControl()
        {
            _controlManager.CurrentControlType = Constants.ControlType.ArrayControl;
            OptionsMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
    }
}
