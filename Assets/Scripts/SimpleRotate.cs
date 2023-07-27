using System.Collections;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public Transform Target;
    public float Speed = 1f;

    private Coroutine LookCoroutine;

    public void StartRotating()
    {
        if(LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);

        float time = 0;

        Quaternion initialRotation = transform.rotation;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, time);

            time += Time.deltaTime * Speed;

            yield return null;
        }
    }

    //public float RotationAmount = 2f;
    //public int TicksPerSecond = 60;
    //public bool Pause = false;

    //private void Start()
    //{
    //    StartCoroutine(Rotate());
    //}

    //private IEnumerator Rotate()
    //{
    //    WaitForSeconds Wait = new WaitForSeconds(1f / TicksPerSecond);

    //    while(true)
    //    {
    //        if(!Pause)
    //        {
    //            transform.Rotate(Vector3.up * RotationAmount);
    //        }

    //        yield return Wait;
    //    }
    //}
}