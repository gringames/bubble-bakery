using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public bool finishedMoving = false;
    
    [Header("Properties")] [SerializeField]
    private float speed = 0.1f;

    public void Enter(Transform entryPoint, Transform targetPoint)
    {
        finishedMoving = false;
        transform.position = entryPoint.position;
        StartCoroutine(nameof(MoveTo), targetPoint);
    }
    
    public void Exit(Transform exitPoint)
    {
        finishedMoving = false;
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

        finishedMoving = true;
    }
}