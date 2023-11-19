using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneTo(int index)
    {
        SceneManager.LoadScene(index);
    }
}
