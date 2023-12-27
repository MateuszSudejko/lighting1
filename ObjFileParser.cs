using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lighting1
{
    public static class ObjFileParser
    {
        public static float[] ExtractFaceVertices(string filePath)
        {
            List<float> faceVertices = new();
            List<Vector3> vertices = new();
            //try
            //{
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.StartsWith("v "))
                {
                    string[] parts = line.Split(' ');

                    parts = parts.Where(val => val != "").ToArray();

                    vertices.Add(new Vector3(float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3])));
                }
                if (line.StartsWith("f "))
                {
                    string[] parts = line.Split(' ');

                    parts = parts.Where(val => val != "").ToArray();

                    float[] normal = CalcNormal(vertices[int.Parse(parts[1]) - 1], vertices[int.Parse(parts[2]) - 1], vertices[int.Parse(parts[3]) - 1]);

                    for(int i = 3; i < parts.Length; i++)
                    {
                        faceVertices.Add(vertices[int.Parse(parts[1]) - 1].X);
                        faceVertices.Add(vertices[int.Parse(parts[1]) - 1].Y);
                        faceVertices.Add(vertices[int.Parse(parts[1]) - 1].Z);
                        faceVertices.Add(normal[0]);
                        faceVertices.Add(normal[1]);
                        faceVertices.Add(normal[2]);

                        faceVertices.Add(vertices[int.Parse(parts[i-1]) - 1].X);
                        faceVertices.Add(vertices[int.Parse(parts[i-1]) - 1].Y);
                        faceVertices.Add(vertices[int.Parse(parts[i-1]) - 1].Z);
                        faceVertices.Add(normal[0]);
                        faceVertices.Add(normal[1]);
                        faceVertices.Add(normal[2]);

                        faceVertices.Add(vertices[int.Parse(parts[i]) - 1].X);
                        faceVertices.Add(vertices[int.Parse(parts[i]) - 1].Y);
                        faceVertices.Add(vertices[int.Parse(parts[i]) - 1].Z);
                        faceVertices.Add(normal[0]);
                        faceVertices.Add(normal[1]);
                        faceVertices.Add(normal[2]);
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error reading the OBJ file: " + ex.Message);
            //}

            faceVertices.Add(faceVertices.Count);

            return faceVertices.ToArray();
        }

        public static float[] CalcNormal(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            float[] ret = new float[3];
            float[] vec1 = new float[3], vec2 = new float[3];
            const int x = 0;
            const int y = 1;
            const int z = 2;

            vec1[x] = v1.X - v2.X;
            vec1[y] = v1.Y - v2.Y;
            vec1[z] = v1.Z - v2.Z;

            vec2[x] = v2.X - v3.X;
            vec2[y] = v2.Y - v3.Y;
            vec2[z] = v2.Z - v3.Z;

            ret[x] = vec1[y] * vec2[z] - vec1[z] * vec2[y];
            ret[y] = vec1[z] * vec2[x] - vec1[x] * vec2[z];
            ret[z] = vec1[x] * vec2[y] - vec1[y] * vec2[x];

            float sqr = (float)Math.Sqrt((double)(ret[x] * ret[x] + ret[y] * ret[y] + ret[z] * ret[z]));

            ret[x] = ret[x]/sqr;
            ret[y] = ret[y] / sqr;
            ret[z] = ret[z] / sqr;

            return ret;
        }
    }

}
