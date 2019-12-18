namespace Day10
{
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