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
    private Transform target;
    private float verticalTimer = 0f, sleepTimer = 0f, maxDeltaY = 20f, rand, randSleep;


    private AudioSource audioSource;
    public AudioClip whirrLoop;

    private Rigidbody rb;
    private Vector3 direction, targetPosition, verticalCenter;

    [SerializeField]
    float posY = 10f, verticalRadius =3f, rotationRadius = 370f, angularSpeed = 1f, ovalWidth = 3f, movementSpeed = 50f, maxSleepTime = 6f;
    [SerializeField]
    Transform rotationCenter;

    private float currY, deltaY, posX, posZ, angle = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = State.Roam;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = whirrLoop;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (transform.position.y < 5f)
        //    StartVertical();
        if (state == State.Roam && Random.Range(0, (1000 - (int)(verticalTimer * 10))) == 0)
        {
            StartVertical();
        }
        if (state == State.Roam)
        {
            if (sleepTimer < randSleep)
            {
                sleepTimer += Time.deltaTime;
            }
            else
            {
                if (setNewPosition)
                {
                    rand = Random.value;
                    posX = (rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius / ovalWidth) * rand;
                    posZ = (rotationCenter.position.z + Mathf.Sin(angle) * rotationRadius) * rand;
                    targetPosition = new Vector3(posX, posY, posZ);
                    angle += Time.deltaTime * movementSpeed / ovalWidth;
                    if (angle > Mathf.PI * 2f)
                    {
                        angle -= Mathf.PI * 2f;
                    }
                    setNewPosition = false;
                }
                direction = targetPosition - transform.position;
                direction.Normalize();
                rb.velocity = direction * movementSpeed;
                if (Vector3.Distance(targetPosition, transform.position) < 15f)
                {
                    if (rand < 0.5f)
                        StartVertical();
                    else
                        StartRoam();
                }
            }
        }
        else if (state == State.Vertical)
        {
            posX = verticalCenter.x + Mathf.Cos(angle) * verticalRadius;
            posZ = verticalCenter.z + Mathf.Sin(angle) * verticalRadius;
            if (currY < posY)
                currY += 1f;
            targetPosition = new Vector3(posX, currY, posZ);
            angle += Time.deltaTime * movementSpeed / verticalRadius;
            if (angle > Mathf.PI * 2f)
            {
                angle -= Mathf.PI * 2f;
            }
            if (transform.position.y - posY < 5f)
                StartRoam();
        }
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
        posY = currY;
        deltaY = Random.Range(0f, maxDeltaY);
        if (currY < 25f || currY < deltaY || currY < 5f)
            posY += deltaY;
        else if (posY > 300f)
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
        Debug.Log("pos Y is" + currY.ToString() + "delta " + deltaY.ToString() + "curr " + currY.ToString());
    }
}
