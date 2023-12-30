using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMaggot : MonoBehaviour
{
    [SerializeField] float speedNormal = 1f;
    [SerializeField] float speedRush = 2f;
    [SerializeField] LayerMask layerPlayer;
    [SerializeField] LayerMask layerWall;
    [SerializeField] Animator maggotAnimator;
    [SerializeField] SpriteRenderer maggotSpriteRenderer;

    private Vector3 startPosition;
    private Vector3 currentPosition;
    private float totalDistanceMoved = 0f;
    private float maxMoveDistance = 2f;

    List<int> turningAngles = new List<int> { 90, 90, -90, -90, 180 };

    private bool isPlayerDetected = false;
    private bool isRushing = false;

    private void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        if (TargetDetection() != true)
        {
            isPlayerDetected = false;
            if (!isRushing)
            {
                Moving(speedNormal);
            }
        }
        else
        {
            isPlayerDetected = true;
            if (!isRushing)
            {
                StartCoroutine(RushForward(speedRush));
            }
        }

        bool isMovingRight = currentPosition.x > 0;
        maggotAnimator.SetBool("isMovingRight", isMovingRight);

    }

    private void Moving(float speed)
    {
        // Moving Forward
        Vector3 step = transform.right * speed * Time.deltaTime;

        if (WallDetection())
        {
            ChangeDirection();
        }
        else if (!isPlayerDetected && totalDistanceMoved + step.magnitude > maxMoveDistance)
        {
            ChangeDirection();            
        }
        else
        {
            transform.position += step;
            totalDistanceMoved += step.magnitude;
        }
    }

    private void ChangeDirection()
    {
        int randomIndex = Random.Range(0, turningAngles.Count);
        int turningValue = turningAngles[randomIndex];
        transform.Rotate(0f, 0f, turningValue);
        totalDistanceMoved = 0f;

        maxMoveDistance = Random.Range(2f, 5f);
        Debug.Log(maxMoveDistance);
    }

    IEnumerator RushForward(float speed)
    {
        isRushing = true;

        // Calculate the target position
        Vector3 targetPosition = transform.position + transform.right * 5;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            if (WallDetection())
            {
                isRushing = false;
                ChangeDirection();
                yield break;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
        }

        transform.position = targetPosition;
        isRushing = false;
    }

    private bool TargetDetection()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.right;
        float distance = 5f;
        Ray targetRay = new Ray(origin, direction);

        RaycastHit2D rayHit = Physics2D.Raycast(origin, direction, distance, layerPlayer);

        if (rayHit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool WallDetection()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.right;
        float distance = 0.45f;
        Ray targetRay = new Ray(origin, direction);

        RaycastHit2D rayHit = Physics2D.Raycast(origin, direction, distance, layerWall);

        if (rayHit)
        {
            Debug.Log("Wall Detected!");
            Debug.DrawLine(origin, rayHit.point, Color.green);
            return true;
        }
        else
        {
            Debug.DrawLine(origin, origin + direction * distance, Color.red);
            return false;
        }
    }

    /*
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.right;
        float distance = 5f;
        Ray targetRay = new Ray(origin, direction);

        RaycastHit2D rayHit = Physics2D.Raycast(origin, direction, distance, layerPlayer);



        if (rayHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, rayHit.point);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(origin, origin + direction * distance);
        }
    }
    */
}