using System;
using System.Collections.Generic;
using System.Linq;

namespace MowTheLawn
{
    public class Lawn
    {
        private readonly Coordinate _topRightCorner;
        public List<GridPoint> Grid = new List<GridPoint>();

        public Lawn(Coordinate topRightCorner)
        {
            _topRightCorner = topRightCorner ?? throw new ArgumentNullException(nameof(topRightCorner));

            for (int y = 0; y <= topRightCorner.Y; y++)
            {
                for (int x = 0; x <= topRightCorner.X; x++)
                {
                    Grid.Add(new GridPoint { Coordinate = new Coordinate(x, y) });
                }
            }
        }

        public bool IsInBounds(Coordinate coordinate)
        {
            return coordinate.X >= 0 && coordinate.X <= _topRightCorner.X &&
                coordinate.Y >= 0 && coordinate.Y <= _topRightCorner.Y;
        }
    }
}