using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawn.FileRepo
{
    public interface IFileRepository
    {
        void ParseInstructions(string filePath, out Lawn lawn, out List<Mower> movers);

        void ParseOutput(string filePath, List<Mower> movers);
    }
}
