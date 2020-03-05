using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : IUpdatable
{
    IUpdateManager _updateManager;
    Player _player;
    Platform _platform;
    Rigidbody2D _rigidbody;

    List<Collider2D> list = new List<Collider2D>();
    ContactFilter2D contactFilter2D = new ContactFilter2D();
    bool isMove = false;
    public ControlManager(IUpdateManager updateManager, Player player, Platform platform)
    {
        _updateManager = updateManager;
        _player = player;
        _platform = platform;

        _updateManager.AddUpdatable(this);
        _player.PlayerGameObject = GameObject.Instantiate(_player.PlayerGameObject);
        _platform.PlatformGameObject = GameObject.Instantiate(_platform.PlatformGameObject);
        _rigidbody = _player.PlayerGameObject.GetComponent<Rigidbody2D>();
    }

    bool onGround()
    {
        _rigidbody.OverlapCollider(contactFilter2D, list);

        if (list.Count > 0)
        {
            isMove = false;
            return true;
        }
        return false;
    }
    public void CustomUpdate()
    {
        if (onGround())
        {
            if (Input.GetKey(KeyCode.A))
            {
                _player.PlayerGameObject.transform.Translate(-0.1f, 0, 0, Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _player.PlayerGameObject.transform.Translate(0.1f, 0, 0, Space.World);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.AddForce(new Vector2(0, 15f), ForceMode2D.Impulse);
                isMove = true;
            }
        }
        if (isMove)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody.AddForce(new Vector2(-5f, 0), ForceMode2D.Impulse);
                isMove = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody.AddForce(new Vector2(5f, 0), ForceMode2D.Impulse);
                isMove = false;
            }
        }
    }
}

