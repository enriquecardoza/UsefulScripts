using System.Collections.Generic;
using UnityEngine;

public class MoveBetween : MonoBehaviour
{
    [Header("Data to fill")]
    [SerializeField] [Tooltip("List of the object to follow")]List<Transform> targets;
    [SerializeField] [Tooltip("Check it if you want that the object follow the rotation of the list")] bool followRotation;

    [Header("ShowData")]
    [SerializeField] [Tooltip("Total time to do the movement")] float totalTime;
    [SerializeField] bool movingFordward;
    [SerializeField] bool movingBackward;

    [Header("Target")]
    private Transform currentTarget;
    private int currentPosition = 0;

    [Header("Distances")]
    private float totalDistance;
    private float distanceBetween;
    private float actualDistance;


    void Start()
    {
        for (int i = 0; i < targets.Count - 1; i++)
        {
            totalDistance += Vector3.Distance(targets[i].position, targets[i + 1].position);
        }
    }

    void Update()
    {
        if (movingFordward)
        {
            MoveForward();
        }
        else if (movingBackward)
        {
            MoveBackward();
        }
    }
    /// <summary>
    /// Move and rotate the object following the array
    /// </summary>
    private void MoveForward()
    {
        if (transform.position != currentTarget.position)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, (totalDistance / totalTime) * Time.deltaTime);
            ///Use the distance to get a number between 0 and 1 to make the rotation
            if (followRotation)
            {
                actualDistance = Vector3.Distance(transform.position, currentTarget.position);
                transform.rotation = Quaternion.Lerp(targets[currentPosition - 1].rotation, currentTarget.rotation, Mathf.SmoothStep(1, 0, (actualDistance / distanceBetween)));
            }
        }
        else
        {
            ///Finish the movement when it reach the final object of the array
            if (currentTarget == targets[targets.Count - 1])
            {
                movingFordward = false;
                return;
            }
            ++currentPosition;
            if (followRotation)
            {
                distanceBetween = Vector3.Distance(transform.position, targets[currentPosition].position);
            }
            currentTarget = targets[currentPosition];
        }
    }
    /// <summary>
    /// Move and rotate the object following the array in reverse order
    /// </summary>
    private void MoveBackward()
    {
        if (transform.position != currentTarget.position)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, (totalDistance / totalTime) * Time.deltaTime);

            if (followRotation)
            {
                actualDistance = Vector3.Distance(transform.position, currentTarget.position);
                transform.rotation = Quaternion.Lerp(targets[currentPosition + 1].rotation, currentTarget.rotation, Mathf.SmoothStep(1, 0, (actualDistance / distanceBetween)));
            }
        }
        else
        {
            ///Finish the movement when it reach the first object of the array
            if (currentTarget == targets[0])
            {
                movingFordward = false;
                return;
            }
            --currentPosition;
            if (followRotation)
            {
                distanceBetween = Vector3.Distance(transform.position, targets[currentPosition].position);
            }
            currentTarget = targets[currentPosition];
        }
    }
    /// <summary>
    /// Stop the movement
    /// </summary>
    public void StopMoving()
    {
        movingFordward = false;
        movingBackward = false;
    }
    /// <summary>
    /// Continue the movement following the array
    /// </summary>
    public void ContinueMovingFordwards()
    {
        movingFordward = true;
    }
    /// <summary>
    /// Continue the movement following the array in reverse order
    /// </summary>
    public void ContinueMovingBackward()
    {
        movingBackward = true;
    }
    /// <summary>
    /// Start the movement following the array
    /// </summary>
    /// <param name="newTime">The time to do the movement</param>
    public void StartMovingFordward(int newTime)
    {
        movingBackward = false;
        movingFordward = true;
        totalTime = newTime;
        this.transform.position = targets[0].position;
        if (followRotation)
            this.transform.rotation = targets[0].rotation;
        currentTarget = targets[0];
        currentPosition = 0;
    }
    /// <summary>
    /// Start the movement following the array in reverse order
    /// </summary>
    /// <param name="newTime">The time to do the movement</param>
    public void StartMovingBackwards(int newTime)
    {
        movingBackward = true;
        movingFordward = false;
        totalTime = newTime;
        this.transform.position = targets[targets.Count - 1].position;
        if (followRotation)
            this.transform.rotation = targets[targets.Count - 1].rotation;
        currentTarget = targets[targets.Count - 1];
        currentPosition = targets.Count - 1;
    }
    /// <summary>
    /// Draw the route
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < targets.Count - 1; i++)
        {
            Debug.DrawLine(targets[i].position, targets[i + 1].position, Color.red);

        }
    }
}
