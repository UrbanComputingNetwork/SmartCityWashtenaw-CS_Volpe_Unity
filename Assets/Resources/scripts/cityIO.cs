using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cityIO : MonoBehaviour
{

	public string url = "http://45.55.73.103/table/citymatrix_volpe";
	public int delayWWW; 

	IEnumerator Start ()
	{
		while (true) {

			WWW www = new WWW (url);
			yield return www;
			Debug.Log (www.text);
			yield return new WaitForSeconds (delayWWW);
		}
	}
}

