using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnitchLogic : MonoBehaviour
{

    public enum State
    {
        Roam = 1,
        Moving = 2,
        Vertical = 3,
        Run = 4
    }
    public State state;

    public Transform[] targets;
    private bool setNewPosition;
    //private Transform target;
    [SerializeField] private GameObject target;

    private float verticalTimer = 0f, sleepTimer = 0f, maxDeltaY = 20f, rand, randSleep;


    private AudioSource audioSource;
    public AudioClip whirrLoop;

    private Rigidbody rb;
    private Vector3 direction, targetPosition, verticalCenter;

    [SerializeField]
    float posY = 10f, verticalRadius =1f, rotationRadius = 370f, angularSpeed = 3f, ovalWidth = 2.5f, movementSpeed = 50f, maxSleepTime = 6f, maxPosY = 300f, minPosY = 5f;
    [SerializeField]
    Transform rotationCenter;

    private float currY, deltaY, posX, posZ, angle = 0f;


    private float targetTime;

    void Start()
    {
        targetTime = TargetsSpawnArea.TargetManager(target, 0f);
        //rb = GetComponent<Rigidbody>();
        //state = State.Roam;
        //audioSource = GetComponent<AudioSource>();
        //audioSource.clip = whirrLoop;
        //audioSource.loop = true;
        //audioSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
        targetTime = TargetsSpawnArea.TargetManager(target, targetTime);

        //Vector3 currentPosition = transform.position;
        //currentPosition.y = Mathf.Clamp(currentPosition.y, 0f, maxPosY);


        //if (state == State.Roam && Random.Range(0, (1000 - (int)(verticalTimer * 10))) == 0)
        //{
        //    StartVertical();
        //}
        //if (state == State.Roam)
        //{
        //    if (sleepTimer < randSleep)
        //    {
        //        sleepTimer += Time.deltaTime;
        //    }
        //    else
        //    {
        //        if (setNewPosition)
        //        {
        //            rand = Random.value;
        //            posX = (rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius / ovalWidth) * rand;
        //            posZ = (rotationCenter.position.z + Mathf.Sin(angle) * rotationRadius) * rand;
        //            targetPosition = new Vector3(posX, posY, posZ);
        //            angle += Time.deltaTime * movementSpeed / ovalWidth;
        //            if (angle > Mathf.PI * 2f)
        //            {
        //                angle -= Mathf.PI * 2f;
        //            }
        //            setNewPosition = false;
        //        }
        //        direction = targetPosition - transform.position;
        //        direction.Normalize();
        //        rb.velocity = direction * movementSpeed;
        //        if (direction != Vector3.zero)
        //        {
        //            Quaternion targetRotation = Quaternion.LookRotation(direction);
        //            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);
        //        }
        //        if (Vector3.Distance(targetPosition, transform.position) < 3f)
        //        {
        //            rand = Random.value;
        //            if (rand < 0.5f) //50/50 
        //                StartVertical();
        //            else
        //                StartRoam();
        //        }

        //    }
        //}
        //else if (state == State.Vertical)
        //{
        //    posX = verticalCenter.x + Mathf.Cos(angle) * verticalRadius;
        //    posZ = verticalCenter.z + Mathf.Sin(angle) * verticalRadius;
        //    if (currY < posY)
        //        currY += 1f;
        //    else
        //        currY -= 1f;
        //    targetPosition = new Vector3(posX, currY, posZ);
        //    angle += Time.deltaTime * angularSpeed / verticalRadius;
        //    if (angle > Mathf.PI * 2f)
        //    {
        //        angle -= Mathf.PI * 2f;
        //    }
        //    direction = targetPosition - transform.position;
        //    rb.velocity = direction * movementSpeed;
        //    if (direction != Vector3.zero)
        //    {
        //        Quaternion targetRotation = Quaternion.LookRotation(direction);
        //        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);
        //    }
        //    if (transform.position.y - currY < 5f)
        //        StartRoam();
        //}
    }

    private void StartRoam()
    {
        state = State.Roam;
        sleepTimer = 0;
        randSleep = Random.Range(0, maxSleepTime);
        setNewPosition = true;
    }

    private void StartVertical()
    {
        state = State.Vertical;
        verticalTimer = 0f;
        verticalCenter = transform.position;
        currY = verticalCenter.y;
        deltaY = Random.Range(5f, maxDeltaY); //delta y is in absolute value, will substract to go down and add to go up
        posY = currY;
        if (currY < 30f || currY < deltaY)
            posY += deltaY;
        else if (posY > maxPosY) //when too high go down
        {
            posY -= deltaY;
        }
        else
        {
            if (deltaY > maxDeltaY/2f) // random 50/50 coin flip OR if delta will send us underground
                posY += deltaY;
            else 
                posY -= deltaY;
        }
        posY = Mathf.Clamp(posY, minPosY, maxPosY);
        Debug.Log("target pos Y is " + posY.ToString() + " delta " + deltaY.ToString() + " curr " + currY.ToString());
    }
}
