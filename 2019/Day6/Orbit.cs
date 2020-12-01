using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    internal class Orbit
    {
        private List<Orbit> _subOrbitList;
        private long _distanceFromCom;
        public string Name { get; }

        public Orbit(string name, long distanceFromCom)
        {
            Name = name;
            _distanceFromCom = distanceFromCom;
            _subOrbitList = new List<Orbit>();
        }

        public static Orbit CreateComOrbit()
        {
            return new Orbit("COM", 0);
        }
        
        public void AddOrbit(string parentName, string orbitName)
        {
            if (Name == parentName)
            {
                _subOrbitList.Add(new Orbit(orbitName, _distanceFromCom + 1));
            }
            else
            {
                foreach (var orbit in _subOrbitList)
                {
                    orbit.AddOrbit(parentName, orbitName);
                }
            }
        }

        public long CalculateOrbit()
        {
            return _distanceFromCom + _subOrbitList.Sum(orbit => orbit.CalculateOrbit());
        }

        public List<string> FindOrbit(string searchPattern)
        {
            if (Name==searchPattern) return new List<string>(new[]{Name});
            foreach (var list in _subOrbitList.Select(orbit => orbit.FindOrbit(searchPattern)).Where(list => list != null))
            {
                list.Add(Name);
                return list;
            }

            return null;
        }
    }
}