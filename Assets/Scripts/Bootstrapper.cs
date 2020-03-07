using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts
{
    public class Bootstrapper : MonoBehaviour
    {
        IUpdateManager _updateManager;
        ControlManager _controlManager;
        IPlatformManager _platformManager;
        IObjectStorage _objectStorage;
        void Start()
        {
            var updateManagerObject = new GameObject("UpdateManager");
            _updateManager = updateManagerObject.AddComponent<UpdateManager>();

            _objectStorage = new ObjectStorage(Constants.platformCount);
            _objectStorage.Initialization(Constants.playerPrefabName, Constants.platformPrefabName);
            _controlManager = new ControlManager(_updateManager, _objectStorage);

            _platformManager = new PlatformManager(_updateManager, _objectStorage);
            _platformManager.StartGenerate();
            _updateManager.Start();
        }
    }
}


