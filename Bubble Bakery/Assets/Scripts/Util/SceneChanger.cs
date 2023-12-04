using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class SceneChanger : MonoBehaviour
    {
        public void ChangeSceneTo(int index)
        {
            SceneManager.LoadScene(index);
        }


        public void EndGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}