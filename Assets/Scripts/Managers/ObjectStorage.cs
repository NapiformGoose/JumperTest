using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class ObjectStorage : IObjectStorage
    {
        IDictionary<string, GameObject> _prefabs;
        public IList<Platform> Platforms { get; set; }
        public Player Player { get; set; }
        public int Score { get; set; }
        public Collider2D LowerTrigger { get; set; }

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

        Collider2D LoadLowerTrigger()
        {
            return GameObject.Find(Constants.lowerTriggerName).GetComponent<Collider2D>();
        }
        void LoadPrefabs()
        {
            _prefabs.Add("player", Resources.Load(Constants.prefabPath + Constants.playerPrefabName) as GameObject);
            _prefabs.Add("platform", Resources.Load(Constants.prefabPath + Constants.platformPrefabName) as GameObject);
        }

        Player CreatePlayer(string name)
        {
            Player player = new Player
            {
                Name = name,
                Speed = 7f,
                PlayerGameObject = GameObject.Instantiate(_prefabs["player"], new Vector3(0, 0, 0), Quaternion.identity)
            };
            player.PlayerRigidbody2D = player.PlayerGameObject.GetComponent<Rigidbody2D>();
            player.PlayerCollider2D = player.PlayerGameObject.GetComponent<Collider2D>();
            return player;
        }

        Platform CreatePlatform(string name)
        {
            Platform platform = new Platform
            {
                Name = name,
                PlatformGameObject = GameObject.Instantiate(_prefabs["platform"], new Vector3(0, -2, 0), Quaternion.identity)
            };
            platform.PlarformRigidbody2D = platform.PlatformGameObject.GetComponent<Rigidbody2D>();
            platform.PlatformCollider2D = platform.PlatformGameObject.GetComponent<Collider2D>();
            platform.PlatformGameObject.SetActive(false);
            return platform;
        }
        #endregion
    }
}
