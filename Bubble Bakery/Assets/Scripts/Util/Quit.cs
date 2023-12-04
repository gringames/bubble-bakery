using UnityEngine;

namespace Util
{
    public class Quit : MonoBehaviour
    {
        public void OnMouseDown()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
