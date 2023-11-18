using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform doorEntryPoint;

    private void Start()
    {
        transform.position = doorEntryPoint.position;
    }

    private void Update()
    {
        
    }
}
