using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Common;
using Microsoft.Practices.Prism.Regions;

namespace MakingRegions
{
    [Export(typeof(MefAutoDiscoveryRegionBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MefAutoDiscoveryRegionBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {
        public const string BehaviorKey = "MefAutoDiscoveryRegionBehavior";

        protected override void OnAttach()
        {
            AddRegisteredViews();
        }

        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<object, IRegionViewMetadata>> Views { get; set; }

        public void OnImportsSatisfied()
        {
            if (this.Region != null)
            {
                AddRegisteredViews();
            }
        }

        private void AddRegisteredViews()
        {
            // todo: this would need to be hardened to check that the view
            //          isn't already in the region.
            foreach (Lazy<object, IRegionViewMetadata> view in Views)
            {
                if (view.Metadata.RegionName == this.Region.Name)
                {
                    this.Region.Add(view.Value);
                }
            }
        }
    }
}
