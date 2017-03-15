using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetController : MonoBehaviour
{

	public  int _poor;
	//poor
	public  int _medium;
	// medium
	public  int _rich;
	//rich




	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Poor") {
			_poor += 1; 

		} else if (other.gameObject.tag == "Rich") {
			_rich += 1; 

		}
		if (other.gameObject.tag == "Medium") {
			_medium += 1; 

		} else {
			return; 
		} 

	}

}