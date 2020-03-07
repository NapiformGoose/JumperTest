using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Models;

namespace Assets.Scripts.Managers
{
    public class PlatformManager : IPlatformManager, IUpdatable
    {
        IUpdateManager _updateManager;
        IObjectStorage _objectStorage;
        Platform _upperPlatform;
        bool isStart = true;

        public PlatformManager(IUpdateManager updateManager, IObjectStorage objectStorage)
        {
            _objectStorage = objectStorage;

            _updateManager = updateManager;
            _updateManager.AddUpdatable(this);
        }
        void GeneratePlatform(int platformIndex)
        {
            _objectStorage.Platforms[platformIndex].PlatformGameObject.SetActive(true);
            _objectStorage.Platforms[platformIndex].PlatformGameObject.transform.position = GetTopPosition();
            _upperPlatform = _objectStorage.Platforms[platformIndex];
        }
        public void StartGenerate()
        {
            _upperPlatform = _objectStorage.Platforms[0];
            _objectStorage.Platforms[0].PlatformGameObject.SetActive(true);

            for (int i = 1; i < 10; i++)
            {
                GeneratePlatform(i);
            }
        }
        Vector3 GetTopPosition()
        {
            Vector2 spawnPos = new Vector2(Random.Range(-10f, 10f), _upperPlatform.PlatformGameObject.transform.position.y + Random.Range(3f, 5f));

            return spawnPos;
        }
        Vector3 GetBesidePosition()
        {
            Vector2 spawnPos = new Vector2(Random.Range(-13f, 13f), _upperPlatform.PlatformGameObject.transform.position.y);

            return spawnPos;
        }
        void MovePlatform()
        {
            foreach (Platform platform in _objectStorage.Platforms)
            {
                platform.PlarformRigidbody2D.AddForce(new Vector2(0, -3f * Time.deltaTime), ForceMode2D.Impulse);
                platform.PlarformRigidbody2D.velocity = new Vector2(0, -3f);
            }
        }
        public void CustomUpdate()
        {
            if (isStart)
            {
                StartGenerate();
                isStart = false;
            }
            MovePlatform();
            for (int i = 0; i < _objectStorage.Platforms.Count; i++)
            {
                if (_objectStorage.LowerTrigger.IsTouching(_objectStorage.Platforms[i].PlatformCollider2D))
                {
                    GeneratePlatform(i);
                }
            }
        }
    }
}
