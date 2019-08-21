using UnityEngine;
using System.Collections;

public class DestroyByBoundary_Script : MonoBehaviour 
{	
	//Called when the Trigger Exit
	void OnTriggerExit2D(Collider2D other)
	{
		other.gameObject.SetActive(false);
		if(other.tag=="Boss1")
		Destroy(other.gameObject);
	}

}
