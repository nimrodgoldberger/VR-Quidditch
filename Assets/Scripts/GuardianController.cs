using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGuardianState { Idle = 0, Defend = 1, GoBack = 2 }



public class GuardianController : MonoBehaviour
{
    
    public GameObject[] targetsToDefend;
    public GameObject[] oponents;

    private GameObject targetToTackle;
    private Vector3 direction;
    public float speed = 1f;
    private Coroutine movementCoroutine;

    private Vector3 startingPosition;
    private eGuardianState eState;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        eState = eGuardianState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(eState)
        {
            case eGuardianState.Idle:
                searchForCloseOponents();
                break;
            case eGuardianState.Defend:
            case eGuardianState.GoBack:
                tackleTarget();
                break;
        }
    }

    private void searchForCloseOponents()
    {
        foreach(GameObject target in targetsToDefend)
        {
            foreach(GameObject oponent in oponents)
            {
                if(IsOponentClose(target, oponent))
                {
                    eState = eGuardianState.Defend;
                    targetToTackle = oponent;
                    break;
                }
            }
        }
    }

    private void retreatToStartingPosition()
    {

    }

    private bool IsOponentClose(GameObject target, GameObject oponent)
    {
        if(Vector3.Distance(oponent.transform.position, target.transform.position) < 50)
        {
            return true;
        }

        return false;
    }

    private void tackleTarget()
    {
        movementCoroutine = StartCoroutine(MoveToTarget());
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        

        while(true)
        {
            // Calculate the direction towards the target
            if(eState == eGuardianState.Defend)
            {
                Vector3 direction = targetToTackle.transform.position - transform.position;
            }
            else
            {
                Vector3 direction = startingPosition - transform.position;
            }


            // Calculate the distance to the target
            float distance = direction.magnitude;

            // Normalize the direction to get a unit direction vector
            Vector3 normalizedDirection = direction.normalized;

            // Calculate the distance to cover in this frame based on the speed
            float distanceToCover = speed * Time.deltaTime;

            // Check if the distance to cover is greater than the remaining distance
            if(distanceToCover >= distance)
            {
                // If the distance to cover is greater or equal to the remaining distance,
                // directly move the GameObject to the target position
                if(eState == eGuardianState.Defend)
                {
                    transform.position = targetToTackle.transform.position;
                }
                else
                {
                    transform.position = startingPosition;
                }
                break; // Exit the coroutine loop
            }
            else
            {
                // If the distance to cover is less than the remaining distance,
                // interpolate the position of the GameObject towards the target
                transform.position += normalizedDirection * distanceToCover;
            }

            yield return null; // Wait for the next frame
        }
    }

}
