using Microsoft.Xrm.Sdk;

namespace Rappen.XTB.AutoNumManager
{
    internal class SolutionProxy
    {
        #region Public Fields

        public Entity Solution;

        #endregion Public Fields

        #region Public Constructors

        public SolutionProxy(Entity solutionentity)
        {
            Solution = solutionentity;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Prefix { get { return ((AliasedValue)Solution["P.customizationprefix"]).Value.ToString() + "_"; } }

        public string UniqueName { get { return Solution["uniquename"].ToString(); } }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return $"{Solution["friendlyname"]} ({Solution["uniquename"]})";
        }

        #endregion Public Methods
    }
}