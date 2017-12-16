using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Reads .ply files and creates a list of points
public class PlyImporter : MonoBehaviour {

    public class Point
    {
        public Point(float xx, float yy, float zz, float nxx, float nyy, float nzz, float rr, float gg, float bb)
        {
            int scale = 10;
            x = xx * scale;
            y = yy * scale;
            z = zz * scale;
            nX = nxx;
            nY = nyy;
            nZ = nzz;
            r = rr;
            g = gg;
            b = bb;
        }
        public float x;
        public float y;
        public float z;

        public float nX;
        public float nY;
        public float nZ;

        public float r;
        public float g;
        public float b;
    }

    // File path for ply file to import
    public string plyFilePath = "/home/branden/Desktop/models0/pmvs_options.txt.ply";

	PointDisplayer pointDisplayer;
    PoissonReconstruction surfaceReconstructor;
	public void readPly(string path)
	{
		StreamReader reader = new StreamReader(plyFilePath);

		// Make sure file is formatted corectly
		string line = reader.ReadLine();
		if(line != "ply")
		{
			Debug.LogError("This is not a correctly formatted ply file!!!");
			return;
		}
		
		bool header = true;
		List<Point> points = new List<Point>();
		while(line != null)
		{
			if(header)
			{
				if(line == "end_header")
				{
					header = false;
				}
			}
			else
			{
				string[] stringArray = line.Split(null);
				if(stringArray.Length != 9)
					continue;
				Point point = new Point(float.Parse(stringArray[0]),float.Parse(stringArray[1]),float.Parse(stringArray[2]),
										      float.Parse(stringArray[3]),float.Parse(stringArray[4]),float.Parse(stringArray[5]),
										      float.Parse(stringArray[6]),float.Parse(stringArray[7]),float.Parse(stringArray[8]));
				points.Add(point);
			}
			line = reader.ReadLine();
			
		}
        if(pointDisplayer != null)
        {
            pointDisplayer.setPointCloud(points);
        }
        if(surfaceReconstructor != null)
        {
            //surfaceReconstructor.reconstruct(points, 6);
        }
		reader.Close();
	}

	void Start () {
		pointDisplayer = this.GetComponent(typeof(PointDisplayer)) as PointDisplayer;
        surfaceReconstructor = this.GetComponent(typeof(PoissonReconstruction)) as PoissonReconstruction;
        readPly(plyFilePath);
	}
}
