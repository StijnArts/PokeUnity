using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.World
{
    public class Direction
    {
        public enum Directions
        {
            North = 0,
            North_East = 45,
            North_West = -45,
            East = 90,
            South = 180,
            South_East = 135,
            South_West = -135,
            West = -90
        }

        public static Directions ApproximateDirection(float angle)
        {
            var differences = new Dictionary<float, Directions>();
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                var difference = Math.Abs(angle - (int)direction);
                if (!differences.ContainsKey(difference))
                {
                    differences.Add(difference, direction);
                }
            }
            var smallestDifference = differences.Keys.Min();
            return differences[smallestDifference];
        }
    }
}
