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
            string collection = "/" + PictureCollection + "/" + resource.File.Name;
            var entry = this.dropBoxCloud.UploadToCloud(resource, collection);
            return entry;
        }

        public Entry GetAllPictures()
        {
            var pictures = this.dropBoxCloud.GetAllMediaFiles(PictureCollection);

            return pictures;
        }

        public DropboxLink GetPictureLink(string path)
        {
            var pictureLink = this.dropBoxCloud.GetMediaLink(path);

            return pictureLink;
        }
    }
}