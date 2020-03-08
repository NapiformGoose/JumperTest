using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Bootstrapper : MonoBehaviour
    {
        IUpdateManager _updateManager;
        IControlManager _controlManager;
        IPlatformManager _platformManager;
        IObjectStorage _objectStorage;
        UIManager _UIManager;
        void Start()
        {
            var updateManagerObject = new GameObject("UpdateManager");
            _updateManager = updateManagerObject.AddComponent<UpdateManager>();

            _objectStorage = new ObjectStorage(Constants.platformCount);
            _objectStorage.Initialization(Constants.playerPrefabName, Constants.platformPrefabName);
            _controlManager = new ControlManager(_updateManager, _objectStorage);

            _platformManager = new PlatformManager(_updateManager, _objectStorage);

            _UIManager = new UIManager(_updateManager, _controlManager, _platformManager, _objectStorage);
            _UIManager.ShowMainMenu();
        }
        
    }
}


