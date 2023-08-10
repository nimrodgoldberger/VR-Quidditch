using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsSpawnArea : MonoBehaviour
{
    
    public static float ElipseValueOfA = 219f, ElipseValueOfB = 72f, minHeight = 8f, maxHeight = 120f;
    //[SerializeField] private float RotationSpeed = 3f;
    //[SerializeField] private GameObject target;
    //[SerializeField] private GameObject baseGameObject;
    //private Coroutine LookCoroutine;
    //private float targetTime;

    //public TargetsSpawnArea(GameObject target)
    //{
    //    this.target = target;
    //}

    //private float ElipseValueOfC;
    //public Vector3 center;
    //public Vector3 spawnArea;


    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    //public float TargetManager(GameObject target, float targetTime)
    //{
    //    targetTime -= Time.deltaTime;
    //    //TODO  need to add on collision only with this specific target
    //    if(targetTime <= 0)
    //    {
    //        RespawnTarget(target);
    //        targetTime = Random.Range(1, 3);
    //    }

    //    return targetTime;
    //}

    //public void RespawnTarget(GameObject target)
    //{
    //    Vector3 targetPosition;
    //    do
    //    {
    //        targetPosition.y = Random.Range(7, 120);
    //        targetPosition.x = Random.Range(-72, 72);
    //        targetPosition.z = Random.Range(-219, 219);
    //    } while(!IsInsidePlayableArea(targetPosition));

    //    target.transform.position = targetPosition;
    //}

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
