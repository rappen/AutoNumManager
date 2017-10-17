using Microsoft.Xrm.Sdk.Metadata;

namespace Rappen.XTB.AutoNumManager
{
    public class AttributeProxy
    {
        #region Internal Fields

        internal StringAttributeMetadata attributeMetadata;

        #endregion Internal Fields

        #region Public Constructors

        public AttributeProxy(StringAttributeMetadata metadata)
        {
            attributeMetadata = metadata;
        }

        #endregion Public Constructors

        #region Public Properties

        public string DisplayName { get { return attributeMetadata.DisplayName?.UserLocalizedLabel?.Label; } }
        public string Format { get { return attributeMetadata.AutoNumberFormat ?? string.Empty; } }
        public string LogicalName { get { return attributeMetadata.LogicalName; } }

        #endregion Public Properties

        #region Public Methods

        public override string ToString()
        {
            return attributeMetadata.LogicalName;
        }

        #endregion Public Methods
    }
}