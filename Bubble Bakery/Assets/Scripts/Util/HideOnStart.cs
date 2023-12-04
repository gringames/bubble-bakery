using UnityEngine;

namespace Util
{
    public class HideOnStart : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}
