using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRLogicManager : PlayerLogicManager
{
    [SerializeField] private CustomMovement m_CustomMovement;

    protected override void Start()
    {
        base.Start();
        this.speed = m_CustomMovement.moveSpeed;
    }

    public override bool TryCatchQuaffle()//TODO
    {
        return false;
    }

    // Seeker + VRPlayer
    public override bool TryCatchSnitch()//TODO
    {
        return false;
    }

    public override void SetTarget(Targetable newTarget)
    { }

    public override void ResetTarget()
    { }

    //public override Targetable GetTarget()
    //{ }

    //public override float GetSpeed()
    //{ }

    public override void SetRotationSpeed(float newRotationSpeed)
    { }

    public override Targetable GetStartingTransformAsTargetable()
    { return null; }

    public override void StopMoveAndRotateToTarget()
    { }

    public override void MoveAndRotateToTarget()
    { }

    //public override IEnumerator MoveAndRotateToBludger(int bludgerIndex, Vector3 relativePos)
    //{ }

    public override void RotateToStartingPosition()
    { }

    public override bool IsSnitchInRange(float range)
    { return true; }

    public override bool IsQuaffleInRange(float range)
    { return true; }

    public override bool IsQuaffleCloseToMyTeamGoals(float range)
    { return true; }

    public override bool IsQuaffleHeldByMyTeam()
    { return true; }

    public override bool IsQuaffleHeldByMe()
    { return true; }

    public override void CaughtQuaffle() //TODO
    { }

    public override void ResetSpeed() //TODO
    { }

    //public override int IsABludgerInRange(float range)
    //{ }

    public override void BudgerWasHit(int bludgerIndex)
    {

    }

    public override void SetGoals(List<ScoreArea> myGoals, List<ScoreArea> enemyGoals)
    { }

    //public override ScoreArea ChooseTargetGoal()
    //{ }

    //public override Vector3 CreateRelativePositionToBewareOfBludgers()
    //{ }

    public override Animator GetAnimator()
    {
        return null;
    }

}
