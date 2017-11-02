using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoissonReconstruction : MonoBehaviour {

    PointOctree<PlyImporter.Point> octree = null;
	// The higher the octree depth the more detail of the surface
	public void reconstruct (List<PlyImporter.Point> points, int maxOctreeDepth = 6) {
        octree = new PointOctree<PlyImporter.Point>(2, new Vector3(0, 0, 0), 1, 12);
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
