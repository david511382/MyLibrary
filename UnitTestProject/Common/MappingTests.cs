using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommon.Tests
{
    [TestClass()]
    public class MappingTests
    {
        [TestMethod()]
        public void SpdA()
        {
            List<string> bases = new List<string>();
            List<string> targets = new List<string>();
            for (int i = 0; i < 10000; i++)
            {
                bases.Add(i.ToString());
            }
            targets.AddRange(bases.OrderByDescending(c => c).ToArray());

            List<string> results = new List<string>();
            foreach(string s in bases)
            {
                foreach(string t in targets)
                {
                    if (s.Equals(t))
                    {
                        results.Add(t);
                        break;
                    }
                }
            }
        }

        [TestMethod()]
        public void SpdB()
        {
            List<string> bases = new List<string>();
            List<string> targets = new List<string>();
            for (int i = 0; i < 10000; i++)
            {
                bases.Add(i.ToString());
            }
            targets.AddRange(bases.OrderByDescending(c => c).ToArray());

            List<string> results = new List<string>();
            Mapping.MappingArray(bases.ToArray(), targets.ToArray(),
                (baseI, targetI) => {
                    results.Add(targets[targetI]);
                }
                );
        }
    }
}