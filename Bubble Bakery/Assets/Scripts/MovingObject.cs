using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [Header("Points")] [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Transform exitPoint;

    [Header("Properties")] [SerializeField]
    private float speed = 0.1f;

    public void Enter()
    {
        transform.position = entryPoint.position;
        StartCoroutine(nameof(MoveTo), targetPoint);
    }
    
    public void Exit()
    {
        StartCoroutine(nameof(MoveTo), exitPoint);
    }

    private IEnumerator MoveTo(Transform end)
    {
        var endPoint = end.position;

        while (transform.position != endPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed);
            yield return new WaitForFixedUpdate();
        }
    }
}