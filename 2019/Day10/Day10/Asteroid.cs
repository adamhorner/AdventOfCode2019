using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace Day10
{
    public class Asteroid
    {
        public int X { get; }
        public int Y { get; }
        private Dictionary<double, List<Asteroid>> VisibleAngles { get; }
        private double LaserAngle { get; set; }
        private int LaserHits { get; set; }

        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
            VisibleAngles = new Dictionary<double, List<Asteroid>>();
            LaserAngle = -0.1d;
            LaserHits = 0;
        }

        public double CalculateAngle(Asteroid other)
        {
            // this is radians based on zero being horizontal to the right (x+ve, y=0)
            double radians;
            if (other.X == X)
                radians = Math.PI / 2d * ((other.Y > Y) ? 1 : -1);
            else if (other.Y == Y)
                radians = (other.X > X) ? 0d : Math.PI;
            else
                radians = Math.Atan((other.Y - Y) / (double)(other.X - X));
            // convert to degrees based on zero being up (x=0, y-ve)
            //TODO fix the maths below
            var degrees = radians * 180d / Math.PI;
            if (other.X < X && other.Y != Y) degrees += 180;
            degrees += 90d;
            degrees += 360d;
            degrees %= 360d;
            if (degrees < 0) throw new Exception($"Calculated degrees as a negative value: {degrees}");
            return degrees;
        }

        public void AssessAsteroid(Asteroid other)
        {
            var angle = CalculateAngle(other);
            if (VisibleAngles.ContainsKey(angle))
            {
                VisibleAngles[angle].Add(other);
            }
            else
            {
                VisibleAngles[angle] = new List<Asteroid>(){other};
            }
        }

        public int GetVisibleAsteroidCount()
        {
            return VisibleAngles.Count;
        }

        public override string ToString()
        {
            return
                $"Asteroid ({X},{Y}) can see {VisibleAngles.Count} unique angles total {VisibleAngles.Values.Count} asteroids.";
        }

        public (double, Asteroid) FireLaser()
        {
            // laser starts at 0 degrees with no hits
            // laser must increase its angle, then fire at the closest asteroid on the next available angle
            // if there are no more asteroids to shoot, then return an appropriate error message
            var asteroidCount = VisibleAngles.Values.SelectMany(a => a).ToList().Count;
            if (asteroidCount == 0)
                return (-1, null);
            // Console.WriteLine($"{asteroidCount} asteroids remaining to choose from");
            var nextAngle = VisibleAngles
                .Select(angle => angle)
                .Where(angle => angle.Key > LaserAngle && angle.Value.Count > 0)
                .OrderBy(angle=> angle.Key)
                .FirstOrDefault();
            if (nextAngle.Value == null || nextAngle.Value.Count == 0)
            {
                // in this case we have no more angles with asteroids in to destroy, start another loop around
                Console.WriteLine("Starting another loop around");
                LaserAngle = -0.1d;
                //recurse
                return FireLaser();
            }

            var asteroidHit = nextAngle.Value
                .Select(asteroid => asteroid)
                .OrderBy(asteroid => asteroid.DistanceFrom(this))
                .First();
            VisibleAngles[nextAngle.Key].Remove(asteroidHit);
            LaserAngle = nextAngle.Key;
            return (nextAngle.Key, asteroidHit);
        }

        private double DistanceFrom(Asteroid other)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(this.X - other.X), 2) + Math.Pow(Math.Abs(this.Y - other.Y),2));
        }
    }
}