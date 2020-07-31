using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MowTheLawn
{
    public class OutputParser
    {
        public OutputParser()
        {
        }

        public string ParseOutput(List<Mower> mowers)
        {
            var result = new StringBuilder();
            mowers.OrderBy(m => m.Id);
            foreach (var mower in mowers)
            {
                result.AppendLine($"{mower.Position} {mower.Orientation}");
            }
            return result.ToString();
        }
    }
}