using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.Hex
{
    public abstract class LevelUpBehaviour : MonoBehaviour, ICanLevelUp
    {
        public abstract int CurrentLevel { get; set; }

        public abstract UnityAction OnLevelChanged { get; set; }

        public abstract void LevelUp();
    }
}