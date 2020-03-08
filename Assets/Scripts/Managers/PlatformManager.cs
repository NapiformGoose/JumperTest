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
        int _slowPlatform;
        public PlatformManager(IUpdateManager updateManager, IObjectStorage objectStorage)
        {
            _objectStorage = objectStorage;

            _updateManager = updateManager;
            _updateManager.AddUpdatable(this);

            _slowPlatform = Constants.countSlowPlatform;
        }
        void GeneratePlatform(int platformIndex)
        {
            _objectStorage.Platforms[platformIndex].PlatformGameObject.SetActive(true);
            _objectStorage.Platforms[platformIndex].Velocity = CalculateVelocity();

            Vector2 spawnPos = new Vector2(Random.Range(-10f, 10f), _upperPlatform.PlatformGameObject.transform.position.y + Random.Range(2f, 3f));
            _objectStorage.Platforms[platformIndex].PlatformGameObject.transform.position = spawnPos;

            _upperPlatform = _objectStorage.Platforms[platformIndex];
        }
        public void StartGenerate()
        {
            if(_objectStorage.Platforms.Count > 0)
            {
                _objectStorage.LowerTrigger.gameObject.SetActive(true);
                _upperPlatform = _objectStorage.Platforms[0];
                _objectStorage.Platforms[0].PlatformGameObject.SetActive(true);
                _objectStorage.Platforms[0].PlatformGameObject.transform.position = Constants.firstPlatformPosition;
                _objectStorage.Platforms[0].Velocity = Constants.slowPlatformVelocity;
                for (int i = 1; i < Constants.platformCount; i++)
                {
                    GeneratePlatform(i);
                }
            }
        }

        Vector2 CalculateVelocity()
        {
            Vector2 velocity;
            if (_slowPlatform == 0)
            {
                velocity = Constants.slowPlatformVelocity;
                _slowPlatform = Constants.countSlowPlatform;
            }
            else
            {
                velocity = Constants.fastPlatformVelocity;
                _slowPlatform--;
            }
            return velocity;
        }

        void MovePlatform()
        {
            foreach (Platform platform in _objectStorage.Platforms)
            {
                platform.PlarformRigidbody2D.velocity = platform.Velocity;
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
