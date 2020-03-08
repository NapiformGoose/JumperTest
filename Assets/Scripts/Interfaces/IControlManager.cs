using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IControlManager
    {
        ControlType CurrentControlType { get; set; }
        int CurrentScore { get; set; }

        void ResetPlayerPosition();
        void Initialization();
    }
}
