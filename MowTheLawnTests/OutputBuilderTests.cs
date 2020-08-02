using MowTheLawn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawnTests
{
    [TestFixture]
    public class OutputBuilderTests
    {
        [Test]
        public void GetOutput()
        {
            var mowers = new List<Mower>
            {
                new Mower(1, 1, 1, MowTheLawn.Enums.Orientation.E, "LRF")
            };

            var outputBuilder = new OutputBuilder();
            var output = outputBuilder.GetOutput(mowers);

            Assert.AreEqual("1 1 E\r\n", output);
        }
    }
}
