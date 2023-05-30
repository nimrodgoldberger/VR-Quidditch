using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float rotationSpeed = 5f;
    [SerializeField] private float diffThreshold = 1f;
    [SerializeField] public Camera childCamera;

    private void Update()
    {
        if (target != null)
        {
            // Calculate the rotation for the parent object
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z);

            float diff = transform.eulerAngles.y - target.eulerAngles.y;
            if (diff < diffThreshold)
                return;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            if (childCamera != null)
            {
                childCamera.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
