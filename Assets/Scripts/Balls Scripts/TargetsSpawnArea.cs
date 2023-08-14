using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsSpawnArea : MonoBehaviour
{
    public static float ElipseValueOfA = 219f, ElipseValueOfB = 72f, minHeight = 8f, maxHeight = 120f;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public static bool IsInsidePlayableArea(Vector3 position)
    {
        bool inside = true;
        float distanceFromCenter = Mathf.Sqrt(Mathf.Pow(position.z, 2) + Mathf.Pow(position.x, 2));

        if(position.y < minHeight || position.y > maxHeight)
        {
            inside = false;
        }
        else if((Mathf.Pow(distanceFromCenter, 2) / Mathf.Pow(ElipseValueOfA, 2)) + (Mathf.Pow(distanceFromCenter, 2) / Mathf.Pow(ElipseValueOfB, 2)) >= 1f)
        {
            inside = false;
        }

        return inside;
    }
}
