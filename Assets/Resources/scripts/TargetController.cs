using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TargetController : MonoBehaviour
{
	
	public  int _poor;
	public  int _medium;
	public  int _rich;





	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Poor") {

			if (_poor >= 10) {
				_poor = 0;
			} else {
				_poor += 1;
			
			}

		} else if (other.gameObject.tag == "Rich") {
			
			if (_rich >= 10) {
				_rich = 0;
			} else {
				_rich += 1;

			}

		}
		if (other.gameObject.tag == "Medium") {


			if (_medium >= 10) {
				_medium = 0;
			} else {
				_medium += 1;

			}

		} else {
			return; 
		} 

	}


		
}
	
