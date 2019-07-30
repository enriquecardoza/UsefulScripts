
using UnityEngine;

public class CheckVisible : MonoBehaviour
{

	/// <summary>
	/// You must consider that the scene camera count as a camera rendering the gameobjects
	/// This script its usefull for example if you want to check  if diferent points are in the camera before start the script of a gameobject
	/// </summary>
	[SerializeField][Tooltip("All script of the gameobject")] MonoBehaviour[] scriptList;
    [SerializeField] [Tooltip("List of gameobject to check if theirs render is visible")] GameObject[] objectsCheckRenderer;
	ScaleElements sc;
	private void Awake()
	{
		if (scriptList.Length == 0)
			scriptList = this.GetComponentsInChildren<MonoBehaviour>();
		if (objectsCheckRenderer.Length == 0)
		{
			objectsCheckRenderer = new GameObject[1];
			objectsCheckRenderer[0] = this.gameObject;
		}
		foreach (MonoBehaviour mb in scriptList)
		{
			mb.enabled = false;
		}
		this.enabled = true;
	}
	void Update()
    {

		bool isVisible = false;

		foreach(GameObject go in objectsCheckRenderer)
		{
			if (go.GetComponent<Renderer>().isVisible)
				isVisible = true;
		}


        if (isVisible)
        {
            foreach (MonoBehaviour mb in scriptList)
            {

                mb.enabled = true;
            }

        }
        else
        {
            foreach (MonoBehaviour mb in scriptList)
            {
                mb.enabled = false;
            }
            this.enabled = true;
        }
    }
	
}
