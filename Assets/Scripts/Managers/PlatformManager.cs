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
            _objectStorage.Platforms[platformIndex].Velocity = CalculateVelocity();
            _objectStorage.Platforms[platformIndex].PlatformGameObject.transform.position = GetTopPosition(_objectStorage.Platforms[platformIndex]);
            _upperPlatform = _objectStorage.Platforms[platformIndex];
        }
        public void StartGenerate()
        {

            _objectStorage.LowerTrigger.gameObject.SetActive(true);
            _upperPlatform = _objectStorage.Platforms[0];
            _objectStorage.Platforms[0].PlatformGameObject.SetActive(true);
            _objectStorage.Platforms[0].PlatformGameObject.transform.position = new Vector3(0, -2, 0);

            for (int i = 1; i < 10; i++)
            {
                GeneratePlatform(i);
            }
        }
        Vector3 GetTopPosition(Platform platform)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-10f, 10f), _upperPlatform.PlatformGameObject.transform.position.y + Random.Range(3f, 5f));

            Vector2 leftPos = new Vector2(spawnPos.x - 2.25f, spawnPos.y);
            //Vector2 leftPosDir = leftPos;
            //leftPosDir.y -= 20;
            Vector3 rightPos = new Vector2(spawnPos.x + 2.25f, spawnPos.y);
            Vector2 rightPosDir = rightPos;
            rightPosDir.y -= 20;

            RaycastHit2D leftHit = Physics2D.Raycast(leftPos, Vector2.down);
            RaycastHit2D rightHit = Physics2D.Raycast(rightPos, rightPosDir);


            if (leftHit.collider != null &&
                leftHit.collider != _objectStorage.LowerTrigger &&
                leftHit.collider != _objectStorage.Player.PlayerCollider2D)
                //leftHit.collider.attachedRigidbody.velocity.magnitude > platform.PlarformRigidbody2D.velocity.magnitude)
            {
                spawnPos = GetTopPosition(platform);
            }

            //if (rightHit.collider != null &&
            //    rightHit.collider != _objectStorage.LowerTrigger &&
            //    rightHit.collider != _objectStorage.Player.PlayerCollider2D &&
            //    rightHit.collider.attachedRigidbody.velocity.magnitude < platform.PlarformRigidbody2D.velocity.magnitude)
            //{
            //    spawnPos = GetTopPosition(platform);
            //}

            //if (rightHit.collider != null && 
            //    rightHit.collider != _objectStorage.LowerTrigger && 
            //    leftHit.collider != _objectStorage.Player.PlayerCollider2D)
            //{
            //    if (leftHit.collider.attachedRigidbody.velocity.Equals(platform.PlarformRigidbody2D.velocity) //equals
            //    {
            //        GetTopPosition(platform);
            //    }
            //}

            return spawnPos;
        }

        Vector2 CalculateVelocity()
        {
            Vector2 velocity = new Vector2(0, Random.Range(-3f, -6f * Time.deltaTime));
            return velocity;
        }
        void MovePlatform()
        {
            foreach (Platform platform in _objectStorage.Platforms)
            {
                //platform.PlarformRigidbody2D.AddForce(platform.Pla, ForceMode2D.Impulse);
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
