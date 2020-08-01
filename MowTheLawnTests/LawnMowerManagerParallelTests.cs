using MowTheLawn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawnTests
{
    [TestFixture]
    public class LawnMowerManagerParallelTests
    {
        [Test]
        public void RunMowers()
        {
            var instructions = new Queue<string>();
            instructions.Enqueue("5 5");
            instructions.Enqueue("1 2 N");
            instructions.Enqueue("LFLFLFLFF");
            instructions.Enqueue("3 3 E");
            instructions.Enqueue("FFRFFRFRRF");

            var expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("1 3 N");
            expectedOutput.AppendLine("5 1 E");

            var manager = new LawnMowerManagerParallel("");
            var output = manager.RunMowers();

            Assert.AreEqual(expectedOutput.ToString(), output);
        }

        [Test]
        public void RunMowers2()
        {
            var instructions = new Queue<string>();
            instructions.Enqueue("1 1");
            instructions.Enqueue("0 0 N");
            instructions.Enqueue("RF");
            instructions.Enqueue("1 0 N");
            instructions.Enqueue("LF");

            var expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("0 0 E");
            expectedOutput.AppendLine("1 0 W");

            var manager = new LawnMowerManagerParallel("");
            var output = manager.RunMowers();

            Assert.AreEqual(expectedOutput.ToString(), output);
        }

        [Test]
        public void RunMowers3()
        {
            var instructions = new Queue<string>();
            instructions.Enqueue("1 1");
            instructions.Enqueue("0 0 N");
            instructions.Enqueue("RF");
            instructions.Enqueue("1 0 E");
            instructions.Enqueue("LF");

            var expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("1 0 E");
            expectedOutput.AppendLine("1 1 N");

            var manager = new LawnMowerManagerParallel("");
            var output = manager.RunMowers();

            Assert.AreEqual(expectedOutput.ToString(), output);
        }

        [Test]
        public void RunMowers4()
        {
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

            var expectedOutput = new StringBuilder();
            expectedOutput.AppendLine("0 0 E");
            expectedOutput.AppendLine("1 0 W");
            expectedOutput.AppendLine("0 2 W");
            expectedOutput.AppendLine("0 1 N");
            expectedOutput.AppendLine("1 2 E");
            expectedOutput.AppendLine("2 1 N");

            var manager = new LawnMowerManagerParallel("");
            var output = manager.RunMowers();

            Assert.AreEqual(expectedOutput.ToString(), output);
        }


        Random r = new Random();


        [Test]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
        [TestCase(2000)]
        public void RunMowers_Performance(int size)
        {
            var instructions = new Queue<string>();
            var instructionStr = new StringBuilder();

            var a = $"{size} {size}";
            instructionStr.AppendLine(a);
            instructions.Enqueue(a);
            for (int i = 0; i < r.Next(size/2, size); i++)
            {
                var b = $"{r.Next(size)} {r.Next(size)} {(Orientation)r.Next(4)}";
                instructionStr.AppendLine(b);
                instructions.Enqueue(b);
                var command = "";
                for (int j = 0; j < r.Next(size/2, size); j++)
                {
                    command += (Command)r.Next(3);
                }
                instructionStr.AppendLine(command);
                instructions.Enqueue(command);
            }

            Console.WriteLine(instructionStr);
            var manager = new LawnMowerManagerParallel("");
            var output = manager.RunMowers();
            Console.WriteLine(output);
        }
    }
}
