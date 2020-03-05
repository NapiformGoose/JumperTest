using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    IUpdateManager _updateManager;
    ControlManager _controlManager;
    void Start()
    {
        var updateManagerObject = new GameObject("UpdateManager");
        _updateManager = updateManagerObject.AddComponent<UpdateManager>();
        Player player = new Player();
        player.Speed = 7f;
        player.PlayerGameObject = Resources.Load("Prefabs/Player") as GameObject;

        Platform platform = new Platform();
        platform.PlatformGameObject = Resources.Load("Prefabs/Platform") as GameObject;
        _controlManager = new ControlManager(_updateManager, player, platform);

        _updateManager.Start();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Box went through!");
    }
}


