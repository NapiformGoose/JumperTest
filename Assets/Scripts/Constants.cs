using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Constants
    {
        public static string platformPrefabName = "Platform";
        public static string playerPrefabName = "Player";
        public static string prefabPath = "Prefabs/";
        public static string lowerTriggerName = "LowerTrigger";
        public static int platformCount = 10;
        public enum ControlType { ArrayControl, WASDControl, Default};

    }
}
