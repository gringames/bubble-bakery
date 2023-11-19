
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clickable : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    
    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
