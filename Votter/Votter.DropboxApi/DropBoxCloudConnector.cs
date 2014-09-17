namespace Votter.DropboxApi
{
    using System;
    using System.Linq;
    using Spring.IO;
    using Spring.Social.Dropbox.Api;

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