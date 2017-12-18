using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Displays a point cloud

[RequireComponent(typeof(ParticleSystem))]
public class PointDisplayer : MonoBehaviour {

    // Size of points
    public float pointSize = 0.01f;
    
    bool bPointsUpdated = false;
    ParticleSystem.Particle[] cloud;

    /* This method displays the point cloud using a mesh.
       It seems better to use a particle system so this isnt used anymore but keep just in case
       we need it for something */
    const int MAX_POINTS_IN_MESH = 65000;
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

	public void SetPointCloud(List<PlyImporter.Point> points)
	{
        cloud = new ParticleSystem.Particle[points.Count];

        for (int i = 0; i < points.Count; i++)
        {
            PlyImporter.Point point = points[i];
            cloud[i].position = new Vector3(point.x, point.y, point.z);
            cloud[i].startColor = new Color(point.r / 100, point.g / 100, point.b / 100);
            cloud[i].startSize = pointSize;
        }
        bPointsUpdated = true;
    }

    void Update()
    {
        if (bPointsUpdated)
        {
            ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
            particleSystem.SetParticles(cloud, cloud.Length);
            bPointsUpdated = false;
        }
    }
}
