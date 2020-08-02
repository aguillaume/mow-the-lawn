using MowTheLawn;
using MowTheLawn.Enums;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawnTests
{
    [TestFixture]
    public class InputParserTests
    {
        #region Positive Tests
        
        [Test]
        public void ParseFileCorrectly()
        {
            var instructions = new Queue<string>();
            instructions.Enqueue("5 5");
            instructions.Enqueue("1 2 N");
            instructions.Enqueue("LFLFLFLFF");
            instructions.Enqueue("3 3 E");
            instructions.Enqueue("FFRFFRFRRF");

            var inputParser = new InputParser();
            inputParser.ParseInput(instructions, out Lawn lawn, out List<Mower> mowers);

            Assert.IsNotNull(lawn);
            Assert.IsNotNull(mowers);
            Assert.AreEqual(36, lawn.Grid.Count);
            Assert.AreEqual(2, mowers.Count);

            var mower1 = mowers[0];
            Assert.AreEqual(1, mower1.Id);
            Assert.AreEqual("LFLFLFLFF", mower1.MowerCommands);
            Assert.AreEqual(Orientation.N, mower1.Orientation);
            Assert.AreEqual(new Coordinate(1, 2), mower1.Position);

            var mower2 = mowers[1];
            Assert.AreEqual(2, mower2.Id);
            Assert.AreEqual("FFRFFRFRRF", mower2.MowerCommands);
            Assert.AreEqual(Orientation.E, mower2.Orientation);
            Assert.AreEqual(new Coordinate(3, 3), mower2.Position);
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

        [Test]
        [TestCase("55")]
        [TestCase("5  5")]
        [TestCase("5")]
        [TestCase("5 X")]
        [TestCase("5x 5y")]
        [TestCase("asdasd")]
        [TestCase("")]
        public void TopRightValidationError(string invalidTopRight)
        {
            var instruction = new Queue<string>();
            instruction.Enqueue(invalidTopRight);
            var inputParser = new InputParser();
            var error = Assert.Throws<Exception>(() => inputParser.ParseInput(instruction, out Lawn lawnm, out List<Mower> mowers));
            Assert.AreEqual($"{invalidTopRight} is not a valid top right coordinate. It should be in this format: 'X Y'", error.Message);
        }

        [Test]
        [TestCaseSource(typeof(TestCases), "PartialMowerInstructions")]
        public void IncompleteMowerInstructions(Queue<string> instructions)
        {
            var inputParser = new InputParser();
            var error = Assert.Throws<Exception>(() => inputParser.ParseInput(instructions, out Lawn lawnm, out List<Mower> mowers));
            Assert.AreEqual($"Mower instructions must be in pairs", error.Message);
        }

        [Test]
        [TestCase("55N")]
        [TestCase("5  5  N")]
        [TestCase("5N")]
        [TestCase("5 X")]
        [TestCase("5x 5y No")]
        [TestCase("asdasd")]
        [TestCase("x y o")]
        [TestCase("")]
        public void MowerPositionValidationError(string invalidMowerPosition)
        {
            var instruction = new Queue<string>();
            instruction.Enqueue("3 3");
            instruction.Enqueue(invalidMowerPosition);
            instruction.Enqueue("FFFFFLR");
            var inputParser = new InputParser();
            var error = Assert.Throws<Exception>(() => inputParser.ParseInput(instruction, out Lawn lawnm, out List<Mower> mowers));
            Assert.AreEqual($"{invalidMowerPosition} is not a valid mower start position. It must be in this format: 'X Y O'", error.Message);
        }

        [Test]
        public void MowerPositionDuplicateError()
        {
            var duplicateMowerPosition = "2 2 N";
            var instruction = new Queue<string>();
            instruction.Enqueue("3 3");
            instruction.Enqueue(duplicateMowerPosition);
            instruction.Enqueue("FFFFFLR");
            instruction.Enqueue(duplicateMowerPosition);
            instruction.Enqueue("FFFFFLR");

            var inputParser = new InputParser();
            var error = Assert.Throws<Exception>(() => inputParser.ParseInput(instruction, out Lawn lawnm, out List<Mower> mowers));
            Assert.AreEqual($"{duplicateMowerPosition} is not a valid starting position. There is already an other Mower at this position.", error.Message);
        }

        [Test]
        public void MowerPositionDuplicateDifferentOrientationError()
        {
            var instruction = new Queue<string>();
            instruction.Enqueue("3 3");
            instruction.Enqueue("2 2 N");
            instruction.Enqueue("FFFFFLR");
            instruction.Enqueue("2 2 W");
            instruction.Enqueue("FFFFFLR");

            var inputParser = new InputParser();
            var error = Assert.Throws<Exception>(() => inputParser.ParseInput(instruction, out Lawn lawnm, out List<Mower> mowers));
            Assert.AreEqual("2 2 W is not a valid starting position. There is already an other Mower at this position.", error.Message);
        }

        [Test]
        [TestCase("55N")]
        [TestCase("F F F F R")]
        [TestCase("LRLRL FF")]
        public void MowerCommandsValidationError(string invalidMowerCommands)
        {
            var instruction = new Queue<string>();
            instruction.Enqueue("3 3");
            instruction.Enqueue("0 0 N");
            instruction.Enqueue(invalidMowerCommands);
            var inputParser = new InputParser();
            var error = Assert.Throws<Exception>(() => inputParser.ParseInput(instruction, out Lawn lawnm, out List<Mower> mowers));
            Assert.AreEqual($"{invalidMowerCommands} is not a valid list of mower commands. It must only contain L, R or F commands.", error.Message);
        }


        #endregion Negative Tests

        private class TestCases
        {
            public static IEnumerable PartialMowerInstructions
            {
                get
                {
                    var mowerStartingPositionOnly = new Queue<string>();
                    mowerStartingPositionOnly.Enqueue("3 3");
                    mowerStartingPositionOnly.Enqueue("1 2");
                    yield return new TestCaseData(mowerStartingPositionOnly);

                    var mowerThirdStartingPositionOnly = new Queue<string>();
                    mowerThirdStartingPositionOnly.Enqueue("3 3");
                    mowerThirdStartingPositionOnly.Enqueue("1 2");
                    mowerThirdStartingPositionOnly.Enqueue("LLFLLR");
                    mowerThirdStartingPositionOnly.Enqueue("0 0");
                    mowerThirdStartingPositionOnly.Enqueue("FFRRFFL");
                    mowerThirdStartingPositionOnly.Enqueue("2 2");
                    yield return new TestCaseData(mowerThirdStartingPositionOnly);
                }
            }
        }
    }
}
