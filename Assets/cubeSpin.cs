using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSpin : MonoBehaviour
{

	private Rigidbody rb;
	private bool onTrack;
	private Vector3 direction;
	private Vector3 targetPosition;

	[SerializeField]
	Transform rotationCenter;

	[SerializeField]
	float rotationRadius = 370f, angularSpeed = 1f, ovalWidth = 3f, movementSpeed = 50f;

	float posX, posZ, angle = 0f;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if(onTrack)
        {
			posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius / ovalWidth;
			posZ = rotationCenter.position.z + Mathf.Sin(angle) * rotationRadius;
			targetPosition = new Vector3(posX, 10f, posZ);
			angle += Time.deltaTime * movementSpeed / ovalWidth;
			if (angle > Mathf.PI * 2f)
			{
				angle -= Mathf.PI * 2f;
			}
		}
		onTrack = (Vector3.Distance(targetPosition, transform.position) < 15f);
		direction = targetPosition - transform.position;
		direction.Normalize();
		rb.velocity = direction * movementSpeed;
	}
}