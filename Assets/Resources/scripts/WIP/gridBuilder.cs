//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//
//
//
//public class gridBuilder : MonoBehaviour
//{
//
//	public int tableX;
//	public int tableY;
//	public int cellWorldSize;
//	public float cellShrink;
//	public int floorHeight;
//
//	private GameObject cube;
//	public GameObject textMeshPrefab;
//
//	public GameObject cityIOHolder; 
//	private Table Cells; 
//
//	public GameObject gridParent;
//	public static List<GameObject> gridObjects = new List<GameObject> ();
//	//new list!
//
//	public Color[] colors;
//
//	void Start()
//	{
//		Table Cells = cityIOHolder.GetComponent<cityIO>().Cells;
//	}
//
//
//
//	void drawTable ()
//	{
//		
//		foreach (Transform child in gridParent.transform) {
//			GameObject.Destroy (child.gameObject); // strat clean 
//			gridObjects.Clear (); // clean list!!!
//		}
//
//		for (int i = 0; i < Cells.grid.Count; i++) {
//
//			cube = GameObject.CreatePrimitive (PrimitiveType.Cube); //make cell cube  
//			cube.transform.parent = gridParent.transform; //put into parent object for later control 
//			cube.transform.position = new Vector3 ((Cells.grid [i].x * cellWorldSize), 0, (Cells.grid [i].y * cellWorldSize)); //compensate for scale shift due to height
//
//
//			for (int n = -10; n < Cells.objects.density.Count; n++) { //go through all 'densities' to match Type to Height 
//				if (Cells.grid [i].type == n) {
//
//					if (Cells.grid [i].type >= 0) { //if this cell is one of the buildings types
//
//						cube.transform.localScale = new Vector3 (cellShrink * cellWorldSize, (Cells.objects.density [n] * floorHeight), cellShrink * cellWorldSize);
//						cube.transform.position = new Vector3 (cube.transform.position.x, (Cells.objects.density [n] * floorHeight) / 2, cube.transform.position.z); //compensate for scale shift and x,y array
//						cube.AddComponent<NavMeshObstacle> ();
//						cube.GetComponent<NavMeshObstacle> ().carving = true; 
//						cube.GetComponent<Renderer> ().material.color = colors [Cells.grid [i].type];
//
//						cube.tag = "amenity"; // to become attractor 
//
//
//					} else { //if road, green or other non building 
//
//						cube.transform.position = new Vector3 (cube.transform.position.x, -5, cube.transform.position.z); //hide base plates 
//						cube.transform.localScale = new Vector3 (cellShrink * cellWorldSize, 1, cellShrink * cellWorldSize);
//						cube.GetComponent<Renderer> ().material.color = Color.black;
//					}
//
//				}
//			}
//
//			gridObjects.Add (cube); //add this Gobj to list
//		}
//
//		//meshTextType (); call if you need type text float 
//
//	}
//
//	private void meshTextType ()
//	{
//
//		for (int i = 0; i < Cells.grid.Count; i++) {
//			//make the text 
//			GameObject textMesh = GameObject.Instantiate (textMeshPrefab, new Vector3 ((Cells.grid [i].x * cellWorldSize), 
//				                      cube.transform.localScale.y + floorHeight, (Cells.grid [i].y * cellWorldSize)), 
//				                      cube.transform.rotation, transform) as GameObject; //spwan prefab text
//
//			textMesh.GetComponent<TextMesh> ().text = Cells.grid [i].type.ToString ();
//			textMesh.GetComponent<TextMesh> ().fontSize = 150;
//			textMesh.GetComponent<TextMesh> ().color = Color.gray;
//			textMesh.GetComponent<TextMesh> ().characterSize = .3f; 	
//			textMesh.transform.parent = cube.transform;
//
//		}
//	}
//}
//
