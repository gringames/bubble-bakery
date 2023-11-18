using System;
using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public Action OnFinishedMoving;
    
    [Header("Properties")] [SerializeField]
    private float speed = 0.1f;

    public void Enter(Transform entryPoint, Transform targetPoint)
    {
        transform.position = entryPoint.position;
        StartCoroutine(nameof(MoveTo), targetPoint);
    }
    
    public void Exit(Transform exitPoint)
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

        OnFinishedMoving?.Invoke();
    }
}