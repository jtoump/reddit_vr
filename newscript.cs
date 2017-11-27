using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newscript : MonoBehaviour
{

    // Use this for initialization

    public GameObject linecontainer;
    public GameObject bringhere;
    public GameObject vertcont;
    public GameObject spherecont;

    private fulltrip[] trips = new fulltrip[400];

    void Start()
    {

        for (int i = 0; i < 400; i++)
        {

            trips[i] = new fulltrip(linecontainer, spherecont, vertcont);
        }

        computeexternalforces(trips);

        linecontainer.transform.parent = bringhere.transform;
        linecontainer.transform.localPosition = new Vector3(0, 0, 0);
        linecontainer.transform.localScale = new Vector3(1, 1, 1);




    }

    // Update is called once per frame
    void Update()
    {

        foreach (fulltrip trip in trips)
        {
            trip.move();
            //trip.checkthevertices();
            //trip.createtheline();

        }


        //computeexternalforces(trips);

    }


    public void computeexternalforces(fulltrip[] trips)
    {

        for (int fores = 0; fores < 4; fores++)
        {
            for (int round = 8; round > 0; round--)
            {
                for (int a = 0; a < trips.Length; a++)
                {
                    Vector3 movethis = trips[a].paths[round];

                    Vector3 sumforce = new Vector3();

                    for (int b = 0; b < trips.Length; b++)
                    {


                        if (a != b)
                        {

                            Vector3 basedonthat = trips[b].paths[round];


                            Vector3 d = basedonthat - movethis;

                            Vector3 direction = d.normalized;

                            float distance = d.magnitude + 0.1f;

                            if (distance < 1.0f)
                            {

                                movethis += 0.03f * direction / distance * 0.6f;
                                basedonthat -= 0.03f * direction / distance * 0.6f;
                            }

                            // movethis = Vector3.MoveTowards(movethis, basedonthat, 0.03f * 1 / distance);
                            //basedonthat = Vector3.MoveTowards(basedonthat, movethis, 0.03f * 1 / distance);

                            //movethis += direction / 0.5f * Mathf.Pow(distance, 2);
                            //basedonthat -= direction / 0.5f * Mathf.Pow(distance, 2);



                            //trips[a].fake[round].transform.localPosition = movethis;
                            //trips[a].paths[round] = Vector3.MoveTowards(movethis,sumforce,finaldist);
                            //trips[a].createtheline();
                            trips[a].paths[round] = movethis;


                            trips[b].paths[round] = basedonthat;
                            //trips[b].fake[round].transform.localPosition = basedonthat;
                            //trips[a].paths[round] = Vector3.MoveTowards(movethis,sumforce,finaldist);
                            //trips[b].createtheline();

                            //float dist = Vector3.Distance(movethis, basedonthat);
                            //Debug.Log(dist);


                            //    float force = 2 * Mathf.Log(dist);

                            //   sumforce += Vector3.MoveTowards(movethis, basedonthat, force * 0.1f);



                        }

                        //trips[b].computeforces();

                        //trips[b].createtheline();

                    }
                   // trips[a].computeforces();

                    trips[a].createtheline();
                    // Debug.Log(newvector +" " + movethis);

                    //trips[a].paths[round] = newvector;
                    //trips[a].fake[round].transform.localPosition = newvector;
                    ////trips[a].paths[round] = Vector3.MoveTowards(movethis,sumforce,finaldist);
                    //trips[a].createtheline();

                }
            }
        }

        //            for (int a = 0; a < trips.Length; a++)
        //            {

        //                for (int b = 0) ; b < trips.Length; base++)
        //        {
        //        }
        //    }
        //}













        //for (int round = 8; round > 1; round--)
        //{
        //    foreach (fulltrip checkthis in trips)

        //{

        //        Vector3 movethis = checkthis.paths[round];
        //        Vector3 sumforce = new Vector3(0,0,0);

        //        foreach (fulltrip trip in trips)
        //        {


        //            Vector3 basedonthat = trip.paths[round];
        //            float dist = Vector3.Distance(movethis, basedonthat);
        //            if (dist < 4.0f && dist>0.0f)
        //            {
        //                float force = 2 * Mathf.Log(dist);
        //            Debug.Log(force);
        //                sumforce +=  Vector3.MoveTowards(movethis, basedonthat, 1/dist);
        //            Debug.Log(sumforce);


        //        }



        //    }



        //    //    float disttothecenter = Vector3.Distance(movethis, sumforce);

        //    //    float forces = 2 * Mathf.Log(disttothecenter/1);
        //    //        Debug.Log(sumforce);


        //    //movethis = Vector3.MoveTowards(movethis,sumforce,forces*0.1f);
        //    //movethis += sumforce;

        //    checkthis.paths[round] = movethis+sumforce;
        //        //checkthis.computeforces();


        //        checkthis.createtheline();
        //    //checkthis.drawboxes();

        //    }


        //}




    }



    public class fulltrip
    {

        Vector3 start;
        Vector3 end;
        Vector3 next;
        Vector3 current;
        public Vector3[] paths = new Vector3[10];
        GameObject atom;
        GameObject linecont;
        GameObject spherecont;
        GameObject vertcont;
        float step;
        bool side;

        Color col;
        public GameObject[] fake = new GameObject[10];


        GameObject line;

        int i;

        public fulltrip(GameObject linecontainer, GameObject spherecontainer, GameObject vertcontainer)
        {

            vertcont = vertcontainer;
            spherecont = spherecontainer;


            //col = Random.ColorHSV();
            col = new Color(0.0f, 0.0f, 0.0f, 0.2f);
            linecont = linecontainer;

            line = new GameObject();
            line.transform.parent = linecont.transform;
            line.AddComponent<MeshFilter>();
            line.AddComponent<MeshRenderer>();

        

            this.start = new Vector3(Random.RandomRange(0.0f, 10.0f), 0, Random.RandomRange(0.0f, 10.0f));
            this.end = new Vector3(Random.RandomRange(0.0f, 10.0f), 10, Random.RandomRange(0.0f, 10.0f));
            this.current = this.start;
            this.step = 0;

            atom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            atom.transform.parent = spherecontainer.transform;
            atom.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            atom.transform.localPosition = this.start;

            oneline();
            //fillthetrips();

            //computeforces();

            createtheline();

        }


        public void computeforces()
        {


            for (int k = 8; k > 0; k--)
            {
                Vector3 movethis = this.paths[k];
                //Debug.Log(k+1);

                Vector3 basedonthat = this.paths[k + 1];





                float force = 2 * Mathf.Log(Vector3.Distance(movethis, basedonthat));

                movethis = Vector3.MoveTowards(movethis, basedonthat, force*0.4f);

                this.paths[k] = movethis;

            }

        }





        public void createtheline()
        {



            Mesh mesh;
            mesh = new Mesh();
            int[] indicies = new int[10];
            List<Vector3> verticies = new List<Vector3>();
            List<Vector3> nomral = new List<Vector3>();

            for (int j = 0; j < 10; j++)
            {
                indicies[j] = j;
                verticies.Add(this.paths[j]);


                nomral.Add(new Vector3(0, 1, 0));


            }

            mesh.SetVertices(verticies);
            //mesh.SetNormals(nomral);
            mesh.SetIndices(indicies, MeshTopology.LineStrip, 0);

            MeshFilter mf = line.GetComponent<MeshFilter>();
            mf.mesh = mesh;
            line.GetComponent<MeshRenderer>().material.name = "linestrip";
            line.GetComponent<MeshRenderer>().material.shader.name = "Standard";

            line.GetComponent<MeshRenderer>().material.color = col;
            //line.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");


            //col.a = 0.1f;
            //line.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", col);
        }


        public void oneline()
        {

            this.paths[0] = this.start;
            //GameObject fake2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //fake2.transform.parent = vertcont.transform;
            //fake2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //fake2.transform.position = this.start;
            //fake[0] = fake2;


            for (int j = 1; j < 9; j++)
            {
                this.paths[j] = new Vector3();
                this.paths[j] = Vector3.Lerp(this.start, this.end, j * 0.1f);
                //Debug.Log(this.paths[j].y);

                //GameObject fake1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                //fake1.transform.parent = vertcont.transform;
                //fake1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                //fake1.transform.position = this.paths[j];
                //fake[j] = fake1;




            }

           // this.next = this.paths[1];
            this.paths[9] = this.end;

            int rs = (int) Random.RandomRange(0, 8);
            this.start = this.paths[rs];
            this.current = this.paths[rs];
            this.next = this.paths[rs + 1];
            this.i = rs;


        }

        public void fillthetrips()
        {

            this.paths[0] = this.start;



            for (int j = 1; j < 9; j++)
            {
                this.paths[j] = new Vector3(Random.Range(0, 10), j, Random.RandomRange(0, 10));



            }
            this.paths[9] = this.end;

            int rs = (int)Random.RandomRange(0, 8);
            this.start = this.paths[rs];
            this.next = this.paths[rs+1];
        }


        public void move()


        {


            this.current = Vector3.Lerp(this.start, this.next,(this.step++%100)/100);
            atom.transform.localPosition = this.current;



            if (this.step==100)
            {
                if (this.i == 9)

                {
                    this.i = 0;
                    //Debug.Log("mpike");
                    this.start = this.paths[0];
                    this.next = this.paths[this.i += 1];
                }else
                {
                    this.next = this.paths[this.i += 1];
                    this.start = this.current;
                }
      
                this.step = 0.0f;

            }
        }

        public void drawboxes()
        {
            for (int j = 1; j < 10; j++)
            {

                GameObject trythis = GameObject.CreatePrimitive(PrimitiveType.Cube);
                trythis.transform.parent = linecont.transform;
                trythis.transform.localPosition = this.paths[j];
                trythis.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            }
        }



        public void checkthevertices()
        {


            for (int i = 1; i < fake.Length - 1; i++)
            {


                this.paths[i] = fake[i].transform.localPosition;
            }

        }




    }
}
