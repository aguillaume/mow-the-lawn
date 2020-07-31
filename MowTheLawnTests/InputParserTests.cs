using MowTheLawn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawnTests
{
    [TestFixture]
    public class InputParserTests
    {
        #region Positive Tests

        [Test]
        public void ReadFile()
        {
            // Have I got the file in memory
        }
        
        [Test]
        public void LawnSize()
        {
            // Check the parser deremines/generates the correct sirface size grid
            var instructions = new Queue<string>();
            instructions.Enqueue("5 5");
            instructions.Enqueue("1 2 N");
            instructions.Enqueue("LFLFLFLFF");
            instructions.Enqueue("3 3 E");
            instructions.Enqueue("FFRFFRFRRF");

            var inputParser = new InputParser();
            inputParser.ParseInput(instructions, out Lawn lawnm, out List<Mower> mowers);

        }

        #endregion Positive Tests


        #region Negative Tests

        [Test]
        public void NullInstructions()
        {
            var inputParser = new InputParser();
            var error = Assert.Throws<ArgumentNullException>(() => inputParser.ParseInput(null, out Lawn lawnm, out List<Mower> mowers));
            Assert.AreEqual("Cannot be null\r\nParameter name: instructions", error.Message);
        }

        [Test]
        public void EmptyInstructions()
        {
            var emptyInstruction = new Queue<string>();
            var inputParser = new InputParser();
           var error = Assert.Throws<ArgumentException>(() => inputParser.ParseInput(emptyInstruction, out Lawn lawnm, out List<Mower> mowers));
            Assert.AreEqual("Must contain at least the Top Right Coordinate Location and one set of mover instructions; made of mower position & orientation and list of mower movements\r\nParameter name: instructions", error.Message);
        }

        #endregion Negative Tests
    }
}
