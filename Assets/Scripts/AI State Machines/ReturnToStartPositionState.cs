using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStartPositionState : State
{
    public IdleState idleState;
    [SerializeField] private float minDistance = 1f;
    Targetable startingPos = null;
    //bool wasSnitchCaught;
    public override State RunCurrentState()
    {
        if(!startingPos)
        {
            startingPos = Logic.GetStartingTransformAsTargetable();
            Logic.SetTarget(startingPos);
        }

        if(Vector3.Distance(transform.position, startingPos.transform.position) <= minDistance)
        {
            transform.position = startingPos.transform.position;
            transform.rotation = startingPos.transform.rotation;

            Debug.Log("I will return to my starting position");
            return idleState;
        }
        else
        {
            Logic.MoveAndRotateToTarget();

            return this;
        }
    }
}
