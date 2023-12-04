using UnityEngine;
using UnityEngine.SceneManagement;

namespace Orders
{
    public class Clickable : MonoBehaviour
    {
        [SerializeField] private int sceneIndex;
    
        private void OnMouseDown()
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
