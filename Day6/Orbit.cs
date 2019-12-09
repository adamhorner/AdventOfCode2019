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
            this._distanceFromCom = distanceFromCom;
            _subOrbitList = new List<Orbit>();
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
    }
}