using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;

using MEFedMVVM.ViewModelLocator;

namespace CinchV2DemoWPF
{

    /// <summary>
    /// Designtime implementation of the 
    /// Data service used by the <c>ImageLoaderViewModel</c> to obtain data
    /// </summary>
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportService(ServiceType.DesignTime, typeof(IImageProvider))]
    public class DesigntimeImageProvider : IImageProvider
    {
        #region Public Methods

        public void FetchImages(string imagePath, Action<List<ImageData>> callback)
        {
            List<ImageData> fakeImages = new List<ImageData>();
            for (int i = 0; i < 10; i++)
            {
                ImageData id = new ImageData();
                id.ImagePath = @"C:\Users\Public\Pictures\Sample Pictures\Desert.jpg";
                id.FileDate = DateTime.Now;
                id.FileExtension = "*.jpg";
                id.FileName = "Desert.jpg";
                id.FileSize = 223;
                fakeImages.Add(id);
            }
            callback(fakeImages);
        }


        #endregion


    }
}
