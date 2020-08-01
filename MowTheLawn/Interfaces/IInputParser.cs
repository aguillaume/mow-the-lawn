using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawn.Interfaces
{
    public interface IInputParser
    {
        void ParseInput(Queue<string> instructions, out Lawn lawn, out List<Mower> mowers);
    }
}
