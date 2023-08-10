using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Targetable : MonoBehaviour
{
    public static void SetRelativeTarget(Vector3 relativePosition, Targetable baseTarget, Targetable relativePositionTarget)
    {
        relativePositionTarget.transform.SetParent(baseTarget.transform);
        relativePositionTarget.transform.localPosition = relativePosition;
    }
}
