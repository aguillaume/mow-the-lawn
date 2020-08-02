using MowTheLawn.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MowTheLawn
{
    public class OutputBuilder : IOutputBuilder
    {
        public string GetOutput(List<Mower> mowers)
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