using UnityEngine;

namespace Ilumisoft.Hex
{
    public abstract class Savesystem : MonoBehaviour
    {
        public abstract bool HasSavestate();

        public abstract void Load();

        public abstract void Save();

        public abstract void ClearSavestate();
    }
}