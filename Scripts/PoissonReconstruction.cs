using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoissonReconstruction : MonoBehaviour {

    PointOctree<PlyImporter.Point> octree = null;
	// The higher the octree depth the more detail of the surface
	public void reconstruct (List<PlyImporter.Point> points, float maxOctreeDepth = 6) {
        float maxSize = .0001f;
        float minOctreeSize = Mathf.Sqrt(3) * Mathf.Sqrt(maxSize * maxSize * Mathf.Pow(2.71828f,(-2 * maxOctreeDepth)));
        Debug.Log(minOctreeSize);
        octree = new PointOctree<PlyImporter.Point>(maxSize, new Vector3(0, 0, 0), minOctreeSize, (int)maxOctreeDepth);
        foreach (PlyImporter.Point point in points)
        {
            octree.Add(point, new Vector3(point.x, point.y, point.z));
        }
    }

    void OnDrawGizmos()
    {
        
        if(octree != null)
        {
            octree.DrawAllBounds();
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}
