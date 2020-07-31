using System;
using System.Collections.Generic;
using System.Linq;

namespace MowTheLawn
{
    public class Mower : IEquatable<Mower>
    {
        private Lawn lawn;
        private Coordinate _nextMove;

        public string MowerCommands { get; set; }
        public Coordinate Position { get; set; }
        public Orientation Orientation { get; set; }
        public int Id { get; internal set; }
        public Queue<Command> CommandQueue = new Queue<Command>();
        //public Coordinate NextMove {
        //    get {
        //        if (_nextMove == null) _nextMove = Move();
        //        return _nextMove;
        //    }
        //}

        public Mower(int id, int x, int y, Orientation mowerOrientation, string mowerCommands, Lawn lawn)
        {
            Id = id;
            Position = new Coordinate(x, y);
            Orientation = mowerOrientation;
            MowerCommands = mowerCommands;
            this.lawn = lawn;

            foreach (var c in MowerCommands)
            {
                CommandQueue.Enqueue(Enum.Parse<Command>(c.ToString()));
            }
        }

        internal Move NextMove()
        {
            if (CommandQueue.Count == 0) return null;
            var command = CommandQueue.Dequeue();

            var move = new Move();
            switch (command)
            {
                case Command.L:
                    move.Orientation = Orientation.Left();
                    break;
                case Command.R:
                    move.Orientation = Orientation.Right();
                    break;
                case Command.F:
                    move.Coordinate = Forward();
                    break;
            }
            return move;
        }

        private Coordinate Forward()
        {
            switch (Orientation)
            {
                case Orientation.N:
                    return new Coordinate(Position.X.Value, Position.Y.Value + 1);
                case Orientation.E:
                    return new Coordinate(Position.X.Value + 1, Position.Y.Value);
                case Orientation.S:
                    return new Coordinate(Position.X.Value, Position.Y.Value - 1);
                case Orientation.W:
                    return new Coordinate(Position.X.Value - 1, Position.Y.Value);
                default:
                    throw new Exception();
            }
        }

        //public Coordinate TryNextMove()
        //{
        //    if (NextMove != null) Position = NextMove;
        //    _nextMove = null;
        //    return NextMove;
        //}

        public void Move(Move move)
        {
            if (move.Coordinate != null) Position = move.Coordinate;
            if (move.Orientation.HasValue) Orientation = move.Orientation.Value;
        }

        //private Coordinate MoveForward()
        //{
        //    Coordinate newPosition = null;
        //    switch(Orientation)
        //    {
        //        case Orientation.N:
        //            newPosition = new Coordinate(Position.X.Value, Position.Y.Value + 1);
        //            break;
        //        case Orientation.E:
        //            newPosition = new Coordinate(Position.X.Value + 1, Position.Y.Value);
        //            break;
        //        case Orientation.S:
        //            newPosition = new Coordinate(Position.X.Value, Position.Y.Value - 1);
        //            break;
        //        case Orientation.W:
        //            newPosition = new Coordinate(Position.X.Value - 1, Position.Y.Value);
        //            break;
        //        default:
        //            throw new Exception();
        //    }

        //    if (lawn.IsInBounds(newPosition) && !MowerCollision(newPosition))
        //    {
        //        return newPosition;
        //    }
        //    return null;
        //}

        //private bool MowerCollision(Coordinate coordinate)
        //{
        //    var mowers = lawn.GetMowersNear(coordinate);
        //    mowers.Remove(this);

        //    if (mowers.Count == 0) return false;
        //    var centerMower = mowers.Find(m => m.Position.Equals(coordinate));
        //    if (centerMower.NextMove == null) return true; //Mower Stationary we would hit it
        //    if (centerMower.NextMove == Position) return true; // Mowers swap places would hit eachother
        //    if (mowers.Any(m => m.NextMove == coordinate)) return true; //An other Mower would move to the same location.

        //    return false;
        //}

        public override bool Equals(object obj)
        {
            return Equals(obj as Mower);
        }

        public bool Equals(Mower other)
        {
            if (ReferenceEquals(other, null) || GetType() != other.GetType()) return false;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(Mower mower1, Mower mower2)
        {
            return EqualityComparer<Mower>.Default.Equals(mower1, mower2);
        }

        public static bool operator !=(Mower mower1, Mower mower2)
        {
            return !(mower1 == mower2);
        }
    }
}