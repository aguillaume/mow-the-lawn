using Microsoft.Extensions.DependencyInjection;
using Moq;
using MowTheLawn;
using MowTheLawn.Enums;
using MowTheLawn.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MowTheLawn.InversionOfControl;

namespace MowTheLawnTests
{
    [TestFixture]
    public class LawnMowerManagerParallelTests
    {

        [Test]
        public void RunMowersExerciseExample()
        {
            Lawn lawn = new Lawn(new Coordinate(5, 5));
            List<Mower> mowers = new List<Mower>
            {
                new Mower(1, 1, 2, Orientation.N, "LFLFLFLFF"),
                new Mower(2, 3, 3, Orientation.E, "FFRFFRFRRF")
            };
            var inputParserMock = new Mock<IInputParser>();
            inputParserMock.Setup(m => m.ParseInput(It.IsAny<Queue<string>>(), out lawn, out mowers));

            var instructions = new Queue<string>();
            instructions.Enqueue("5 5");
            instructions.Enqueue("1 2 N");
            instructions.Enqueue("LFLFLFLFF");
            instructions.Enqueue("3 3 E");
            instructions.Enqueue("FFRFFRFRRF");

            var expectedOutput = new List<Mower>
            {
                new Mower(1, 1, 3, Orientation.N, "LFLFLFLFF"),
                new Mower(2, 5, 1, Orientation.E, "FFRFFRFRRF")
            };

            var manager = new LawnMowerManagerParallel(inputParserMock.Object);
            var output = manager.RunMowers(instructions);

            for (int i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].Id, output[i].Id);
                Assert.AreEqual(expectedOutput[i].Position, output[i].Position);
                Assert.AreEqual(expectedOutput[i].Orientation, output[i].Orientation);
            } 
        }

        [Test]
        public void RunMowersCollisionExchange()
        {
            Lawn lawn = new Lawn(new Coordinate(1, 1));
            List<Mower> mowers = new List<Mower>
            {
                new Mower(1, 0, 0, Orientation.N, "RF"),
                new Mower(2, 1, 0, Orientation.N, "LF")
            };
            var inputParserMock = new Mock<IInputParser>();
            inputParserMock.Setup(m => m.ParseInput(It.IsAny<Queue<string>>(), out lawn, out mowers));

            var instructions = new Queue<string>();
            instructions.Enqueue("1 1");
            instructions.Enqueue("0 0 N");
            instructions.Enqueue("RF");
            instructions.Enqueue("1 0 N");
            instructions.Enqueue("LF");

            var expectedOutput = new List<Mower>
            {
                new Mower(1, 0, 0, Orientation.E, "RF"),
                new Mower(2, 1, 0, Orientation.W, "LF")
            };

            var manager = new LawnMowerManagerParallel(inputParserMock.Object);
            var output = manager.RunMowers(instructions);

            for (int i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].Id, output[i].Id);
                Assert.AreEqual(expectedOutput[i].Position, output[i].Position);
                Assert.AreEqual(expectedOutput[i].Orientation, output[i].Orientation);
            }
        }

        [Test]
        public void RunMowersMoveToMoveAwayNoCollision()
        {
            Lawn lawn = new Lawn(new Coordinate(1, 1));
            List<Mower> mowers = new List<Mower>
            {
                new Mower(1, 0, 0, Orientation.N, "RF"),
                new Mower(2, 1, 0, Orientation.E, "LF")
            };
            var inputParserMock = new Mock<IInputParser>();
            inputParserMock.Setup(m => m.ParseInput(It.IsAny<Queue<string>>(), out lawn, out mowers));

            var instructions = new Queue<string>();
            instructions.Enqueue("1 1");
            instructions.Enqueue("0 0 N");
            instructions.Enqueue("RF");
            instructions.Enqueue("1 0 E");
            instructions.Enqueue("LF");

            var expectedOutput = new List<Mower>
            {
                new Mower(1, 1, 0, Orientation.E, "RF"),
                new Mower(2, 1, 1, Orientation.N, "LF")
            };

            var manager = new LawnMowerManagerParallel(inputParserMock.Object);
            var output = manager.RunMowers(instructions);

            for (int i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].Id, output[i].Id);
                Assert.AreEqual(expectedOutput[i].Position, output[i].Position);
                Assert.AreEqual(expectedOutput[i].Orientation, output[i].Orientation);
            }
        }

        [Test]
        public void RunMowersAllCollisionsAtOnce()
        {
            Lawn lawn = new Lawn(new Coordinate(2, 2));
            List<Mower> mowers = new List<Mower>
            {
                new Mower(1, 0, 0, Orientation.E, "F"),
                new Mower(2, 1, 0, Orientation.W, "F"),
                new Mower(3, 0, 2, Orientation.N, "L"),
                new Mower(4, 0, 1, Orientation.N, "F"),
                new Mower(5, 1, 2, Orientation.E, "F"),
                new Mower(6, 2, 1, Orientation.N, "F"),
            };
            var inputParserMock = new Mock<IInputParser>();
            inputParserMock.Setup(m => m.ParseInput(It.IsAny<Queue<string>>(), out lawn, out mowers));

            var instructions = new Queue<string>();
            instructions.Enqueue("2 2");
            instructions.Enqueue("0 0 E");
            instructions.Enqueue("F");
            instructions.Enqueue("1 0 W");
            instructions.Enqueue("F");
            instructions.Enqueue("0 2 N");
            instructions.Enqueue("L");
            instructions.Enqueue("0 1 N");
            instructions.Enqueue("F");
            instructions.Enqueue("1 2 E");
            instructions.Enqueue("F");
            instructions.Enqueue("2 1 N");
            instructions.Enqueue("F");

            var expectedOutput = new List<Mower>
            {
                new Mower(1, 0, 0, Orientation.E, "F"),
                new Mower(2, 1, 0, Orientation.W, "F"),
                new Mower(3, 0, 2, Orientation.W, "L"),
                new Mower(4, 0, 1, Orientation.N, "F"),
                new Mower(5, 1, 2, Orientation.E, "F"),
                new Mower(6, 2, 1, Orientation.N, "F"),
            };

            var manager = new LawnMowerManagerParallel(inputParserMock.Object);
            var output = manager.RunMowers(instructions);

            for (int i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].Id, output[i].Id);
                Assert.AreEqual(expectedOutput[i].Position, output[i].Position);
                Assert.AreEqual(expectedOutput[i].Orientation, output[i].Orientation);
            }
        }


        Random r = new Random();


        
        [Test]
        [Ignore("Performance Test should not be run each time all tests are run")]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
        [TestCase(2000)]
        public void RunMowers_Performance(int size)
        {
            var instructions = new Queue<string>();
            var instructionStr = new StringBuilder();
            List<Mower> mowers = new List<Mower>();
            Lawn lawn = new Lawn(new Coordinate(size, size));

            var a = $"{size} {size}";
            instructionStr.AppendLine(a);
            instructions.Enqueue(a);
            for (int i = 0; i < r.Next(size / 2, size); i++)
            {
                var x = r.Next(size);
                var y = r.Next(size);
                Orientation o = (Orientation)r.Next(4);
                var b = $"{x} {y} {o}";
                instructionStr.AppendLine(b);
                instructions.Enqueue(b);
                var command = "";
                for (int j = 0; j < r.Next(size / 2, size); j++)
                {
                    command += (Command)r.Next(3);
                }
                instructionStr.AppendLine(command);
                instructions.Enqueue(command);
                mowers.Add(new Mower(i, x, y, o, command));
            }

            var inputParserMock = new Mock<IInputParser>();
            inputParserMock.Setup(m => m.ParseInput(It.IsAny<Queue<string>>(), out lawn, out mowers));

            Console.WriteLine(instructionStr);
            var manager = new LawnMowerManagerParallel(inputParserMock.Object);
            var output = manager.RunMowers(instructions);
            Console.WriteLine(output);
        }
    }
}
