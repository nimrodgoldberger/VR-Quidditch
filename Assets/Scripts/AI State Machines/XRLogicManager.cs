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


}
