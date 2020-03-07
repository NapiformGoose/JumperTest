using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

namespace Assets.Scripts.Interfaces
{
    public interface IObjectStorage
    {
        IList<Platform> Platforms { get; set; }
        Player Player { get; set; }
        int Score { get; set; }
        Collider2D LowerTrigger { get; set; }
        void Initialization(string playerName, string platformName);
    }
}
