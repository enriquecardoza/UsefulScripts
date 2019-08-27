using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MoveBetween : MonoBehaviour
{
    [Header("Data to fill")]
    [SerializeField] [Tooltip("List of the object to follow")]List<Transform> targets;
    [SerializeField] [Tooltip("Check it if you want that the object follow the rotation of the list")] bool followRotation;

    [Header("ShowData")]
    [SerializeField] [Tooltip("Total time to do the movement")] float totalTime;
    [SerializeField] bool movingFordward;
    [SerializeField] bool movingBackward;

    [Header("Position/Objetives")]
    private Transform currentTarget;
    private int currentPosition = 0;

    [Header("Distances")]
    private float totalDistance;
    private float distanceBetween;
    private float actualDistance;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < targets.Count - 1; i++)
        {
            totalDistance += Vector3.Distance(targets[i].position, targets[i + 1].position);
        }


    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    StartMovingFordward(5);
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    StartMovingBackwards(5);
        //}
        if (movingFordward)
        {
            MoveForward();
        }

        if (movingBackward)
        {
            MoveBackward();
        }
    }

    private void MoveForward()
    {
        if (transform.position != currentTarget.position)
        {


            this.transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, (totalDistance / totalTime) * Time.deltaTime);
            if (followRotation)
            {
                actualDistance = Vector3.Distance(transform.position, currentTarget.position);
                transform.rotation = Quaternion.Lerp(targets[currentPosition - 1].rotation, currentTarget.rotation, Mathf.SmoothStep(1, 0, (actualDistance / distanceBetween)));
            }
        }
        else
        {

            //transform.rotation = currentTarget.rotation;
            if (currentTarget == targets[targets.Count - 1])
            {
                movingFordward = false;
                return;
            }

            ++currentPosition;
            if (followRotation)
                distanceBetween = Vector3.Distance(transform.position, targets[currentPosition].position);
            currentTarget = targets[currentPosition];
        }
    }

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
            if (currentTarget == targets[0])
            {
                movingFordward = false;
                return;
            }
            --currentPosition;
            if (followRotation)
                distanceBetween = Vector3.Distance(transform.position, targets[currentPosition].position);
            currentTarget = targets[currentPosition];
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public void StopMoving()
    {
        movingFordward = false;
        movingBackward = false;
    }
    /// <summary>
    /// 
    /// </summary>
    public void ContinueMovingFordwards()
    {
        movingFordward = true;
    }
    /// <summary>
    /// 
    /// </summary>
    public void ContinueMovingBackward()
    {
        movingBackward = true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newTime"></param>
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
    /// 
    /// </summary>
    /// <param name="newTime"></param>
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

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < targets.Count - 1; i++)
        {
            Debug.DrawLine(targets[i].position, targets[i + 1].position, Color.red);

        }
    }
}
