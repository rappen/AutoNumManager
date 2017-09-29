using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rappen.XTB.AutoNumManager
{
    class SolutionProxy
    {
        public Entity Solution;

        public SolutionProxy(Entity solutionentity)
        {
            Solution = solutionentity;
        }

        public string Prefix
        {
            get
            {
                return ((AliasedValue)Solution["P.customizationprefix"]).Value.ToString() + "_";
            }
        }

        public override string ToString()
        {
            return $"{Solution["friendlyname"]} ({Solution["uniquename"]}";
        }
    }
}
