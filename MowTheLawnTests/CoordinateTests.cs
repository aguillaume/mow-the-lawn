using MowTheLawn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawnTests
{
    [TestFixture]
    public class CoordinateTests
    {
        #region Positive Tests

        [Test]
        public void ConstructorTwoInts()
        {
            int expectedX = 1;
            int expectedY = 5;
            var coordinate = new Coordinate(expectedX, expectedY);

            Assert.IsTrue(coordinate.X.HasValue);
            Assert.AreEqual(expectedX, coordinate.X);
            Assert.IsTrue(coordinate.Y.HasValue);
            Assert.AreEqual(expectedY, coordinate.Y);
        }

        [Test]
        public void ToStringOversite()
        {
            int expectedX = 1;
            int expectedY = 5;
            var coordinate = new Coordinate(expectedX, expectedY);

            Assert.AreEqual($"{expectedX} {expectedY}", coordinate.ToString());
        }


        [Test]
        public void CoordinateEquals()
        {
            int expectedX = 1;
            int expectedY = 5;
            var coordinate1 = new Coordinate(expectedX, expectedY);
            var coordinate2 = new Coordinate(expectedX, expectedY);

            Assert.IsTrue(coordinate1.Equals(coordinate2));
            Assert.IsTrue(coordinate2.Equals(coordinate1));
            Assert.IsTrue(coordinate2.Equals((object)coordinate1));
        }

        [Test]
        public void CoordinateEqualsOperator()
        {
            int expectedX = 1;
            int expectedY = 5;
            var coordinate1 = new Coordinate(expectedX, expectedY);
            var coordinate2 = new Coordinate(expectedX, expectedY);

            Assert.IsTrue(coordinate1 == coordinate2);
            Assert.IsTrue(coordinate2 == coordinate1);
        }

        [Test]
        public void CoordinateNotEquals()
        {
            var coordinate1 = new Coordinate(1, 2);
            var coordinate2 = new Coordinate(3, 4);

            Assert.IsFalse(coordinate1.Equals(coordinate2));
            Assert.IsFalse(coordinate2.Equals(coordinate1));
            Assert.IsFalse(coordinate1.Equals(null));
            Assert.IsFalse(coordinate2.Equals(null));
        }

        [Test]
        public void CoordinateNotEqualsOperator()
        {
            var coordinate1 = new Coordinate(1, 2);
            var coordinate2 = new Coordinate(3, 4);

            Assert.IsTrue(coordinate1 != coordinate2);
            Assert.IsTrue(coordinate2 != coordinate1);
        }

        #endregion Positive Tests

        #region Negative Tests



        #endregion Negative Tests
    }
}
