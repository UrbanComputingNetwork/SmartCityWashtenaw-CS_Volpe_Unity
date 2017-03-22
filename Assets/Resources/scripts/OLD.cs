using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;




public class OLD: MonoBehaviour
{

	public GameObject _cityIoObj;
	cityIO _script;

	public List <Transform> TargetsList;

	[Range (0.0f, 5.0f)]
	public int _toggleSelector;

	public GameObject _kendallHeatmap;
	public GameObject _kendallDay;


	public int _subDevisions;
	public float _pixelSize;
	[Range (0.0f, 1.0f)]
	public float _shrinkingFactor;
	private GameObject _mapPixelObj;
	public Material _baseMaterial;

	public GameObject _targetsParent;
	public  List<GameObject> _heatMapPixels = new List<GameObject> ();
	private Collider[] _radiusColliders;
	private int _agentsAtTarget;


	RaycastHit _hitInfo;
	//ray cast

	void Start ()
	{
		_script = _cityIoObj.transform.GetComponent<cityIO> ();
		TargetsList = _targetsParent.GetComponentsInChildren<Transform> ().Skip (1).ToList (); //move to update for constant scan of list of points 

		for (int x = 0; x < _subDevisions; x++) {
			for (int y = 0; y < _subDevisions; y++) {

				_mapPixelObj = GameObject.CreatePrimitive (PrimitiveType.Sphere); //make cell cube 

				Destroy (_mapPixelObj.GetComponent <MeshCollider> ());
				var _locX = this.transform.position.x; 
				var _locY = this.transform.position.y; 
				var _locZ = this.transform.position.z; 
				_mapPixelObj.transform.localScale = new Vector3 (_pixelSize * _shrinkingFactor, _pixelSize * _shrinkingFactor, _pixelSize * _shrinkingFactor);
				_mapPixelObj.transform.parent = this.transform; //put into parent object for later control 
				_mapPixelObj.transform.position = new Vector3 ((x * _pixelSize) + _locX, _locY, (y * _pixelSize) + _locZ); //compensate for scale shift due to height
				//_quad.transform.Rotate (90, 90, 0); 
				_mapPixelObj.AddComponent <BoxCollider> ();
				_mapPixelObj.GetComponent<BoxCollider> ().isTrigger = true; 
				_mapPixelObj.GetComponent <BoxCollider> ().size = new Vector3 (_pixelSize / 8, 1, _pixelSize / 8); //divide by 8 to get right area around pixel 

				_mapPixelObj.GetComponent<Renderer> ().material = _baseMaterial;
				_mapPixelObj.transform.GetComponent<Renderer> ().shadowCastingMode = ShadowCastingMode.Off;
				_mapPixelObj.tag = "heatmap"; 
				_heatMapPixels.Add (_mapPixelObj);
			}
		}


		if (_script._Cells.objects.toggle4 != _toggleSelector) {
			foreach (Transform child in this.transform) {
				child.gameObject.SetActive (false);
				_kendallHeatmap.SetActive (false); 
				_kendallDay.SetActive (true); 


			}

		}

	}

	// Update is called once per frame
	void Update ()
	{
		if (_script._Cells.objects.toggle4 == _toggleSelector) {
			foreach (Transform _heatMapChildGO in this.transform) {
				_heatMapChildGO.gameObject.SetActive (true);
			}
			_kendallHeatmap.SetActive (true); 
			_kendallDay.SetActive (false); 




			foreach (var i in TargetsList) {
				if (Physics.Raycast (i.transform.position, Vector3.up, out _hitInfo, Mathf.Infinity)) {
				
					if (_hitInfo.collider.tag == "heatmap") {
					
						TargetController _targetsVars = i.gameObject.GetComponent<TargetController> (); //get vars of rich, poor, med from other script 

							
						_agentsAtTarget = (_targetsVars._medium + _targetsVars._poor + _targetsVars._rich); //should show more specific response to types !!

						_radiusColliders = Physics.OverlapSphere (_hitInfo.collider.transform.position, 2 * _agentsAtTarget); //radius of affection 

						for (int x = 0; x < _radiusColliders.Count (); x++) {
							float _dist = Vector3.Distance (_hitInfo.collider.transform.position, _radiusColliders [x].transform.position);

							_radiusColliders [x].transform.position = new Vector3 (
								_radiusColliders [x].transform.position.x,
								(_radiusColliders [x].transform.position.y + _agentsAtTarget * Mathf.Sqrt (_dist)),
								_radiusColliders [x].transform.position.z);
						}
					}
				}
			}



		} else {

			foreach (Transform _heatMapChildGO in this.transform) {
				_heatMapChildGO.gameObject.SetActive (false);
			}
			_kendallHeatmap.SetActive (false); 
			_kendallDay.SetActive (true); 

		}


	}
}

