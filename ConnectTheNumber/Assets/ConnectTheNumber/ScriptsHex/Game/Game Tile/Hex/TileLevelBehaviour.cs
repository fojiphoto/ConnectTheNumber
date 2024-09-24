using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Ilumisoft.Hex
{
    public class TileLevelBehaviour : LevelUpBehaviour
    {
        int currentLevel = 0;

        [SerializeField]
        List<Color> levelColors = new List<Color>();

        public override UnityAction OnLevelChanged { get; set; }

        public void SetLevel(int level)
        {
            CurrentLevel = level;
        }

        public override int CurrentLevel
        {
            get => currentLevel;

            set
            {
                currentLevel = value;

                OnLevelChanged?.Invoke();
            }
        }

        public override void LevelUp()
        {
            CurrentLevel++;
        }

        public Color Color
        {
            get
            {
                int index = Mathf.Min(currentLevel, levelColors.Count - 1);

                return levelColors[index];
            }
        }
    }
}