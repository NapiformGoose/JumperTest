using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Models;

namespace Assets.Scripts.Managers
{
    public class ControlManager : IControlManager, IUpdatable
    {
        IUpdateManager _updateManager;
        IObjectStorage _objectStorage;
        Vector3 _oldPos;
        List<Collider2D> _list;
        ContactFilter2D _contactFilter2D;

        public int CurrentScore { get; set; }
        public ControlType CurrentControlType { get; set; }

        public ControlManager(IUpdateManager updateManager, IObjectStorage objectStorage)
        {
            _updateManager = updateManager;
            _objectStorage = objectStorage;

            _updateManager.AddUpdatable(this);

            _list = new List<Collider2D>();
            _contactFilter2D = new ContactFilter2D();

            CurrentControlType = ControlType.Default;
        }
        public void Initialization()
        {
            _objectStorage.Player.PlayerGameObject.SetActive(true);
            _objectStorage.Player.PlayerGameObject.transform.position = Constants.playerStartPosition;
            _objectStorage.Player.PlayerRigidbody2D.velocity = Constants.playerStartVelocity;
            CurrentScore = 0;
            _oldPos = new Vector3(0, 0, 0);
        }
        bool onGround()
        {
            _objectStorage.Player.PlayerRigidbody2D.OverlapCollider(_contactFilter2D, _list);

            if (_list.Count > 0)
            {
                return true;
            }
            return false;
        }

        void CalculateScore()
        {
            foreach(Platform platform in _objectStorage.Platforms)
            {
                if (_objectStorage.Player.PlayerCollider2D.IsTouching(platform.PlatformCollider2D) && _list.Count > 0)
                {
                    if (_oldPos.y < _list[0].gameObject.transform.position.y)
                    {
                        CurrentScore++;
                    }
                    _oldPos = _list[0].gameObject.transform.position;
                }
            }
        }
        public void CustomUpdate()
        {
            if (CurrentControlType == ControlType.ArrayControl)
            {
                ArrayControl();
            }
            if (CurrentControlType == ControlType.WASDControl || CurrentControlType == ControlType.Default)
            {
                WASDControl();
            }

            if (onGround())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _objectStorage.Player.PlayerRigidbody2D.AddForce(Constants.playerJumpForce, ForceMode2D.Impulse);
                }
            }
            CalculateScore();
        }

        void ArrayControl()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _objectStorage.Player.PlayerRigidbody2D.AddForce(Constants.playerLeftLateralForce, ForceMode2D.Force);
                if (_objectStorage.Player.PlayerRigidbody2D.velocity.x <= -Constants.maxPlayerVelocity)
                {
                    _objectStorage.Player.PlayerRigidbody2D.velocity = new Vector2(-Constants.maxPlayerVelocity, _objectStorage.Player.PlayerRigidbody2D.velocity.y);
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _objectStorage.Player.PlayerRigidbody2D.AddForce(Constants.playerRightLateralForce, ForceMode2D.Force);
                if (_objectStorage.Player.PlayerRigidbody2D.velocity.x >= Constants.maxPlayerVelocity)
                {
                    _objectStorage.Player.PlayerRigidbody2D.velocity = new Vector2(Constants.maxPlayerVelocity, _objectStorage.Player.PlayerRigidbody2D.velocity.y);
                }
            }
        }
        void WASDControl()
        {
            if (Input.GetKey(KeyCode.A))
            {
                _objectStorage.Player.PlayerRigidbody2D.AddForce(Constants.playerLeftLateralForce, ForceMode2D.Force);
                if (_objectStorage.Player.PlayerRigidbody2D.velocity.x <= -Constants.maxPlayerVelocity)
                {
                    _objectStorage.Player.PlayerRigidbody2D.velocity = new Vector2(-Constants.maxPlayerVelocity, _objectStorage.Player.PlayerRigidbody2D.velocity.y);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                _objectStorage.Player.PlayerRigidbody2D.AddForce(Constants.playerRightLateralForce, ForceMode2D.Force);
                if (_objectStorage.Player.PlayerRigidbody2D.velocity.x >= Constants.maxPlayerVelocity)
                {
                    _objectStorage.Player.PlayerRigidbody2D.velocity = new Vector2(Constants.maxPlayerVelocity, _objectStorage.Player.PlayerRigidbody2D.velocity.y);
                }
            }
        }
        public void ResetPlayerPosition()
        {
            _objectStorage.Player.PlayerGameObject.transform.position = Constants.playerStartPosition;
        }
    }
}

