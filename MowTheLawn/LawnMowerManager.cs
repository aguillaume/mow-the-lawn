using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MowTheLawn
{
    public class LawnMowerManager
    {
        public string RunMowers(Queue<string> instructions)
        {
            var inputParser = new InputParser();
            inputParser.ParseInput(instructions, out Lawn lawn, out List<Mower> mowers);
            AddMowersToLawn(lawn, mowers);

            var maxInstructions = mowers.Select(m => m.MowerCommands.Length).Max();

            for (int i = 0; i < maxInstructions; i++)
            {
                var moves = new Dictionary<Mower, Move>();

                foreach (var mower in mowers)
                {
                    var move = mower.NextMove();
                    if ((move == null) || // Mower instructions finished.
                        (move.Coordinate != null && !lawn.IsInBounds(move.Coordinate))) // mower advanced out of bounds
                            continue;
                    moves.Add(mower, move);
                }

                // Check for collisions
                // Mowers going to same location
                var sameLocationCollision = moves
                    .Where(m => m.Value.Coordinate != null)
                    .GroupBy(m => m.Value.Coordinate, m => m.Key)
                    .Where(g => g.Count() > 1)
                    .SelectMany(c => c.Select(m => m));

                // Mowers moving into statinary mower or Mowers Swapping
                var intoStationaryCollision = moves
                    .Where(m => 
                        {
                            if (m.Value.Coordinate != null && mowers.Select(x => x.Position).Contains(m.Value.Coordinate))
                            {
                                var mowerInTheWay = mowers.Find(a => a.Position.Equals(m.Value.Coordinate));
                                if (!moves.Where(b => b.Value.Coordinate != null).Select(b => b.Key).Contains(mowerInTheWay)) return true; // Stationary >> Collision
                                else
                                {
                                    var otherMowerMove = moves[mowerInTheWay];
                                    if (otherMowerMove.Coordinate.Equals(m.Key.Position)) return true; // Moving into eachother >> Collision
                                    return false; // Other Mower moved away >> No Collision
                                }
                            }

                            return false;
                        }
                    )
                    .Select(m => m.Key);

                var mowersInCollision = sameLocationCollision.Union(intoStationaryCollision).ToList();
                
                foreach (var move in moves)
                {
                    if (mowersInCollision.Contains(move.Key)) continue;
                    move.Key.Move(move.Value);
                }

                //UpdateMowersLocationOnLawn(lawn, moves);
            }

            var outputParser = new OutputParser();
            return outputParser.ParseOutput(mowers);
        }

        private void UpdateMowersLocationOnLawn(Lawn lawn, Dictionary<Mower, Coordinate> moves)
        {
            foreach (var move in moves)
            {
                lawn.Grid.Find(g => move.Key.Equals(g.Mower)).Mower = null;
                lawn.Grid.Find(g => move.Value.Equals(g.Coordinate)).Mower = move.Key;
            }
        }

        private void AddMowersToLawn(Lawn lawn, List<Mower> mowers)
        {
            foreach (var mower in mowers)
            {
                lawn.Grid.Find(g => mower.Position.Equals(g.Coordinate)).Mower = mower;
            }
        }
    }
}
