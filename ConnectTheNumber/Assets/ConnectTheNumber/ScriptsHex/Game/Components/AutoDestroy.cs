using UnityEngine;

namespace Ilumisoft.Hex
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField]
        float lifeTime = 1f;

        private void Start()
        {
            Destroy(this.gameObject, lifeTime);
        }
    }
}