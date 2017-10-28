using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[RequireComponent(typeof(PointDisplayer))]
// Reads .ply files and creates a list of points
// Will only work with ply files formatted the way the python photogrammetry toolbox creates
public class PlyImporter : MonoBehaviour {

	// File path for ply file to import
	public string plyFilePath = "/home/branden/Desktop/models0/pmvs_options.txt.ply";

	PointDisplayer pointDisplayer;

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
		List<PointDisplayer.Point> points = new List<PointDisplayer.Point>();
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
				PointDisplayer.Point point = new PointDisplayer.Point(float.Parse(stringArray[0]),float.Parse(stringArray[1]),float.Parse(stringArray[2]),
										      float.Parse(stringArray[3]),float.Parse(stringArray[4]),float.Parse(stringArray[5]),
										      float.Parse(stringArray[6]),float.Parse(stringArray[7]),float.Parse(stringArray[8]));
				points.Add(point);

			}
			line = reader.ReadLine();
			
		}
		pointDisplayer.setPointCloud(points);
		reader.Close();
	}

	void Start () {
		pointDisplayer = this.GetComponent(typeof(PointDisplayer)) as PointDisplayer;
		readPly(plyFilePath);
	}
}
