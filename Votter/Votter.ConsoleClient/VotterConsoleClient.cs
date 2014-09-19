namespace Votter.ConsoleClient
{
    using System;
    using Spring.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Votter.Data;
    using Votter.DropboxApi;
    using Votter.PubnubApi;

    public class VotterConsoleClient
    {
        private static readonly VotterData votterData = new VotterData();

        internal static void Main()
        {
            //Console.WriteLine(votterData.Pictures.All().Count());

            // Testing Dropbox
            var dropbox = new DropBoxCloudConnector();

            string testPicture = @"../../Resources/1.png";
            var file = new FileResource(testPicture);
            var uploadedfFile = dropbox.UploadPicturesToCloud(file);
            Console.WriteLine(uploadedfFile.Path);

            var allPictures = dropbox.GetAllPictures();
            var picOne = allPictures.Contents[0];
            var picTwo = allPictures.Contents[allPictures.Contents.Count / 2];

            string picOneFilePath = picOne.Path;
            var picOneLink = dropbox.GetPictureLink(picOneFilePath);
            string picTwoFilePath = picTwo.Path;
            var picTwoLink = dropbox.GetPictureLink(picTwoFilePath);
            Console.WriteLine(picOneLink.Url);
            Console.WriteLine(picTwoLink.Url);

            // Testing Pubnub
            var pubnub = new PubnubAlert();
            pubnub.Start();
            while (true)
            {
                string msg = Console.ReadLine();

                pubnub.PublishMessage(msg);
            }
        }
    }
}