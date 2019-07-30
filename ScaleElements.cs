
using UnityEngine;

public class ScaleElements : MonoBehaviour
{

	[Header("Objects")]
	[SerializeField] GameObject objectToScale;
	[SerializeField] GameObject objectToCheck;
	[SerializeField] GameObject cameraToCheck;
	[Header("InitialData")]
	[SerializeField] [Tooltip("Initial position of the gameobject")] public Vector3 posIni;
	[SerializeField] [Tooltip("Initial scale of the gameobject")] public Vector3 scaleIni;
	[SerializeField] float distanceIni;
	[Header("ShowInfo")]
	[SerializeField] [Tooltip("Distance between the came and the gameobject")] float distance;



	private void Start()
	{

		if (objectToScale == null)
		{
			objectToScale = this.gameObject;
		}
		if (cameraToCheck == null)
		{
			cameraToCheck = UnityEngine.Camera.main.gameObject;
		}
		if (objectToCheck == null)
		{
			objectToCheck = this.gameObject;
		}
		scaleIni = objectToScale.transform.localScale;
		posIni = objectToScale.transform.position;


		Vector3 v1 = new Vector3(cameraToCheck.transform.position.x, 0, cameraToCheck.transform.position.z);
		Vector3 v2 = new Vector3(objectToCheck.transform.position.x, 0, objectToCheck.transform.position.z);
		distanceIni = Vector3.Distance(v2, v1);
	}

	private void Update()
	{
		Vector3 v1 = new Vector3(cameraToCheck.transform.position.x, 0, cameraToCheck.transform.position.z);
		Vector3 v2 = new Vector3(objectToCheck.transform.position.x, 0, objectToCheck.transform.position.z);
		distance = Vector3.Distance(v2, v1);
		objectToScale.transform.localScale = Vector3.LerpUnclamped(Vector3.zero, scaleIni, distance / distanceIni);
	}
	public GameObject CameraToCheck
	{
		get
		{
			if (cameraToCheck == null)
			{
				cameraToCheck = Camera.main.gameObject;
			}
			return cameraToCheck;
		}
		set
		{
			cameraToCheck = value;
		}
	}
	public GameObject ObjectToScale
	{
		get
		{
			if (objectToScale == null)
			{
				objectToScale = this.gameObject;
			}
			return cameraToCheck;
		}
		set
		{
			objectToScale = value;
		}
	}
	public GameObject ObjectToCheck
	{
		get
		{
			if (objectToCheck == null)
			{
				objectToCheck = this.gameObject;
			}
			return objectToCheck;
		}
		set
		{
			objectToCheck = value;
		}
	}
}

