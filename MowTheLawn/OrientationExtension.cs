using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawn
{
    public static class OrientationExtension
    {
        public static Orientation Right(this Orientation orientation)
        {
            switch(orientation)
            {
                case Orientation.N:
                    return Orientation.E;
                case Orientation.E:
                    return Orientation.S;
                case Orientation.S:
                    return Orientation.W;
                case Orientation.W:
                    return Orientation.N;
                default:
                    throw new Exception();
            }
        }

        public static Orientation Left(this Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.N:
                    return Orientation.W;
                case Orientation.E:
                    return Orientation.N;
                case Orientation.S:
                    return Orientation.E;
                case Orientation.W:
                    return Orientation.S;
                default:
                    throw new Exception();
            }
        }
    }
}
