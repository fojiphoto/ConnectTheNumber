using System.Collections;
using UnityEngine;

namespace Ilumisoft.Hex
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        GameMode gameMode = null;

        private void Awake()
        {
            // Adjusting vsync and framerate settings for smooth gameplay
            Application.targetFrameRate = 60;
            
            //QualitySettings.vSyncCount = 0;
        }

        IEnumerator Start()
        {
            // Start the game
            yield return gameMode.StartGame();

            // Run the game and wait until it is over
            yield return gameMode.RunGame();

            // Finish the game
            yield return gameMode.EndGame();
        }
    }
}