from pypoisson import poisson_reconstruction
from ply_from_array import points_normals_from, ply_from_array
import sys

filename = sys.argv[1]
filename = 'horse_with_normals.xyz'
print (filename)

output_file = "reconstruction2.ply"

#Helper Function to read the xyz-normals point cloud file
points, normals = points_normals_from(filename)

faces, vertices = poisson_reconstruction(points, normals, depth=10)

#Helper function to save mesh to PLY Format
ply_from_array(vertices, faces, output_file=output_file)


