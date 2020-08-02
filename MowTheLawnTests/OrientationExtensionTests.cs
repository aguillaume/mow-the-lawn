using MowTheLawn.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawnTests
{
    [TestFixture]
    public class OrientationExtensionTests
    {
        [Test]
        [TestCase(Orientation.N, Orientation.E)]
        [TestCase(Orientation.E, Orientation.S)]
        [TestCase(Orientation.S, Orientation.W)]
        [TestCase(Orientation.W, Orientation.N)]
        public void RightRotation(Orientation startOrientation, Orientation expectedOrientation)
        {
            Assert.AreEqual(expectedOrientation, startOrientation.Right());
        }

        [Test]
        [TestCase(Orientation.N, Orientation.W)]
        [TestCase(Orientation.E, Orientation.N)]
        [TestCase(Orientation.S, Orientation.E)]
        [TestCase(Orientation.W, Orientation.S)]
        public void LeftRotation(Orientation startOrientation, Orientation expectedOrientation)
        {
            Assert.AreEqual(expectedOrientation, startOrientation.Left());
        }
    }
}
