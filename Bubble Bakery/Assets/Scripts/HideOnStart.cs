using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
