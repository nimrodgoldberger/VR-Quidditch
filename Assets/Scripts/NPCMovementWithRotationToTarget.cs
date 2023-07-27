using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementWithRotationToTarget : MonoBehaviour
{

    public Transform[] targets;
    [SerializeField] private GameObject target;
    [SerializeField] private float movementSpeed = 50f;
    private Rigidbody rb;
    private float targetTime;
    private Vector3 direction, targetPosition, verticalCenter;

    //private Vector3 targetCenter;

    //private TargetsSpawnArea m_TargetsSpawnArea;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RespawnTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
        targetTime -= Time.deltaTime;
        //TODO  need to add on collision only with this specific target
        if(targetTime <= 0 )
        {
            RespawnTarget();
        }
    }
    
    private void RespawnTarget()
    {
        do
        {
            targetPosition.y = Random.Range(7, 120);
            targetPosition.x = Random.Range(-72, 72);
            targetPosition.z = Random.Range(-219, 219);
        }while(!TargetsSpawnArea.IsInsidePlayableArea(targetPosition));
        
        target.transform.position = targetPosition;
        targetTime = Random.Range(1, 3);
    }
}
