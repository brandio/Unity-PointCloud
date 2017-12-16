using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creats a new gameobject to display points

[RequireComponent(typeof(PointDisplayer))]
public class PointDisplayer : MonoBehaviour {

	const int MAX_POINTS_IN_MESH = 1;

    IEnumerator MakeMeshes(List<PlyImporter.Point> points)
    {
        int numMeshes = points.Count / MAX_POINTS_IN_MESH;
        Material mat = new Material(Shader.Find("Particles/Multiply"));
        for (int i = 0; i < numMeshes; i++)
        {
            GameObject meshObject = new GameObject();
            meshObject.transform.parent = this.transform;
            MeshFilter filter = meshObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
            MeshRenderer renderer = meshObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            Mesh mesh = filter.mesh;

            int numberPoints = Mathf.Min(points.Count - (i * MAX_POINTS_IN_MESH), MAX_POINTS_IN_MESH);
            Vector3[] verts = new Vector3[numberPoints];
            Vector3[] normals = new Vector3[numberPoints];
            Color[] colors = new Color[numberPoints];
            int[] indices = new int[numberPoints];
            for (int ii = 0; ii < numberPoints; ii++)
            {
                int pntIndex = ii + i * MAX_POINTS_IN_MESH;
                verts[ii] = new Vector3(points[pntIndex].x, points[pntIndex].y, points[pntIndex].z);
                normals[ii] = new Vector3(points[pntIndex].nX, points[pntIndex].nY, points[pntIndex].nZ);
                colors[ii] = new Color(points[pntIndex].r / 100, points[pntIndex].g / 100, points[pntIndex].b / 100);
                indices[ii] = ii;
            }
            mesh.vertices = verts;
            mesh.colors = colors;
            mesh.normals = normals;
            mesh.SetIndices(indices, MeshTopology.Points, 0);

            renderer.material = mat;
            
            if (i % 5000 == 0)
            {
                yield return new WaitForEndOfFrame();
                Debug.Log(i + " " + points.Count);
            }
                
        }
    }
	public void setPointCloud(List<PlyImporter.Point> points)
	{
        StartCoroutine("MakeMeshes", points);
	}
}
