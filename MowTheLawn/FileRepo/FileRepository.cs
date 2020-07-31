using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawn.FileRepo
{ 
    public class FileRepository : IFileRepository
    {
        public void ParseInstructions(string filePath, out Lawn lawn, out List<Mower> movers)
        {
            throw new NotImplementedException();
        }

        public void ParseOutput(string filePath, List<Mower> movers)
        {
            throw new NotImplementedException();
        }
    }
}
