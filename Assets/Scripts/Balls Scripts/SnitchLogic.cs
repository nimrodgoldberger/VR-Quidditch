using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnitchLogic : Targetable
{
    [SerializeField] public GameObject target;
    // To make target respawn when Snitch gets too close or certain amount of time passes
    [SerializeField] private float targetTime = 5.0f;
    [SerializeField] private float minDistanceToRespawnFrom = 7.0f;
    // To make target respawn far from the snitch
    [SerializeField] private float mixDistanceToRespawnTo = 80.0f;
    // To adjust the Snitch movement and catching distance
    [SerializeField] private float RotationSpeed = 3f;
    [SerializeField] private float movementSpeed = 50f;
    [SerializeField] private float takeDistance = 5.0f;// TODO adjust value just to try it 
    // To add score and end Match after snitch was caught
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TimeTextManager timeTextManager;
    //To know if SnitchWasCaught and by which team
    private bool wasSnitchCaught = false;
    public PlayerTeam caughtBy = PlayerTeam.None;
    // To make it face the target
    private Coroutine LookCoroutine;
    //Flags to gradually slow down the snitch and make it easier to catch
    private bool afterQuarter1 = false;
    private bool afterQuarter2 = false;
    private bool afterQuarter3 = false;

    // TODO Audio for Snitch Redo 
    public AudioClip whirrLoop;
    private AudioSource audioSource;

    void Start()
    {
        RespawnTarget();
    }

    void FixedUpdate()
    {
        if(!wasSnitchCaught)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.fixedDeltaTime);
            StartRotating();
            targetTime -= Time.fixedDeltaTime;
            if(Vector3.Distance(transform.position, target.transform.position) < minDistanceToRespawnFrom)
            {
                RespawnTarget();
                targetTime = Random.Range(2, 4); //Maybe change range to 3-5 or 2-5
            }
            else if(targetTime <= 0)
            {
                RespawnTarget();
                targetTime = Random.Range(2, 4);
            }

            MakeCatchingEasierGradually();
        }
    }

    public void RespawnTarget()
    {
        Vector3 targetPosition;
        bool isTargetTooClose = true;

        do
        {
            targetPosition.y = Random.Range(8f, 120f);
            targetPosition.x = Random.Range(-72f, 72f);
            targetPosition.z = Random.Range(-219f, 219f);
            isTargetTooClose = Vector3.Distance(transform.position, targetPosition) < mixDistanceToRespawnTo;
        } while(isTargetTooClose || !TargetsSpawnArea.IsInsidePlayableArea(targetPosition));

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

    public bool WasSnitchCaught()
    {
        return wasSnitchCaught;
    }

    public bool TryCatchSnitch(PlayerLogicManager player)
    {
        if(!wasSnitchCaught && Vector3.Distance(transform.position, player.transform.position) <= takeDistance)
        {
            wasSnitchCaught = true;
            SnitchWasCaught(player);
        }

        return wasSnitchCaught;
    }

    private void SnitchWasCaught(PlayerLogicManager player)
    {
        Vector3 relativepos = new Vector3(0.35f, 0.35f, 0.2f);
        caughtBy = player.PlayerTeam;
        transform.SetParent(player.transform);
        transform.localPosition = relativepos;
        string name = Enum.GetName(caughtBy.GetType(), caughtBy);
        // TODO Check that works
        scoreManager.SetTeamScore(caughtBy, 150);
        timeTextManager.TimeRemaining = 0.0f;
    }

    private void MakeCatchingEasierGradually()
    {
        float timeLeft = timeTextManager.TimeRemaining;
        if(!afterQuarter1 && timeLeft < 0.75 * timeTextManager.totalTime)
        {
            afterQuarter1 = true;
            movementSpeed = movementSpeed * 0.9f;
        }
        else if(!afterQuarter2 && timeLeft < 0.5 * timeTextManager.totalTime)
        {
            afterQuarter2 = true;
            movementSpeed = movementSpeed * 0.8f;
            minDistanceToRespawnFrom = minDistanceToRespawnFrom / 2.0f;
        }
        else if(!afterQuarter3 && timeLeft < 0.25 * timeTextManager.totalTime)
        {
            afterQuarter3 = true;
            movementSpeed = movementSpeed * 0.8f;
            minDistanceToRespawnFrom = 1.0f;
        }
    }
}
