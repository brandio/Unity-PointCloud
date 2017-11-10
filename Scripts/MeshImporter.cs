using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Creats a mesh from .ply file
public class MeshImporter : MonoBehaviour {

    // File path for ply file to import
    public string plyFilePath = "/home/branden/Desktop/models0/pmvs_options.txt.ply";

    public Vector3[] newVertices;
    public int[] newTriangles;

    void CreateMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
    }

    void LoadPly()
    {
        StreamReader reader = new StreamReader(plyFilePath);
        string line = reader.ReadLine();
        bool header = true;
        while (line != null)
        {
            if (header)
            {
                if (line == "end_header")
                {
                    header = false;
                }
            }
            else
            {
                string[] stringArray = line.Split(null);
                if (stringArray.Length != 6)
                    continue;
                //Point point = new Point(float.Parse(stringArray[0]), float.Parse(stringArray[1]), float.Parse(stringArray[2]),
                                              float.Parse(stringArray[3]), float.Parse(stringArray[4]), float.Parse(stringArray[5]),
                                              float.Parse(stringArray[6]), float.Parse(stringArray[7]), float.Parse(stringArray[8]));
                //points.Add(point);
            }
            line = reader.ReadLine();

        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
