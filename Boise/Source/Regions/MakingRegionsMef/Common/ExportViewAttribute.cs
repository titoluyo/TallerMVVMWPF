using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Common
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportViewAttribute : ExportAttribute, IRegionViewMetadata
    {

        public ExportViewAttribute() : base(typeof(object))
        {
        }

        public string RegionName { get; set; }
    }

    public interface IRegionViewMetadata
    {
        string RegionName { get; }
    }
}
