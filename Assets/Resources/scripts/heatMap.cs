using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;




public class HeatMap : MonoBehaviour
{

	public int _subDevisions;
	private float _pixelSize;
	private GameObject _quad;
	public Material _baseMaterial;
	public int _startHeight;

	public GameObject _heatMapParent;
	public GameObject _targetsParent;

	public  List<GameObject> _heatMapPixels = new List<GameObject> ();
	private Collider[] _radiusColliders;


	RaycastHit _hitInfo;
	//ray cast
	public List <Transform> TargetsList;


	// Use this for initialization
	void Start ()
	{
		_pixelSize = 1000 / _subDevisions; 
		for (int x = 0; x < _subDevisions; x++) {
			for (int y = 0; y < _subDevisions; y++) {

				_quad = GameObject.CreatePrimitive (PrimitiveType.Quad); //make cell cube 

				Destroy (_quad.GetComponent <MeshCollider> ());
				var _locX = _heatMapParent.transform.position.x; 
				var _locY = _heatMapParent.transform.position.z; 

				_quad.transform.localScale = new Vector3 (_pixelSize, _pixelSize, _pixelSize);
				_quad.transform.parent = _heatMapParent.transform; //put into parent object for later control 
				_quad.transform.position = new Vector3 ((x * _pixelSize) + _locX, _startHeight, (y * _pixelSize) + _locY); //compensate for scale shift due to height
				_quad.transform.Rotate (90, 90, 0); 
				_quad.AddComponent <BoxCollider> ();
				_quad.GetComponent<BoxCollider> ().isTrigger = true; 
				_quad.GetComponent<Renderer> ().material = _baseMaterial;
				_quad.GetComponent<Renderer> ().material.color = new Color (0, 0, 0, 0);
				_quad.transform.GetComponent<Renderer> ().shadowCastingMode = ShadowCastingMode.Off;

				_heatMapPixels.Add (_quad);
			}
		}
		
		TargetsList = _targetsParent.GetComponentsInChildren<Transform> ().Skip (1).ToList ();

	}

	// Update is called once per frame
	void Update ()
	{
		foreach (var i in TargetsList) {
			if (Physics.Raycast (i.transform.position, Vector3.up, out _hitInfo, Mathf.Infinity)) {
				TargetController _targetsVars = i.gameObject.GetComponent<TargetController> (); //get vars of rich, poor, med from other script 
				var _agentsAtTarget = (_targetsVars._medium + _targetsVars._poor + _targetsVars._rich);

				print (_targetsVars._medium + " " + _targetsVars._poor + " " +  _targetsVars._rich);

				_radiusColliders = Physics.OverlapSphere (_hitInfo.collider.transform.position, (_subDevisions / 10) * _agentsAtTarget); //radius of affection  

				for (int x = 0; x < _radiusColliders.Count (); x++) {
					float dist = Vector3.Distance (_hitInfo.collider.transform.position, _radiusColliders [x].transform.position);

					if (_radiusColliders [x].GetComponent<Renderer> ().material.color.a == 0) {
						_radiusColliders [x].GetComponent<Renderer> ().material.color = 
							new Color (dist / 100, 100 / (dist + 0.001f), 0, 0.75f);
						
						if (dist > 0) {
							
							_radiusColliders [x].transform.position = new Vector3 (_radiusColliders [x].transform.position.x,
								(_radiusColliders [x].transform.position.y - Mathf.Sqrt (dist)), 
								_radiusColliders [x].transform.position.z); 
							
						}
					}
				}
			}
		}
	}
}

