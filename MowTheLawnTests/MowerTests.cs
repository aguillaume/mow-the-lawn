using MowTheLawn;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MowTheLawn.Enums;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Collections;

namespace MowTheLawnTests
{
    [TestFixture]
    public class MowerTests
    {

        [Test]
        public void MowerConstructor()
        {
            var commands = "FRLFRL";
            Coordinate position = new Coordinate(1, 1);
            var id = 1;
            var mower = new Mower(id, position.X.Value, position.Y.Value, Orientation.E, commands);

            Assert.AreEqual(1, mower.Id);
            Assert.AreEqual(position, mower.Position);
            Assert.AreEqual(Orientation.E, mower.Orientation);
            Assert.AreEqual(commands, mower.MowerCommands);
        }

        [Test]
        [TestCaseSource(typeof(TestCases), "NextMoveCases")]
        public void NextMove(Mower mower, Move expetedMove)
        {
            var move = mower.NextMove();

            Assert.AreEqual(expetedMove.Coordinate, move.Coordinate);
            Assert.AreEqual(expetedMove.Orientation, move.Orientation);
        }

        [Test]
        [TestCaseSource(typeof(TestCases), "MoveCases")]
        public void Move(Mower mower, Move move)
        {
            mower.Move(move);

            if (move.Coordinate != null)
            {
                Assert.AreEqual(move.Coordinate, mower.Position);
            }
            else
            {
                Assert.AreEqual(move.Orientation, mower.Orientation);
            }
        }

        private class TestCases
        {
            public static IEnumerable NextMoveCases
            {
                get
                {
                    var mower = new Mower(1, 1, 1, Orientation.N, "L");
                    var expectedMove = new Move
                    {
                        Coordinate = null,
                        Orientation = Orientation.W
                    };

                    yield return new TestCaseData(mower, expectedMove)
                        .SetDescription("Tests a left rotation");

                    mower = new Mower(1, 1, 1, Orientation.N, "R");
                    expectedMove = new Move
                    {
                        Coordinate = null,
                        Orientation = Orientation.E
                    };

                    yield return new TestCaseData(mower, expectedMove)
                        .SetDescription("Tests a right rotation");

                    mower = new Mower(1, 1, 1, Orientation.N, "F");
                    expectedMove = new Move
                    {
                        Coordinate = new Coordinate(1, 2),
                        Orientation = null
                    };

                    yield return new TestCaseData(mower, expectedMove)
                        .SetDescription("Tests a forward movement when facing north");

                    mower = new Mower(1, 1, 1, Orientation.E, "F");
                    expectedMove = new Move
                    {
                        Coordinate = new Coordinate(2, 1),
                        Orientation = null
                    };

                    yield return new TestCaseData(mower, expectedMove)
                        .SetDescription("Tests a forward movement when facing east");

                    mower = new Mower(1, 1, 1, Orientation.S, "F");
                    expectedMove = new Move
                    {
                        Coordinate = new Coordinate(1, 0),
                        Orientation = null
                    };

                    yield return new TestCaseData(mower, expectedMove)
                        .SetDescription("Tests a forward movement when facing south");

                    mower = new Mower(1, 1, 1, Orientation.W, "F");
                    expectedMove = new Move
                    {
                        Coordinate = new Coordinate(0, 1),
                        Orientation = null
                    };

                    yield return new TestCaseData(mower, expectedMove)
                        .SetDescription("Tests a forward movement when facing west");
                }
            }

            public static IEnumerable MoveCases
            {
                get
                {
                    var mower = new Mower(1, 1, 1, Orientation.N, "LRF");
                    var move = new Move
                    {
                        Coordinate = new Coordinate(1, 2),
                        Orientation = Orientation.N
                    };
                    yield return new TestCaseData(mower, move);

                    move = new Move
                    {
                        Coordinate = null,
                        Orientation = Orientation.E
                    };
                }
            }
        }
    }
}
