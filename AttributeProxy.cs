using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rappen.XTB.AutoNumManager
{
    public class AttributeProxy
    {
        internal StringAttributeMetadata attributeMetadata;

        public AttributeProxy(StringAttributeMetadata metadata)
        {
            attributeMetadata = metadata;
        }

        public string LogicalName { get { return attributeMetadata.LogicalName; } }

        public string DisplayName { get { return attributeMetadata.DisplayName?.UserLocalizedLabel?.Label; } }

        public string Format { get { return attributeMetadata.AutoNumberFormat ?? string.Empty; } }

        public override string ToString()
        {
            return attributeMetadata.LogicalName;
        }
    }
}
