using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Rappen.XTB.AutoNumManager
{
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Auto Number Manager"),
        ExportMetadata("Description", "This is a description for my first plugin"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", null),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", null),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class PluginInfo : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new AutoNumMgr();
        }
    }
}
