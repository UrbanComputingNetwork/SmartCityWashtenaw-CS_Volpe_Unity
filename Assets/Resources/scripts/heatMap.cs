using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatMap : MonoBehaviour
{


	public int subDevisions;
	public float pixelSize;
	private GameObject quad;
	public GameObject heatMapParent;
	public static List<GameObject> heatMapPixels = new List<GameObject> ();

	private GameObject spwanerParent;
	private List <GameObject> agentsListInSpawner;
	private static List <int> heatmapCollidersList = new List<int> ();
	RaycastHit hit;





	// Use this for initialization
	void Start ()
	{
		spwanerParent = GameObject.Find ("Spawners");
		agentsListInSpawner = spwanerParent.GetComponent<AgentsSpawner> ().AgentsList; 
		collisionMap (); 	
	}

	private void collisionMap ()
	{

		for (int x = 0; x < subDevisions; x++) {
			for (int y = 0; y < subDevisions; y++) {

				quad = GameObject.CreatePrimitive (PrimitiveType.Quad); //make cell cube  

				Destroy (quad.GetComponent <MeshCollider> ());
				quad.transform.localScale = new Vector3 (pixelSize, pixelSize, pixelSize);
				quad.transform.parent = heatMapParent.transform; //put into parent object for later control 
				quad.transform.position = new Vector3 (x * pixelSize, 1000, y * pixelSize); //compensate for scale shift due to height
				quad.transform.Rotate (90, 90, 0); 
				quad.GetComponent<Renderer> ().material.color = Color.white;
				quad.AddComponent <BoxCollider> ();
				quad.GetComponent<BoxCollider> ().isTrigger = true; 

				heatMapPixels.Add (quad);
			}

		}

	}


//	void Update ()
//	{
//
//	
//		for (int i = 0; i < agentsListInSpawner.Count; i++) {
//			if (agentsListInSpawner[i].tag == "StoppedAgent" && Physics.Raycast (transform.position, Vector3.up, out hit, 2000)) {
//				print (agentsListInSpawner [i].GetInstanceID ().ToString () + " just hit " + hit.collider.name.ToString () + ": " + hit.transform.GetInstanceID ().ToString ());
//				heatmapCollidersList.Add (hit.transform.GetInstanceID ()); 
//			}
//
//
//		for (int i = 0; i < heatMapPixels.Count; i++) {
//			for (int n = 0; n < colliderListInAgents.Count; n++) {
//				print ("i  " + i + " n " + n); 
//				if (heatMapPixels [i].GetInstanceID () == colliderListInAgents [n]) {
//					quad.transform.position = new Vector3 (quad.transform.position.x, quad.transform.position.y+1 , quad.transform.position.z);
//
//				}
//			}
//		}
//	}

}
