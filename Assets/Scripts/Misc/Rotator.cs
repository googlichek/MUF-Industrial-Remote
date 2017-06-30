using UnityEngine;

public class Rotator : MonoBehaviour
{
	public Vector3 RotationVector = Vector3.zero;

	private void Update()
	{
		transform.Rotate(RotationVector*Time.deltaTime);
	}
}