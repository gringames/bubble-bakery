using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private int nextSceneIndex = 2;

        public static void ChangeSceneTo(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void ChangeScene()
        {
            ChangeSceneTo(nextSceneIndex);
        }
    }
}