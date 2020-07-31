using System;
using System.Collections.Generic;
using System.Linq;

namespace MowTheLawn
{
    public class Lawn
    {
        private Coordinate topRightCorner;
        public List<GridPoint> Grid = new List<GridPoint>();

        public Lawn(Coordinate topRightCorner)
        {
            if (topRightCorner == null) throw new ArgumentNullException(nameof(topRightCorner));
            if (!topRightCorner.X.HasValue || !topRightCorner.Y.HasValue)
                throw new ArgumentException(nameof(topRightCorner));

            this.topRightCorner = topRightCorner;
            for (int y = 0; y <= topRightCorner.Y; y++)
            {
                for (int x = 0; x <= topRightCorner.X; x++)
                {
                    Grid.Add(new GridPoint { Coordinate = new Coordinate(x, y) } );
                }
            }
        }

        public bool IsInBounds(Coordinate coordinate)
        {
            return coordinate.X >= 0 && coordinate.X <= topRightCorner.X &&
                coordinate.Y >= 0 && coordinate.Y <= topRightCorner.Y;
        }

        public List<Mower> GetMowersNear(Coordinate coordinate)
        {
            return Grid.FindAll(g =>
                {
                    return (g.Coordinate.X == coordinate.X && g.Coordinate.Y == coordinate.Y) || // Centre 
                        (g.Coordinate.X == coordinate.X && g.Coordinate.Y == coordinate.Y + 1) || // North
                        (g.Coordinate.X == coordinate.X + 1 && g.Coordinate.Y == coordinate.Y) || //East
                        (g.Coordinate.X == coordinate.X && g.Coordinate.Y == coordinate.Y - 1) || // South
                        (g.Coordinate.X == coordinate.X - 1 && g.Coordinate.Y == coordinate.Y); // West
                })
                .Select(g => g.Mower)
                .Where(m => m != null).ToList();
        }
    }
}