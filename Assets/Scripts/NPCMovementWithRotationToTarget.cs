using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementWithRotationToTarget : MonoBehaviour
{

    public enum State
    {
        Roam = 1,
        Moving = 2,
        Vertical = 3,
        Run = 4
    }
    public State state;


    //Target Related Fields
    [SerializeField] public GameObject target;
    [SerializeField] private float RotationSpeed = 3f;
    private Coroutine LookCoroutine;
    private float targetTime = 5;

    private AudioSource audioSource;
    public AudioClip whirrLoop;
    [SerializeField] private float movementSpeed = 50f;
    private Vector3 direction, targetPosition, verticalCenter;


    void Start()
    {
        RespawnTarget();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
        StartRotating();
        targetTime -= Time.deltaTime;
        //TODO  need to add on collision only with this specific target
        if(targetTime <= 0)
        {
            RespawnTarget();
            targetTime = Random.Range(2, 5);
        }
    }

    public void RespawnTarget()
    {
        Vector3 targetPosition;
        do
        {
            targetPosition.y = Random.Range(7, 120);
            targetPosition.x = Random.Range(-72, 72);
            targetPosition.z = Random.Range(-219, 219);

        } while(!TargetsSpawnArea.IsInsidePlayableArea(targetPosition));

        target.transform.position = targetPosition;
    }


    private void StartRotating()
    {
        if(LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - transform.position);

        float time = 0;

        Quaternion initialRotation = transform.rotation;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, time);

            time += Time.deltaTime * RotationSpeed;

            yield return null;
        }
    }
}
