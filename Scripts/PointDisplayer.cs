using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointDisplayer))]
public class PointDisplayer : MonoBehaviour {

	const int MAX_POINTS_IN_MESH = 65000;
	public void setPointCloud(List<PlyImporter.Point> points)
	{
		int numMeshes = points.Count/MAX_POINTS_IN_MESH;
		// I dont think this will ever be the case so just log and return for now
		if(numMeshes > 1)
		{
			Debug.LogError("Too many points for one mesh!!!");
			return;
		}

		GameObject meshObject = new GameObject();
		meshObject.transform.parent = this.transform;
		MeshFilter filter = meshObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		MeshRenderer renderer = meshObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		Mesh mesh = filter.mesh;

		Vector3[] verts = new Vector3[points.Count];
		Vector3[] normals = new Vector3[points.Count];
		Color[] colors = new Color[points.Count];
		int[] indices = new int[points.Count];
		for(int i = 0; i < points.Count; i++)
		{	
			verts[i] = new Vector3(points[i].x,points[i].y,points[i].z);
			normals[i] = new Vector3(points[i].nX,points[i].nY,points[i].nZ);
			colors[i] = new Color(points[i].r/100,points[i].g/100,points[i].b/100);

			indices[i] = i;
		}
		mesh.vertices = verts;
		mesh.colors = colors;
		mesh.normals = normals;
		mesh.SetIndices(indices,MeshTopology.Points,0);

		Material mat = new Material(Shader.Find("Particles/Multiply"));
		renderer.material = mat;	
	}
}
