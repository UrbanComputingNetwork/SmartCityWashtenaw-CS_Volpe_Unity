using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{

	public Transform _target;

	public float _speed = 5f;
	cityIO _script;
	public GameObject _cityIOgameObj;
//	private Vector3 _startPos;
//	private Quaternion _startRot;


	void Start ()
	{
//		_startPos = transform.position;
//		_startRot = transform.rotation;
		//print(_startPos.ToString());
		_script = _cityIOgameObj.transform.GetComponent<cityIO> ();
	}

	void Update ()
	{
		if (_script._flag == true) { // data IS flowing from cityIO 
			if (_script._Cells.objects.dockID == -1) { // slider position 
				transform.RotateAround (_target.position, Vector3.up, _speed * Time.deltaTime); //-Input.GetAxis("Horizontal")
			
			} else {
//				transform.position = _startPos; // if slider or toggle is not in this view anymore 
//				transform.rotation = _startRot; // if slider or toggle is not in this view anymore 

				transform.RotateAround (_target.position, Vector3.up, 0); //-Input.GetAxis("Horizontal")


			}
		}
	}
}
