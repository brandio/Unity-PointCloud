using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Creats a mesh from .ply file
public class MeshImporter : MonoBehaviour {

    // File path for ply file to import
    public string plyFilePath = "C:/Users/Branden/Documents/Point Cloud Surface Reconstruction/Assets/Unity-PointCloud/mesh.ply";

    Vector3[] newVertices;
    int[] newTriangles;

    void CreateMesh()
    {
        GameObject meshObject = new GameObject();
        meshObject.transform.parent = this.transform;
        MeshFilter filter = meshObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer renderer = meshObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        Material material = new Material(Shader.Find("Standard"));

        Mesh mesh = new Mesh();
        filter.mesh = mesh;
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;

        renderer.material = material;
    }

    void LoadPly()
    {
        StreamReader reader = new StreamReader(plyFilePath);
        string line = reader.ReadLine();
        bool header = true;
        int numberVerts = 0;
        int numberTriangles = 0;
        int postHeaderIndex = 0;
        // Parse file to fill newTriangles and newVertices
        while (line != null)
        {
            
            if (header)
            {
                if(line.Contains("element vertex"))
                {
                    string[] stringArray = line.Split(null);
                    numberVerts = int.Parse(stringArray[2]);
                    newVertices = new Vector3[numberVerts];
                }
                else if(line.Contains("element face"))
                {
                    string[] stringArray = line.Split(null);
                    numberTriangles = int.Parse(stringArray[2]) * 3;
                    newTriangles = new int[numberTriangles];
                }
                else if (line == "end_header")
                {
                    header = false;
                }
            }
            else
            {
                // Vertex first
                if(postHeaderIndex < numberVerts)
                {
                    string[] stringArray = line.Split(null);

                    Vector3 vertex = new Vector3(float.Parse(stringArray[0]), float.Parse(stringArray[1]), float.Parse(stringArray[2]));
                    newVertices[postHeaderIndex] = vertex;
                }
                else
                {
                    string[] stringArray = line.Split(null);

                    newTriangles[(postHeaderIndex - numberVerts) * 3] = int.Parse(stringArray[1]);
                    newTriangles[(postHeaderIndex - numberVerts) * 3 + 1] = int.Parse(stringArray[3]);
                    newTriangles[(postHeaderIndex - numberVerts) * 3 + 2] = int.Parse(stringArray[5]);
                }
                postHeaderIndex++;
                if (postHeaderIndex > numberVerts + numberTriangles)
                {
                    break;
                }
            }
            line = reader.ReadLine();
        }
        Debug.Log(numberVerts + " " + numberTriangles);
    }
	// Use this for initialization
	void Start () {
        LoadPly();
        CreateMesh();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
