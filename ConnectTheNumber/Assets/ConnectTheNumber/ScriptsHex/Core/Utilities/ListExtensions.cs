using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.Hex
{
    public static class ListExtensions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}