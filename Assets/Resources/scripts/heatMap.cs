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
	//count the pixels


	// Use this for initialization
	void Start ()
	{
		collisionMap (); 	
	}

	private void collisionMap ()
	{

		for (int x = 0; x < subDevisions; x++) {
			for (int y = 0; y < subDevisions; y++) {

				quad = GameObject.CreatePrimitive (PrimitiveType.Quad); //make cell cube  

				Destroy (quad.GetComponent <MeshCollider>());
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
//		for (int i = 0; i < heatMapPixels.Count; i++) {
//
//		}
//
//	}



}



