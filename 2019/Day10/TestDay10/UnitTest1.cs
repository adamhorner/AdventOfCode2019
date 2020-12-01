using System;
using Day10;
using NUnit.Framework;

namespace TestDay10
{
    public class Tests
    {
        private Asteroid _asteroid = new Asteroid(0, 0);
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test120()
        {
            Assert.LessOrEqual(Math.Abs(120d - _asteroid.CalculateAngle(new Asteroid(26,15))), 0.02d);
        }

        [Test]
        public void Test030()
        {
            Assert.LessOrEqual(Math.Abs(30d - _asteroid.CalculateAngle(new Asteroid(15,-26))), 0.02d);
        }

        [Test]
        public void Test330()
        {
            Assert.LessOrEqual(Math.Abs(330d - _asteroid.CalculateAngle(new Asteroid(-15,-26))), 0.02d);
        }
        
        [Test]
        public void Test060()
        {
            Assert.LessOrEqual(Math.Abs(60d - _asteroid.CalculateAngle(new Asteroid(26,-15))), 0.02d);
        }

        [Test]
        public void Test150()
        {
            Assert.LessOrEqual(Math.Abs(150d- _asteroid.CalculateAngle(new Asteroid(15, 26))), 0.02d);
        }

        [Test]
        public void Test000()
        {
            Assert.AreEqual(0d, _asteroid.CalculateAngle(new Asteroid(0, -3)));
        }

        [Test]
        public void Test270()
        {
            Assert.AreEqual(270d, _asteroid.CalculateAngle(new Asteroid(-3, 0)));
        }

        [Test]
        public void Test045()
        {
            Assert.AreEqual(45d, _asteroid.CalculateAngle(new Asteroid(3, -3)));
        }

        [Test]
        public void Test135()
        {
            Assert.AreEqual(135d, _asteroid.CalculateAngle(new Asteroid(3, 3)));
        }

        [Test]
        public void Test225()
        {
            Assert.AreEqual(225d, _asteroid.CalculateAngle(new Asteroid(-3, 3)));
        }

        [Test]
        public void Test315()
        {
            Assert.AreEqual(315d, _asteroid.CalculateAngle(new Asteroid(-3, -3)));
        }

        [Test]
        public void Test090()
        {
            Assert.AreEqual(90d, _asteroid.CalculateAngle(new Asteroid(3, 0)));
        }

        [Test]
        public void Test180()
        {
            Assert.AreEqual(180d, _asteroid.CalculateAngle(new Asteroid(0, 3)));
        }
    }
}