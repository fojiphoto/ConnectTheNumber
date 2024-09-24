using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.Hex
{
    public static class Highscore
    {

        static readonly string key = "Highscore";

        public static int Value
        {
            get => ReadValue();
            set => SetValue(value);
        }

        static void SetValue(int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        static int ReadValue()
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }
            else
            {
                return 0;
            }
        }

        
    }
}