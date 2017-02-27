using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class AgentsController : MonoBehaviour
{

	private GameObject spwanerParent;
	private new List <GameObject> otherList;
	private NavMeshAgent thisAgentNavMesh;
	private TrailRenderer tr;
	public float timeAtAmenity;

	private Color thisColor;


	void Start ()
	{

		spwanerParent = GameObject.Find ("Spawners");
		otherList = spwanerParent.GetComponent<AgentsSpawner> ().AgentsList; 

		thisColor = gameObject.GetComponent<Renderer> ().material.color;
		tr = GetComponent<TrailRenderer> ();
		tr.material = new Material (Shader.Find ("Sprites/Default"));

		// A simple 2 color gradient with a fixed alpha of 1.0f.
		float alpha = 0.5f;
		Gradient gradient = new Gradient ();
		gradient.SetKeys (
			new GradientColorKey[] { new GradientColorKey (thisColor, 0.0f), new GradientColorKey (thisColor, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey (alpha, 0f), new GradientAlphaKey (alpha / 100, 1f) }
		);
		tr.colorGradient = gradient;
	}

	void OnTriggerEnter (Collider other)
	{
		thisAgentNavMesh = this.GetComponent<NavMeshAgent> ();

//		if (other.gameObject.tag == "kill") {
//			Destroy (gameObject);
//			otherList.Remove (gameObject);
//			//Debug.Log (otherList.Count);

		if (other.gameObject.tag == "amenity") {
			StartCoroutine (killAfterTime());


		}

//		} else if (other.gameObject.tag != this.gameObject.tag) {
//			//draw nice things 
//		}
	}

	IEnumerator killAfterTime ()
	{
		//Debug.Log ("amen");
		thisAgentNavMesh.Stop();
		yield return new WaitForSeconds (timeAtAmenity);
		Destroy (gameObject);
		otherList.Remove (gameObject);
	}

	
	//	void Update ()
	//	{
	//		for (int i = 0; i < otherList.Count; i++) {
	//			var thisagent = otherList [i];
	//		}
	//
	//	}
	//
}
