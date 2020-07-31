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

        #endregion Positive Tests

        #region Negative Tests
        [Test]
        public void NullTopRightCoordiate()
        {
            Assert.Throws<ArgumentNullException>(() => new Lawn(null));
        }

        [Test]
        [TestCase("")]
        [TestCase("X")]
        [TestCase("XY")]
        [TestCase("X YZ")]
        public void InvalidTopRightCoordiate(string topRightCoordinate)
        {
            Assert.Throws<ArgumentException>(() => new Lawn(null));
        }
        #endregion Negative Tests




    }
}
    