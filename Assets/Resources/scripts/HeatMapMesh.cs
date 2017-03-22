using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

public class HeatMapMesh: MonoBehaviour
{
	private int _random;


	public GameObject _cityIoObj;
	public GameObject _targetsParent;
	public List <Transform> TargetsList;
	public  List<GameObject> _heatMapPixels = new List<GameObject> ();
	private Collider[] _radiusColliders;
	private int _agentsAtTarget;
	RaycastHit _hitInfo;




	void Start ()
	{
		TargetsList = _targetsParent.GetComponentsInChildren<Transform> ().Skip (1).ToList (); //move to update for constant scan of list of points 
		transform.GetComponent<Renderer> ().shadowCastingMode = ShadowCastingMode.Off;
		tag = "heatmap"; 
	}

	void Update ()
	{
		Mesh mesh = GetComponent<MeshFilter> ().mesh;

			
		foreach (var i in TargetsList) {
			TargetController _targetsVars = i.gameObject.GetComponent<TargetController> (); //get vars of rich, poor, med from other script 
			_agentsAtTarget = (_targetsVars._medium + _targetsVars._poor + _targetsVars._rich); //should show more specific response to types !!

			if (Physics.Raycast (i.transform.position, Vector3.up, out _hitInfo, Mathf.Infinity)) {

				var MC = _hitInfo.collider as MeshCollider;
				if (MC != null) {
					var index = _hitInfo.triangleIndex * 3;
					var hit1 = mesh.vertices [mesh.triangles [index]];
					//var hit2 = mesh.vertices [mesh.triangles [index + 1]];
					//var hit3 = mesh.vertices [mesh.triangles [index + 2]];

					Vector3[] vertices = mesh.vertices;
					int[] triangles = mesh.triangles;

					int x = 0;
					while (x < vertices.Length) {
						if (vertices [x] == hit1 && Mathf.Sqrt( _agentsAtTarget) > 0)
							vertices [x] = new Vector3 (hit1.x,  _agentsAtTarget,hit1.z);
						x++;
				
						mesh.vertices = vertices;
						mesh.RecalculateBounds ();
					}
				}
			}
		}
	}
}
