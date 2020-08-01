using MowTheLawn.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;

namespace MowTheLawn.FileRepo
{
    public class FileRepository : IFileRepository
    {
        public Queue<string> GetInstructions(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException($"No file exists at the provided path: {filePath}");
            var result = new Queue<string>();
            using(var file = new StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    result.Enqueue(line);
                }
            }
            return result;
        }

        public void ParseOutput(string filePath, List<Mower> movers)
        {
            throw new NotImplementedException();
        }
    }
}
