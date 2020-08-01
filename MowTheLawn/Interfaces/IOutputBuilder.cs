using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawn.Interfaces
{
    public interface IOutputBuilder
    {
        string Getoutput(List<Mower> mowers);
    }
}
