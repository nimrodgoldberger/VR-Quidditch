using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float rotationSpeed = 1f;
    [SerializeField] private float diffThreshold = 1f;
    [SerializeField] public Camera childCamera;

    private float mappedMultiplier;
    //private float diffMin = 0f;
    //private float diffMax = 360f;
    //[SerializeField] private float multiplierMin = 1f;
    //[SerializeField] private float multiplierMax = 4f;

    private void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the rotation for the parent object
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z);

            float diff = Mathf.Abs(transform.eulerAngles.y - target.eulerAngles.y);
            //mappedMultiplier = Remap(diff, diffMin, diffMax, multiplierMin, multiplierMax);

            if (diff < diffThreshold)
                return;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            if (childCamera != null)
            {
                childCamera.transform.localRotation = Quaternion.identity;
            }
        }
    }

    private float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        var valueAbs = value - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = valueAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}
