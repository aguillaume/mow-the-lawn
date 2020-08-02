using MowTheLawn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawnTests
{
    [TestFixture]
    public class LawnTests
    {
        #region Positive Tests

        [Test]
        public void LawnSize()
        {
            var lawn = new Lawn(new Coordinate(2, 4));
            Assert.AreEqual(15, lawn.Grid.Count);
        }

        [Test]
        [TestCase(1, 1, true)]
        [TestCase(0, 0, true)]
        [TestCase(5, 5, true)]
        [TestCase(6, 5, false)]
        [TestCase(5, 6, false)]
        [TestCase(-1, 5, false)]
        public void IsInBounds(int x, int y, bool expectedResult)
        {
            var lawn = new Lawn(new Coordinate(5, 5));
            Assert.AreEqual(expectedResult, lawn.IsInBounds(new Coordinate(x, y)));
        }

        #endregion Positive Tests

        #region Negative Tests
        [Test]
        public void NullTopRightCoordiate()
        {
            Assert.Throws<ArgumentNullException>(() => new Lawn(null));
        }

        #endregion Negative Tests




    }
}
    