using System;
using System.Collections.Generic;
using System.Text;

namespace TrianglesApp
{
    public class Program
    {
        // This console app determines vertices for each triangle in the given problem domain.
        // It also determines a particular triangle when present with a set of vertices.
        // Assumption was made that a first quadrant number line would be used, with 0,0 in the lower left.
        // Unit tests would normally be used in a real application. This app simply solves the problem at hand.
        public static void Main(string[] args)
        {
            // Translate alpha rows, so that index position can be used
            var rows = new[] {"F", "E", "D", "C", "B", "A"};

            // Set up an object that will be used to store the output, and allow quick lookup
            // of the vertices for any particular triangle index.
            var output = new Dictionary<string, Dictionary<string, int[]>>();

            // Cycle through each row
            for (var i = 0; i < rows.Length; i++)
            {
                // Cycle through each column
                for (var j = 0; j < 12; j++)
                {
                    // Declare the vertices
                    int[] v1;
                    int[] v2;
                    int[] v3;

                    // Odd and even columns are calculated differently
                    // Determine which one we're dealing with and calculate
                    // the respective vertice
                    if (j%2 == 0)
                    {
                        v1 = new[] {j*5, i*10};
                        v2 = new[] {v1[0], v1[1]+10};
                        v3 = new[] {v1[0]+10, v1[1]};
                    }
                    else
                    {
                        v1 = new[] {(j+1)*5, (i + 1)*10};
                        v2 = new[] {v1[0], v1[1] - 10};
                        v3 = new[] {v1[0] - 10, v1[1]};
                    }

                    // Calculate the name of the triangle and use it as the index in the parent dictionary
                    var key = rows[i] + (j + 1);

                    // Add an entry to the parent dictionary
                    output.Add(key, new Dictionary<string, int[]>()
                    {
                        {"V(1)", v1 },
                        {"V(2)", v2 },
                        {"V(3)", v3 }
                    });
                }
            }

            // Cycle through each output value
            foreach (var o in output)
            {
                // Set up a string builder using string interpolation
                var result = new StringBuilder($"Triangle {o.Key}: ");

                // Cycle through each vertice for a particular triangle
                foreach (var v in o.Value)
                {
                    result.Append($"{v.Key}: ({v.Value[0]},{v.Value[1]}) ");
                }

                // Write the current triangle and its vertices to the console window
                Console.WriteLine(result);
            }

            // Go solve the problem of determining a triangle, given its vertices
            GetTriangles(output);

            // Do let the console window close before we want it to
            Console.ReadKey();
        }

        public static void GetTriangles(Dictionary<string, Dictionary<string, int[]>> data)
        {
            // Translate alpha rows, so that index position can be used
            var rows = new[] { "F", "E", "D", "C", "B", "A" };

            // Cycle through each triangle to get the vertices
            foreach (var d in data)
            {
                var vertices = d.Value;
                var row = string.Empty;
                var column = 0;

                // Calculations are different, depending on whether the V2 and V3 vertices are in
                // a negative direction from the V1 vertice
                if (vertices["V(3)"][0] < vertices["V(1)"][0])
                {
                    row = rows[(vertices["V(1)"][1] - 10) / 10];
                    column = vertices["V(1)"][0]/5;
                }
                else
                {
                    row = rows[vertices["V(1)"][1]/10];
                    column = (vertices["V(1)"][0] / 5) + 1;
                }

                // Combine the row and column to get the triangle name
                var result = row + column;

                // Show that the key of the original triangele passed in is the same as the
                // computed triangle name
                Console.WriteLine($"{d.Key} = {result}");
            }
        }
    }
}
