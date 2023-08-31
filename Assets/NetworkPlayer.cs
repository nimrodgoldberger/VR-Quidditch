using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;


public class NetworkPlayer : MonoBehaviour
{
    private Transform xrOrigin;
    private Transform mainCamera;
    private Transform leftHandController;
    private Transform rightHandController;
    //public
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        // Find the XR Origin GameObject in the scene
        xrOrigin = GameObject.Find("XR Origin").transform;
        xrOrigin = xrOrigin.Find("Camera Offset");

        // Find subcomponents of the XR Origin
        mainCamera = xrOrigin.Find("Main Camera");
        leftHandController = xrOrigin.Find("LeftHand Controller");
        rightHandController = xrOrigin.Find("RightHand Controller");

        // Check if the subcomponents were found
        if (mainCamera == null)
            Debug.LogError("Main Camera not found under XR Origin");
        else
        {
            Debug.LogError("found XR Origin cam");
        }

        if (leftHandController == null)
            Debug.LogError("Left Hand Controller not found under XR Origin");
        else
        {
            Debug.LogError("found XR Origin left");
        }

        if (rightHandController == null)
            Debug.LogError("Right Hand Controller not found under XR Origin");
        else
        {
            Debug.LogError("found XR Origin right");
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) //Update your avatar only
        {
            return;
        }
        else if (xrOrigin == null)
        {
            Debug.LogError("NO XR Origin");
            return;
        }

        head.gameObject.SetActive(false);
        leftHand.gameObject.SetActive(false);
        rightHand.gameObject.SetActive(false);

        Vector3 newHeadPosition = xrOrigin.Find("Main Camera").position;
        Quaternion newHeadRotation = xrOrigin.Find("Main Camera").rotation;

        Vector3 newLeftPosition = xrOrigin.Find("LeftHand Controller").position;
        Quaternion newLeftRotation = xrOrigin.Find("LeftHand Controller").rotation;

        Vector3 newRightPosition = xrOrigin.Find("RightHand Controller").position;
        Quaternion newRightRotation = xrOrigin.Find("RightHand Controller").rotation;

        Debug.Log("Main Camera position is: " + newHeadPosition.ToString());
        Debug.Log("LeftHand position is: " + newLeftPosition.ToString());
        Debug.Log("RightHand position is: " + newRightPosition.ToString());



        // Update the positions of head, left hand, and right hand based on XR Origin
        if (head != null)
        {
            head.position = newHeadPosition;
            head.rotation = newHeadRotation;
        }
        else
            Debug.LogError("Camera not found under XR Origin update");

        if (leftHand != null)
        {
            leftHand.position = newLeftPosition;
            leftHand.rotation = newLeftRotation;
        }
        else
            Debug.LogError("Left Hand Controller not found under XR Origin update");

        if (rightHand != null)
        {
            rightHand.position = newRightPosition;
            rightHand.rotation = newRightRotation;
        }
        else
            Debug.LogError("Right Hand Controller not found under XR Origin update");


    }


    //void MapPosition(Transform target, XRNode node)
    //{
    //    InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
    //    Debug.Log("positon of + " + node.ToString() + " :" + position.ToString());
    //    InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);
    //    Debug.Log("positon of + " + node.ToString() + " :" + rotation.ToString());


    //    target.position = position;
    //    target.rotation = rotation;
    //}
}
