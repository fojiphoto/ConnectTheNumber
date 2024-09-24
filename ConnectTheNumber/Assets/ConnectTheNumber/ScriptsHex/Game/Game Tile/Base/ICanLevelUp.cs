using UnityEngine.Events;

namespace Ilumisoft.Hex
{
    public interface ICanLevelUp
    {
        int CurrentLevel { get; set; }

        UnityAction OnLevelChanged { get; set; }

        void LevelUp();
    }
}