using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [Header("Points")] [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform targetPoint;

    [Header("Properties")] [SerializeField]
    private float speed = 0.1f;

    private void Start()
    {
        transform.position = entryPoint.position;
        MoveToTarget();
    }


    private void MoveToTarget()
    {
        StartCoroutine(nameof(MoveToTargetPoint));
    }

    private IEnumerator MoveToTargetPoint()
    {
        var endPoint = targetPoint.position;

        while (transform.position != endPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed);
            yield return new WaitForFixedUpdate();
        }
    }
}