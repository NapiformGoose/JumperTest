using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public enum ControlType { ArrayControl, WASDControl, Default };
    public interface IPlatformManager
    {
        void StartGenerate();
    }
}
