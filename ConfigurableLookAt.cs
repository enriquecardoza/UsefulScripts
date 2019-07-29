using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurableLookAt : MonoBehaviour
{
	public enum TargetAxis
	{
		XY,
		XZ,
		YZ,
		XYZ,
		InverseXY,
		InverseXZ,
		InverseYZ,
		InverseXYZ,
		FREE
	}

	[SerializeField] private TargetAxis targetAxis = TargetAxis.XYZ;
	public TargetAxis TargetAxisRotation
	{
		get
		{
			return targetAxis;
		}
		set
		{
			targetAxis = value;
		}
	}

	[SerializeField] private Transform targetTransform;
	public Transform TargetTransform
	{
		get
		{
			if (targetTransform == null)
			{
				targetTransform = Camera.main.transform;
			}
			return targetTransform;
		}
		set
		{
			targetTransform = value;
		}
	}

	private Transform m_transform;

	private void Start()
	{
		m_transform = this.transform;
	}

	private void Update()
	{
		if (TargetTransform == null)
		{
			return;
		}

		Vector3 posTarget = this.transform.position;

		// Adjust for the pivot axis.
		switch (targetAxis)
		{
			case TargetAxis.XY:
				posTarget = new Vector3(targetTransform.transform.position.x, targetTransform.transform.position.y, this.transform.position.z);
				break;

			case TargetAxis.XZ:
				posTarget = new Vector3(targetTransform.transform.position.x, this.transform.position.y, targetTransform.transform.position.z);
				break;

			case TargetAxis.YZ:
				posTarget = new Vector3(this.transform.position.x, targetTransform.transform.position.y, targetTransform.transform.position.z);
				break;
			case TargetAxis.XYZ:
				posTarget = targetTransform.transform.position;
				break;
			case TargetAxis.InverseXY:
				posTarget = 2 * transform.position - new Vector3(targetTransform.transform.position.x, targetTransform.transform.position.y, this.transform.position.z);
				break;
			case TargetAxis.InverseXZ:
				posTarget = 2 * transform.position - new Vector3(targetTransform.transform.position.x, this.transform.position.y, targetTransform.transform.position.z);
				break;
			case TargetAxis.InverseYZ:
				posTarget = 2 * transform.position - new Vector3(this.transform.position.x, targetTransform.transform.position.y, targetTransform.transform.position.z);
				break;
			case TargetAxis.InverseXYZ:
				posTarget = 2 * transform.position - targetTransform.transform.position;
				break;
			case TargetAxis.FREE:
			default:
				break;
		}

		this.transform.LookAt(posTarget);

	}
}

