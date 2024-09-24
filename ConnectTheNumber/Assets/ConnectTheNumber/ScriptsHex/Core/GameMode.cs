using System.Collections;
using UnityEngine;

namespace Ilumisoft.Hex
{
    public abstract class GameMode : MonoBehaviour
    {
        public abstract IEnumerator StartGame();

        public abstract IEnumerator RunGame();

        public abstract IEnumerator EndGame();
    }
}