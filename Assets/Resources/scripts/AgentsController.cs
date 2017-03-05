using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class AgentsController : MonoBehaviour
{

	private GameObject spwanerParent;
	private List <GameObject> agentsListInSpawner;
	private NavMeshAgent thisAgentNavMesh;
	private TrailRenderer trail;
	public float timeAtAmenity;


	private Color thisColor;


	void Start ()
	{

		spwanerParent = GameObject.Find ("Spawners");
		agentsListInSpawner = spwanerParent.GetComponent<AgentsSpawner> ().AgentsList; 

		thisColor = gameObject.GetComponent<Renderer> ().material.color;
		trail = GetComponent<TrailRenderer> ();
		trail.material = new Material (Shader.Find ("Sprites/Default"));

		// A simple 2 color gradient with a fixed alpha of 1.0f.
		float alpha = 0.5f;
		Gradient gradient = new Gradient ();
		gradient.SetKeys (
			new GradientColorKey[] { new GradientColorKey (thisColor, 0.0f), new GradientColorKey (thisColor, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey (alpha, 0f), new GradientAlphaKey (alpha / 100, 1f) }
		);
		trail.colorGradient = gradient;
	}

	void OnTriggerEnter (Collider other)
	{
		thisAgentNavMesh = this.GetComponent<NavMeshAgent> ();

		if (other.gameObject.tag == "amenity") {
			StartCoroutine (killAfterTime ());
		}
	}

	IEnumerator killAfterTime ()
	{
		thisAgentNavMesh.Stop ();
		yield return new WaitForSeconds (timeAtAmenity);
		Destroy (gameObject);
		agentsListInSpawner.Remove (gameObject);
	}

	

}
