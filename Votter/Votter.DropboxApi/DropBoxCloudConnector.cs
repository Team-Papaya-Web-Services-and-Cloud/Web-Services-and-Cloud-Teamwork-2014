namespace DropBoxTest.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Spring.Social.OAuth1;
    using Spring.Social.Dropbox.Api;
    using Spring.Social.Dropbox.Connect;
    using Spring.IO;

    public class DropBoxCloudConnector
    {
        private const string PictureCollection = "Pictures";

        private readonly DropBoxCloud dropBoxCloud;

        public DropBoxCloudConnector()
            : this(new DropBoxCloud())
        {
        }

        public DropBoxCloudConnector(DropBoxCloud dropBoxCloud)
        {
            this.dropBoxCloud = dropBoxCloud;
        }


        public Entry UploadPicturesToCloud(FileResource resource)
        {
            string collection = "/" + PictureCollection + "/" + DateTime.Now + ".png";
            var entry = this.dropBoxCloud.UploadToCloud(resource, collection);

            return entry;
        }
    }
}
