using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cityIO : MonoBehaviour
{

	public string url = "http://45.55.73.103/table/citymatrix_volpe";
	public int delayWWW;
	private WWW www;
	private string cityIOtext;
	private string cityIOtext_Old;
	//this one look for changes

	public int tableX;
	public int tableY;


	/// <summary>
	/// class start
	/// </summary>
	/// 
	/// 

	private RootObject Cells;

	[System.Serializable]

	public class Grid
	{
		public int type;
		public int x;
		public int y;
		public int rot;
	}

	public class Objects
	{
		public int slider1;
		public int toggle1;
		public int toggle2;
		public int toggle3;
		public int toggle4;
		public int dockID;
		public int dockRotation;
		public int IDMax;
		public List<int> density;
		public int pop_young;
		public int pop_mid;
		public int pop_old;
	}

	public class RootObject
	{
		public List<Grid> grid;
		public Objects objects;
	}

	//	public cityIO(){
	//
	//	}
	/// <summary>
	/// class end
	/// </summary>

	public GameObject gridParent;
	public float cellScale;
	public static List<GameObject> gridObjects = new List<GameObject> ();
	//new list!

	IEnumerator Start ()
	{
		while (true) {

			WWW www = new WWW (url);
			yield return www;
			yield return new WaitForSeconds (delayWWW);
			cityIOtext = www.text; //just a cleaner Var

			if (cityIOtext != cityIOtext_Old) 
			{
				jsonHandler ();
			}
		}

	}


	void jsonHandler ()

	{
		print ("Table Updated" + '\n' + "New data from CityIO server: " + '\n' + cityIOtext);
	
		Cells = JsonUtility.FromJson<RootObject> (cityIOtext); // get parsed JSON into Cells variable --- MUST BE BEFORE CALLING ANYTHING FROM CELLS!!

		for (int i = 0; i < Cells.grid.Count; i++) {

			//print (" X: " + Cells.grid [i].x + " Y: " + Cells.grid [i].y + " Type: " + Cells.grid [i].type);
			
		}

		foreach (Transform child in gridParent.transform) {
			GameObject.Destroy (child.gameObject); // strat clean 
		}

		int counter = 0;
		for (int x = 0; x < tableX; x++) {
			for (int y = 0; y < tableY; y++) {			

				counter++; 

//					GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//					sphere.transform.position = new Vector3 (x, Cells.grid[counter].type, y);
//					sphere.transform.parent = gridParent.transform;

				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cube.transform.position = new Vector3 (x, 0, y);
				cube.transform.localScale = new Vector3 (cellScale, (cube.transform.localScale.y), cellScale); 
				cube.transform.parent = gridParent.transform;


				gridObjects.Add (cube);

			}
		}

		//print ("total objects in grid: " + gridObjects.Count); 

		cityIOtext_Old = cityIOtext; //new data has arrived from server 

	}
}

