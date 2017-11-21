using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spacedatacube : MonoBehaviour {

    // Use this for initialization

    public GameObject linecontainer;

    private fulltrip[] trips = new fulltrip[100];

	void Start () {

            for (int i = 0; i < 100; i++)
            {

            trips[i] = new fulltrip(linecontainer);
            }
        

	}
	
	// Update is called once per frame
	void Update () {

        foreach (fulltrip trip in trips)
        {
            trip.move();
        }
	}


    public class fulltrip
    {

        Vector3 start;
        Vector3 end;
        Vector3 next;
        Vector3 current;
        Vector3 []  paths = new Vector3[10];
        GameObject atom;
        GameObject linecont;

        int i ;
       
         public fulltrip(GameObject linecontainer)
        {
            linecont = linecontainer;

            this.start = new Vector3(Random.RandomRange(0, 10), 0, Random.RandomRange(0, 10));
//            this.end = new Vector3(Random.RandomRange(0, 10), 10, Random.RandomRange(0, 10));
            this.current = this.start;

            atom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            atom.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            atom.transform.position = this.start;

            fillthetrips();

            this.i = 0;
            createtheline();

        }

        public void createtheline()
        {
            GameObject line = new GameObject();
            line.transform.parent = linecont.transform;
            line.AddComponent<MeshFilter>();
            line.AddComponent<MeshRenderer>();

            Mesh mesh;
            mesh = new Mesh();
            int[] indicies = new int[10];
            List<Vector3> verticies = new List<Vector3>();
            List<Vector3> nomral = new List<Vector3>();
            
            for(int j = 0; j < 10; j++)
            {
                indicies[j] = j;
                verticies.Add(this.paths[j]);
                nomral.Add(new Vector3(0, 1, 0));
   

            }

            mesh.SetVertices(verticies);
            //mesh.SetNormals(nomral);
            mesh.SetIndices(indicies, MeshTopology.LineStrip,0);
            
            MeshFilter mf = line.GetComponent<MeshFilter>();
            mf.mesh = mesh;
            line.GetComponent<MeshRenderer>().material.name = "linestrip";
            line.GetComponent<MeshRenderer>().material.shader.name = "lines";
            
        }

        public void fillthetrips()
        {

            this.paths[0] = this.start;
            for(int j = 1; j < 10; j++)
            {
                this.paths[j] = new Vector3(Random.Range(0, 10), j, Random.RandomRange(0, 10));


            }
            this.next = this.paths[1];
        }
        

        public void  move()
        {
            this.current = Vector3.Lerp(this.start, this.next, (Time.time*0.9f%20)/20);
            atom.transform.position = this.current;

            if(Vector3.Distance(this.current,this.next) <0.01f)
            {
                if (this.i ==10)

                {
                    this.i = 0;
                    //Debug.Log("mpike");
                }
                this.next = this.paths[this.i+=1];
                this.start = this.current;
             
            }
        }
    }
}
