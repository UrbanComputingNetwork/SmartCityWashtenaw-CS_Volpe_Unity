using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class cameraControl : MonoBehaviour
{

	public Transform _target;
	private float _startSize;
	public float _endSize = 200;
	public int _add = 0;
	//temp for test
	private bool _flag = true;
	public float _rotSpeed = 5f;
	public float _transitionDuration = 1;
	private float t = 0.0f;


	CityScopeData _script;
	public GameObject _cityIOgameObj;
	public Camera _thisCam;


	void Start ()
	{
		_script = _cityIOgameObj.transform.GetComponent<CityScopeData> ();
		_startSize = _thisCam.orthographicSize; 
	}

	void Update ()
	{
			
			if (Table.Instance.objects.dockID + _add == -1) { // slider position 
				
				if (_thisCam.orthographicSize != _startSize) { 
					t -= Time.deltaTime * (Time.timeScale / _transitionDuration);
					_thisCam.orthographicSize = Mathf.Lerp (_startSize, _endSize, t);
				}

				transform.RotateAround (_target.position, Vector3.up, _rotSpeed * Time.deltaTime); //-Input.GetAxis("Horizontal")

			} else {
				
				transform.RotateAround (_target.position, Vector3.up, 0); //-Input.GetAxis("Horizontal")
				t += Time.deltaTime * (Time.timeScale / _transitionDuration);
				_thisCam.orthographicSize = Mathf.Lerp (_startSize, _endSize, t);

			}
	}
}