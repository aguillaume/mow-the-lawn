using MowTheLawn.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MowTheLawn
{
    public class InputParser : IInputParser
    {
        private Regex topRightValidator = new Regex(@"^(\d+) (\d+)$", RegexOptions.Compiled);
        private Regex mowerPositionValidator = new Regex(@"^(\d+) (\d+) ([NESW])$", RegexOptions.Compiled);
        private Regex mowerMovementValidator = new Regex(@"^[LRF]*$", RegexOptions.Compiled);

        public void ParseInput(Queue<string> instructions, out Lawn lawn, out List<Mower> mowers)
        {
            if (instructions == null) throw new ArgumentNullException(nameof(instructions), "Cannot be null");

            var currentItem = instructions.Dequeue();
            var topRightMatch = topRightValidator.Match(currentItem);
            if (!topRightMatch.Success) throw new Exception($"{currentItem} is not a valid top right coordinate. It should be in this format: 'X Y'");
            var x = int.Parse(topRightMatch.Groups[1].Value);
            var y = int.Parse(topRightMatch.Groups[2].Value);
            var topRightCoordinate = new Coordinate(x, y);
            lawn = new Lawn(topRightCoordinate);

            if (instructions.Count % 2 != 0) throw new Exception("Mower instructions must be in pairs");
            mowers = new List<Mower>();
            var mowerOrder = 0;
            while (instructions.Count > 0)
            {
                mowerOrder++;
                currentItem = instructions.Dequeue();
                var mowerPositionMatch = mowerPositionValidator.Match(currentItem);
                if (!mowerPositionMatch.Success) throw new Exception($"{currentItem} is not a valid mower start position. It must be in this format: 'X Y O'");
                var mowerX = int.Parse(mowerPositionMatch.Groups[1].Value);
                var mowerY = int.Parse(mowerPositionMatch.Groups[2].Value);
                var mowerOrientation = Enum.Parse<Orientation>(mowerPositionMatch.Groups[3].Value);
                if (mowers.Select(m => m.Position).Any(P => P.Equals(new Coordinate(x, y)))) throw new Exception($"{currentItem} is not a valid starting position. There is already an other Mower at this position.");

                currentItem = instructions.Dequeue();
                var mowerMovementMatch = mowerMovementValidator.Match(currentItem);
                if (!mowerMovementMatch.Success) throw new Exception($"{currentItem} is not a valid list of mower commands. It must only contain L, R or F commands.");

                var mowerCommands = mowerMovementMatch.Value;
                mowers.Add(new Mower(mowerOrder, mowerX, mowerY, mowerOrientation, mowerCommands));
            }
        }
    }
}
