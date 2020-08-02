using System;
using System.Collections.Generic;
using System.Text;

namespace MowTheLawn.Interfaces
{
    public interface IFileRepository
    {
        Queue<string> GetInstructions(string filePath);
    }
}
