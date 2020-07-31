using System;
using System.Collections.Generic;
using System.Linq;

namespace MowTheLawn
{
    public class Mower : IEquatable<Mower>
    {
        public Queue<Command> CommandQueue = new Queue<Command>();

        public string MowerCommands { get; set; }
        public Coordinate Position { get; set; }
        public Orientation Orientation { get; set; }
        public int Id { get; }

        public Mower(int id, int x, int y, Orientation mowerOrientation, string mowerCommands)
        {
            Id = id;
            Position = new Coordinate(x, y);
            Orientation = mowerOrientation;
            MowerCommands = mowerCommands;

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

        public void Move(Move move)
        {
            if (move.Coordinate != null) Position = move.Coordinate;
            if (move.Orientation.HasValue) Orientation = move.Orientation.Value;
        }

        #region IEquatable
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
        #endregion IEquatable
    }
}