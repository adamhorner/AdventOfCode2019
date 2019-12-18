namespace Day10
{
    public class Angle
    {
        public double Degrees { get; }

        public Angle(double degrees)
        {
            Degrees = degrees;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || ! this.GetType().Equals(obj.GetType())) 
            {
                return false;
            }
            else { 
                var other = (Angle) obj;
                return Degrees.Equals(other.Degrees);
            }   
        }

        public override int GetHashCode()
        {
            return Degrees.GetHashCode();
        }

        public override string ToString()
        {
            return "Degrees: " + Degrees;
        }

    }
}