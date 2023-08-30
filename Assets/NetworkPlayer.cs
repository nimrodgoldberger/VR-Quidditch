using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NetworkPlayer : MonoBehaviour
{

    [SerializeField] Transform XRheadTransform;
    [SerializeField] Transform XRleftTransform;
    [SerializeField] Transform XRrightTransform;



    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    void Update()
    {
        MapPosition(head, XRNode.Head);
        MapPosition(leftHand, XRNode.LeftHand);
        MapPosition(rightHand, XRNode.RightHand);
    }

    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        Debug.Log("positon:" + position.ToString());
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;
    }
}
