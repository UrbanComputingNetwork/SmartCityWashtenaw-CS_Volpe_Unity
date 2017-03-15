using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class AgentsController : MonoBehaviour
{

	private GameObject _spwanerParent;
	private List <GameObject> _agentsListInSpawner;
	private NavMeshAgent _thisAgentNavMesh;
	private TrailRenderer _trail;
	public float _timeAtAmenity;
	private Color _thisColor;
	public int _dieAfter = 150;



	void Start ()
	{

		_spwanerParent = GameObject.Find ("Spawners"); 
		_agentsListInSpawner = _spwanerParent.GetComponent<SpawnersController> ().AgentsList; // get the agents list from thier spawning parent 

		_thisColor = gameObject.GetComponent<Renderer> ().material.color;
		_trail = GetComponent<TrailRenderer> ();
		_trail.material = new Material (Shader.Find ("Sprites/Default"));

		// A simple 2 color gradient with a fixed alpha of 1.0f.
		float alpha = 0.5f;
		Gradient gradient = new Gradient ();
		gradient.SetKeys (
			new GradientColorKey[] { new GradientColorKey (_thisColor, 0.0f), new GradientColorKey (_thisColor, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey (alpha, 0f), new GradientAlphaKey (alpha / 100, 1f) }
		);
		_trail.colorGradient = gradient;
		Destroy (gameObject, _dieAfter); 
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "amenity") {
			_thisAgentNavMesh = this.GetComponent<NavMeshAgent> ();
			StartCoroutine (killAfterTime ());

		}
	}

	IEnumerator killAfterTime () // killed after x time at target with Tag y 
	{
		_thisAgentNavMesh.Stop ();
		if (this.GetComponent<NavMeshAgent> ().velocity.x < 1 && this.GetComponent<NavMeshAgent> ().velocity.z < 1) { //really stopped 
			_thisAgentNavMesh.tag = "StoppedAgent"; 
		}
		yield return new WaitForSeconds (_timeAtAmenity);
		Destroy (gameObject);
		_agentsListInSpawner.Remove (gameObject);

	}

}

