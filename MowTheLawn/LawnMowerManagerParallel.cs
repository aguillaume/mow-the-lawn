using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MowTheLawn
{
    public class LawnMowerManagerParallel
    {
        public string RunMowers(Queue<string> instructions)
        {
            var inputParser = new InputParser();
            inputParser.ParseInput(instructions, out Lawn lawn, out List<Mower> mowers);

            var maxInstructions = mowers.Select(m => m.MowerCommands.Length).Max();

            for (int i = 0; i < maxInstructions; i++)
            {
                var moves = new ConcurrentDictionary<Mower, Move>();

                Parallel.ForEach(mowers, mower =>
                {
                    var move = mower.NextMove();
                    if ((move == null) || // Mower instructions finished.
                        (move.Coordinate != null && !lawn.IsInBounds(move.Coordinate))) // mower advanced out of bounds
                        return;
                    moves.TryAdd(mower, move);
                });

                List<Mower> mowersInCollision = CheckForCollisions(mowers, moves);

                Parallel.ForEach(moves, move =>
                {
                    if (mowersInCollision.Contains(move.Key)) return;
                    move.Key.Move(move.Value);
                });
            }

            var outputParser = new OutputParser();
            return outputParser.ParseOutput(mowers);
        }

        private List<Mower> CheckForCollisions(List<Mower> mowers, ConcurrentDictionary<Mower, Move> moves)
        {
            // Mowers going to same location
            var sameLocationCollision = moves.AsParallel()
                .Where(m => m.Value.Coordinate != null)
                .GroupBy(m => m.Value.Coordinate, m => m.Key)
                .Where(g => g.Count() > 1)
                .SelectMany(c => c.Select(m => m));

            // Mowers moving into statinary mower or Mowers Swapping
            var intoStationaryCollision = moves.AsParallel()
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
                })
                .Select(m => m.Key);

            var mowersInCollision = sameLocationCollision.Union(intoStationaryCollision).ToList();
            return mowersInCollision;
        }
    }
}