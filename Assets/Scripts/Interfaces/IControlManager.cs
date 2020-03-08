using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IControlManager
    {
        Constants.ControlType CurrentControlType { get; set; }
        void ResetPlayerPosition();
        void Initialization();
    }
}
