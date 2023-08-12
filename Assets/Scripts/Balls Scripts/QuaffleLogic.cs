using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaffleLogic : Targetable
{
    public float takeDistance = 2f;
    public float takeTime = 0.2f;
    private bool isQuaffleHeld = false;
    private PlayerTeam heldBy = PlayerTeam.None;
    //private float[] teamTimers = {0.0f, 0.0f, 0.0f, 0.0f};

    // TODO For passing and throwing
    private bool wasThrown = false;
    public bool isFlying = false;
    public Targetable target;
    [SerializeField] private float movementSpeed = 30f;


    private void FixedUpdate()
    {
        if(isFlying)
        {
            //Debug.Log("Quaffle Is Flying");
            MoveAndRotateToTarget();
        }
        if(isFlying && heldBy != PlayerTeam.None)
        {
            //Debug.Log("MAYBE ERROR!! Quaffle Is Flying BUT heldBy != PlayerTeam.None");
            isFlying = false;
            isQuaffleHeld = false;
        }
    }


    public bool CanBeTaken(PlayerLogicManager potentialHolder)
    {
        // Check if the potential holder is within range of the ball
        return (Vector3.Distance(transform.position, potentialHolder.transform.position) < takeDistance) && (heldBy != potentialHolder.PlayerTeam);
    }

    public bool TryTakeQuaffle(PlayerLogicManager player)
    {
        bool result = false;

        if(Vector3.Distance(transform.position, player.transform.position) <= takeDistance)
        {
            result = TakeQuaffle(player);
        }

        //if(heldBy != player.PlayerTeam)
        //{
        //    // TODO Check if timer works
        //    // 0.5 seconds pass before taking it;

        //    player.quaffleTakeTime += Time.fixedDeltaTime;
        //    if(player.quaffleTakeTime > takeTime)
        //    {
        //        result = TakeQuaffle(player);
        //        player.quaffleTakeTime = 0.0f;
        //    }
        //}
        //else
        //{
        //    player.quaffleTakeTime = 0.0f;
        //    if(heldBy == PlayerTeam.None)
        //    {
        //        result = TakeQuaffle(player);
        //    }
        //}

        return result;
    }

    private bool TakeQuaffle(PlayerLogicManager player)
    {
        if(!isQuaffleHeld) // Free to take.
        {
            PlayerHoldsQuaffle(player);
        }
        else if(heldBy != player.PlayerTeam)
        {
            player.quaffleTakeTime += Time.fixedDeltaTime;
            if(player.quaffleTakeTime > takeTime)
            {
                PlayerHoldsQuaffle(player);
            }
            //else
            //{

            //}
        }

        return isQuaffleHeld;
    }

    private void PlayerHoldsQuaffle(PlayerLogicManager player)
    {
        Vector3 relativepos = new Vector3(0.35f, 0.35f, 0.2f);
        isFlying = false;
        isQuaffleHeld = true;
        heldBy = player.PlayerTeam;
        transform.SetParent(player.transform);
        transform.localPosition = relativepos;
        player.quaffleTakeTime = 0.0f;

    }

    // XROrigin TakeQuaffle

    private void SetQuaffleTarget(Targetable newTarget)
    {
        target = newTarget;
    }

    private void FlyToTarget()
    {
        //transform.SetParent(null);
        isFlying = true;
        wasThrown = true;
        MoveAndRotateToTarget();
        //1. FlyToTarget
        //2. SetTheNewParent + team...
    }

    public void ThrowQuaffle(Targetable newTarget)
    {
        transform.parent = null;
        heldBy = PlayerTeam.None;
        isQuaffleHeld = false;
        SetQuaffleTarget(newTarget);
        FlyToTarget();
    }

    public void MoveAndRotateToTarget()
    {
        //if(isFlying)
        //    return; // Return if already moving
        StartCoroutine(MoveAndRotateCoroutine());
    }

    private IEnumerator MoveAndRotateCoroutine()
    {
        if(wasThrown && target)
        {
            isFlying = true;
            float totalDistance = Vector3.Distance(transform.position, target.transform.position);
            float travelDuration = totalDistance / movementSpeed;
            Vector3 initialPosition = transform.position;
            Vector3 targetPosition = target.transform.position;
            Quaternion initialRotation = transform.rotation;
            Quaternion lookRotation = Quaternion.LookRotation(targetPosition - initialPosition);

            float elapsedTime = 0f;
            while(elapsedTime < travelDuration)
            {
                // Calculate the new position and rotation using Mathf.Lerp
                float t = elapsedTime / travelDuration;
                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
                transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, t);

                // Yielding inside FixedUpdate to wait for the next FixedUpdate step
                yield return new WaitForFixedUpdate();

                elapsedTime += Time.fixedDeltaTime;
            }

            // Ensure the final position and rotation are set correctly
            transform.position = targetPosition;
            transform.rotation = lookRotation;

            // At the end of the coroutine, reset the target and isMoving flag
            ResetTarget();
            isFlying = false;
            wasThrown = false;
        }
    }

    public void ResetTarget()
    {
        target = null;
    }

    public bool IsQuaffleHeldByTeam(PlayerTeam team)
    {
        return heldBy == team;
    }

    public bool IsQuaffleHeldByPlayer(PlayerLogicManager player)
    {
        return heldBy == player.PlayerTeam && player == transform.parent.GetComponent<PlayerLogicManager>();
    }

}
