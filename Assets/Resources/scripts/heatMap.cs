using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



public class HeatMap : MonoBehaviour
{

	public int _subDevisions;
	public float _pixelSize;
	private GameObject _quad;

	public GameObject _heatMapParent;
	public GameObject _targetsParent;

	public  List<GameObject> heatMapPixels = new List<GameObject> ();
	private List <GameObject> _targetsList;


	RaycastHit _hitInfo;
	//ray cast
	public List <Transform> TargetsList;


	// Use this for initialization
	void Start ()
	{
		for (int x = 0; x < _subDevisions; x++) {
			for (int y = 0; y < _subDevisions; y++) {

				_quad = GameObject.CreatePrimitive (PrimitiveType.Quad); //make cell cube  

				Destroy (_quad.GetComponent <MeshCollider> ());
				var _locX = _heatMapParent.transform.position.x; 
				var _locY = _heatMapParent.transform.position.z; 

				_quad.transform.localScale = new Vector3 (_pixelSize, _pixelSize, _pixelSize);
				_quad.transform.parent = _heatMapParent.transform; //put into parent object for later control 
				_quad.transform.position = new Vector3 ((x * _pixelSize) + _locX, 1000, (y * _pixelSize) + _locY); //compensate for scale shift due to height
				_quad.transform.Rotate (90, 90, 0); 
				_quad.GetComponent<Renderer> ().material.color = Color.white;
				_quad.AddComponent <BoxCollider> ();
				_quad.GetComponent<BoxCollider> ().isTrigger = true; 

				heatMapPixels.Add (_quad);
			}
		}
		
		TargetsList = _targetsParent.GetComponentsInChildren<Transform> ().Skip (1).ToList ();

	}

	// Update is called once per frame
	void Update ()
	{

		foreach (var i in TargetsList) {
			TargetController tmp = i.gameObject.GetComponent<TargetController> ();

			if (Physics.Raycast (i.transform.position, Vector3.up, out _hitInfo, Mathf.Infinity)) {
				Collider[] _hitColliders = Physics.OverlapSphere (_hitInfo.collider.transform.position, 5*(tmp._medium + tmp._poor + tmp._rich));

				for (int x = 0; x < _hitColliders.Count(); x++) {
					_hitColliders [x].GetComponent<Renderer> ().material.color = new Color (0, 1/(tmp._poor+.1f) , 0, 0.5f);
					_hitColliders [x].GetComponent<Renderer> ().material.color = new Color (1/(tmp._medium+.1f) ,0 ,0, 0.5f);
					_hitColliders [x].GetComponent<Renderer> ().material.color = new Color (0 ,0 , 1/(tmp._medium+.1f), 0.5f);

				}
			} 
		}
			
	}
}
