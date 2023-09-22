using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMiddleGate : MonoBehaviour
{
    public GameObject openPosition; // The position the gate should move to when it is open
    public GameObject closedPosition; // The position the gate should move to when it is closed
    public float moveSpeed = 10f; // The speed at which the gate should move
    public float tolerance = 0.1f; // The tolerance to consider the gate has reached the target position

    private Coroutine moveCoroutine;

    public void OpenGate()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToPosition(openPosition.transform.position));
    }

    public void CloseGate()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToPosition(closedPosition.transform.position));
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > tolerance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        moveCoroutine = null;
    }
}