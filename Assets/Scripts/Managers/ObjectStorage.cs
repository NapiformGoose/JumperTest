using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.Interfaces;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ObjectStorage : IObjectStorage
    {
        IDictionary<string, GameObject> _prefabs;
        public IList<Platform> Platforms { get; set; }
        public Player Player { get; set; }
        public int Score { get; set; }
        public Collider2D LowerTrigger { get; set; }

        public IList<Button> Buttons { get; set; }

        public ObjectStorage(int platformCount)
        {
            Platforms = new List<Platform>(platformCount);
            _prefabs = new Dictionary<string, GameObject>();
        }

        public void Initialization(string playerName, string platformName)
        {
            LowerTrigger = LoadLowerTrigger();
            LoadPrefabs();

            Player = CreatePlayer(playerName);
            for (int i = 0; i < Constants.platformCount; i++)
            {
                Platforms.Add(CreatePlatform(platformName));
            }
        }

        #region LoadData

        //пока нет методов для обработки/преобразования данных хранилища, функционал загрузки данных находится здесь

        Collider2D LoadLowerTrigger()
        {
            GameObject lowerTriggerGameObject = GameObject.Find(Constants.lowerTriggerName);
            lowerTriggerGameObject.SetActive(false);
            
            return lowerTriggerGameObject.GetComponent<Collider2D>();
        }
        void LoadPrefabs()
        {
            _prefabs.Add(Constants.playerPrefabName, Resources.Load(Constants.prefabPath + Constants.playerPrefabName) as GameObject);
            _prefabs.Add(Constants.platformPrefabName, Resources.Load(Constants.prefabPath + Constants.platformPrefabName) as GameObject);
        }

        Player CreatePlayer(string name)
        {
            Player player = new Player
            {
                Name = name,
                Speed = Constants.playerSpeed,
                PlayerGameObject = GameObject.Instantiate(_prefabs[Constants.playerPrefabName])
            };
            player.PlayerRigidbody2D = player.PlayerGameObject.GetComponent<Rigidbody2D>();
            player.PlayerCollider2D = player.PlayerGameObject.GetComponent<Collider2D>();
            player.PlayerGameObject.SetActive(false);
            return player;
        }

        Platform CreatePlatform(string name)
        {
            Platform platform = new Platform
            {
                Name = name,
                PlatformGameObject = GameObject.Instantiate(_prefabs[Constants.platformPrefabName])
            };
            platform.PlarformRigidbody2D = platform.PlatformGameObject.GetComponent<Rigidbody2D>();
            platform.PlatformCollider2D = platform.PlatformGameObject.GetComponent<Collider2D>();
            platform.PlatformGameObject.SetActive(false);
            return platform;
        }
        #endregion
    }
}
