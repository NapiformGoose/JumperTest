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

        //Game logic: platforms
        public static int platformCount = 10;
        public static int countSlowPlatform = 6;
        public static Vector3 firstPlatformPosition = new Vector3(0, -2, 0);
        public static Vector2 slowPlatformVelocity = new Vector2(0, Random.Range(-3f, -4f));
        public static Vector2 fastPlatformVelocity = new Vector2(0, -6f);

        //Game logic: player
        public static float playerSpeed = 7f;
        public static Vector3 playerStartPosition = new Vector3(0, 0, 0);
        public static Vector2 playerStartVelocity= new Vector2(0, 0);
        public static Vector2 playerJumpForce = new Vector2(0, 340f);
        public static Vector2 playerLeftLateralForce = new Vector2(-280f, 0);
        public static Vector2 playerRightLateralForce = new Vector2(280f, 0);
        public static float maxPlayerVelocity = 10f;



        //Buttons
        public static string playBatton = "PlayButton";
        public static string restartButton = "RestartButton";
        public static string continueButton = "ContinueButton";
        public static string optionsButton = "OptionsButton";
        public static string menuButton = "MenuButton";
        public static string exitButton = "ExitButton";
        public static string WASDControl = "WASDControl";
        public static string arrayControl = "ArrayControl";
        public static string backButton = "BackButton";

        //UIContainers
        public static string mainMenu = "MainMenu";
        public static string optionsMenu = "OptionsMenu";
        public static string gameOverMenu = "GameOverMenu";

        //Score UIElement
        public static string currentScore = "CurrentScore";
        public static string totalScore = "TotalScore";
        //Score text
        public static string currentScoreText = "Score: ";
        public static string totalScoreText = "Total score: ";


    }
}
