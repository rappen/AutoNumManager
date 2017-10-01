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

        public string Attribute { get { return attributeMetadata.LogicalName; } }

        public string Format { get { return attributeMetadata.AutoNumberFormat; } }

        public override string ToString()
        {
            return attributeMetadata.LogicalName;
        }
    }
}
