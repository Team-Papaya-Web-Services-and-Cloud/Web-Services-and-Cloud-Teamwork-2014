namespace Votter.ConsoleClient
{
    using System;
    using System.Linq;

    using DropBoxTest.Data;

    using Spring.IO;

    public class VotterConsoleClient
    {
        private const string TestPicture = @"../../Resources/1.png";

        internal static void Main()
        {
            var dropbox = new DropBoxCloudConnector();
            var file = new FileResource(TestPicture);

            var a = dropbox.UploadPicturesToCloud(file);
        }
    }
}