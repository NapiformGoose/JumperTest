using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class ControlManager : IControlManager, IUpdatable
    {
        IUpdateManager _updateManager;
        IObjectStorage _objectStorage;
        
        List<Collider2D> _list;
        ContactFilter2D _contactFilter2D;

        public Constants.ControlType CurrentControlType { get; set; }
        public ControlManager(IUpdateManager updateManager, IObjectStorage objectStorage)
        {
            _updateManager = updateManager;
            _objectStorage = objectStorage;

            _updateManager.AddUpdatable(this);

            _list = new List<Collider2D>();
            _contactFilter2D = new ContactFilter2D();

            CurrentControlType = Constants.ControlType.Default;
        }
        public void Initialization()
        {
            _objectStorage.Player.PlayerGameObject.SetActive(true);
            _objectStorage.Player.PlayerGameObject.transform.position = new Vector3(0, 0, 0);
            _objectStorage.Player.PlayerRigidbody2D.velocity = new Vector2(0, 0);
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

        public void CustomUpdate()
        {
            if (CurrentControlType == Constants.ControlType.ArrayControl)
            {
                ArrayControl();
            }
            if (CurrentControlType == Constants.ControlType.WASDControl || CurrentControlType == Constants.ControlType.Default)
            {
                WASDControl();
            }

            if (onGround())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _objectStorage.Player.PlayerRigidbody2D.AddForce(new Vector2(0, 300f), ForceMode2D.Impulse);
                }
            }
        }
        void ArrayControl()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _objectStorage.Player.PlayerRigidbody2D.AddForce(new Vector2(-280, 0), ForceMode2D.Force);
                if (_objectStorage.Player.PlayerRigidbody2D.velocity.x <= -10)
                {
                    _objectStorage.Player.PlayerRigidbody2D.velocity = new Vector2(-10, _objectStorage.Player.PlayerRigidbody2D.velocity.y);
                }
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _objectStorage.Player.PlayerRigidbody2D.AddForce(new Vector2(280, 0), ForceMode2D.Force);
                if (_objectStorage.Player.PlayerRigidbody2D.velocity.x >= 10)
                {
                    _objectStorage.Player.PlayerRigidbody2D.velocity = new Vector2(10, _objectStorage.Player.PlayerRigidbody2D.velocity.y);
                }
            }
        }
        void WASDControl()
        {
            if (Input.GetKey(KeyCode.A))
            {
                _objectStorage.Player.PlayerRigidbody2D.AddForce(new Vector2(-280, 0), ForceMode2D.Force);
                if (_objectStorage.Player.PlayerRigidbody2D.velocity.x <= -10)
                {
                    _objectStorage.Player.PlayerRigidbody2D.velocity = new Vector2(-10, _objectStorage.Player.PlayerRigidbody2D.velocity.y);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                _objectStorage.Player.PlayerRigidbody2D.AddForce(new Vector2(280, 0), ForceMode2D.Force);
                if (_objectStorage.Player.PlayerRigidbody2D.velocity.x >= 10)
                {
                    _objectStorage.Player.PlayerRigidbody2D.velocity = new Vector2(10, _objectStorage.Player.PlayerRigidbody2D.velocity.y);
                }
            }
        }
        public void ResetPlayerPosition()
        {
            _objectStorage.Player.PlayerGameObject.transform.position = new Vector3(0, 0, 0);
        }
    }
}

