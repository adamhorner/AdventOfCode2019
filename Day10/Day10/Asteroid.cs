using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace Day10
{
    internal class Asteroid
    {
        public int X { get; }
        public int Y { get; }
        private Dictionary<Angle, int> VisibleAngles { get; }

        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
            VisibleAngles = new Dictionary<Angle, int>();
        }

        private Angle CalculateAngle(Asteroid other)
        {
            double angle;
            if (other.X == X)
                angle = Math.PI / 2 * ((other.Y > Y) ? 1 : -1);
            else if (other.Y == Y)
                angle = (other.X > X) ? 0 : Math.PI;
            else
                angle = Math.Tan((other.Y - Y) / (double)(other.X - X));
            return new Angle(angle, X > other.X);
        }

        public void AssessAsteroid(Asteroid other)
        {
            var angle = CalculateAngle(other);
            if (VisibleAngles.ContainsKey(angle))
            {
                VisibleAngles[angle]++;
            }
            else
            {
                VisibleAngles[angle] = 1;
            }
        }

        public int GetVisibleAsteroidCount()
        {
            return VisibleAngles.Count;
        }

        public override string ToString()
        {
            return
                $"Asteroid ({X},{Y}) can see {VisibleAngles.Count} unique angles total {VisibleAngles.Values.Sum()} asteroids.";
        }
    }

    public class Angle
    {
        public double Radians { get; }
        public bool IsXPositive { get; }

        public Angle(double radians, bool isXPositive)
        {
            Radians = radians;
            IsXPositive = isXPositive;
        }

        public override bool Equals(object? obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || ! this.GetType().Equals(obj.GetType())) 
            {
                return false;
            }
            else { 
                var other = (Angle) obj;
                return IsXPositive == other.IsXPositive && Radians.Equals(other.Radians);
            }   
        }

        public override int GetHashCode()
        {
            return (Radians.GetHashCode() << 2) ^ (IsXPositive?1:0);
        }

        public override string ToString()
        {
            return "X" + (IsXPositive ? "+" : "-") + "ve, Radians: " + Radians;
        }
    }
}