using System;
using DlibDotNet;
using DlibDotNet.Extensions;
using Dlib = DlibDotNet.Dlib;
/// https://github.com/takuya-takeuchi/FaceRecognitionDotNet/wiki/Quickstart
using FaceRecognitionDotNet;
using System.Linq;
using System.IO;
using System.Net.Http;

namespace CompareFacesFramework
{
    public static class FaceHelper
    {
        // Default file paths
        private const string ImageDirectory = "c:\\Models\\Images";

        private const string ImageBaseDirectory = "c:\\Models\\Images";

        private const string directory = "C:\\Models";

        private const string ModelTempDirectory = "TempModels";

        public static string CompareFaceWithBase(string imageToCompareName)
        {
            #region Initializing and defaults
			//TODO: These values should be moved into configuraton file
            string returnMessage = "The base file doesn't exist";

            FaceRecognition.InternalEncoding = System.Text.Encoding.GetEncoding("shift_jis");
            FaceRecognition _FaceRecognition;
            
            _FaceRecognition = FaceRecognition.Create(directory);
            var imageUrl1 = "";// Example URL: "https://upload.wikimedia.org/wikipedia/commons/3/3f";
            var imageFile1 = imageToCompareName;
            var imageUrl2 = "";// Example URL: "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/President_Barack_Obama.jpg";
            var imageFile2 = "base.jpg";
            bool atLeast1Time = false;
            double tollerance = 0.61;
            #endregion

            #region Construct first image
            var path1 = Path.Combine(ImageDirectory, imageFile1);
            if (!File.Exists(path1))
            {
                var url = $"{imageUrl1}/{imageFile1}";
                var binary = new HttpClient().GetByteArrayAsync(url).Result;

                Directory.CreateDirectory(ImageDirectory);
                File.WriteAllBytes(path1, binary);
            }
            else
            {
                returnMessage = string.Format("File {0} doesn't exist!", path1);
            }
            #endregion

            #region Construct second image
            var path2 = Path.Combine(ImageBaseDirectory, imageFile2);
            if (!File.Exists(path2))
            {
                var url = $"{imageUrl2}/{imageFile2}";
                var binary = new HttpClient().GetByteArrayAsync(url).Result;

                Directory.CreateDirectory(ImageDirectory);
                File.WriteAllBytes(path2, binary);
            }
            else
            {
                returnMessage = string.Format("File {0} doesn't exist!", path2);
            }
            #endregion

            using (var image1 = FaceRecognition.LoadImageFile(path1))
            using (var image2 = FaceRecognition.LoadImageFile(path2))
            {
                foreach (var numJitters in new[] { 1, 2 })
                    foreach (var model in Enum.GetValues(typeof(PredictorModel)).Cast<PredictorModel>())
                    {
                        if (model == PredictorModel.Custom)
                            continue;

                        var endodings1 = _FaceRecognition.FaceEncodings(image1, null, numJitters, model).ToArray();
                        var endodings2 = _FaceRecognition.FaceEncodings(image2, null, numJitters, model).ToArray();

                        foreach (var encoding in endodings1)
                            foreach (var compareFace in FaceRecognition.CompareFaces(endodings2, encoding, tollerance))
                            {
                                //Face compare: TRUE
                                                             
                                //Log file: Console.WriteLine("{0}", compareFace + "   " + $"{nameof(numJitters)}: {numJitters}");
                                if (compareFace)
                                {
                                    atLeast1Time = true;   
                                    //LogFile:  Console.WriteLine("{0}", model + "   " + $"{nameof(numJitters)}: {numJitters}");
                                    returnMessage = string.Format("Sistemul gaseste multe similitudini identificand fata ca apartinand aceleiasi persoane: " + "{0}", model + "   " + $"{nameof(numJitters)}: {numJitters}");
                                }
                            }

                        foreach (var encoding in endodings1)
                            encoding.Dispose();
                        foreach (var encoding in endodings2)
                            encoding.Dispose();
                    }
            }

            //Face compare: FALSE
            if (!atLeast1Time)
                returnMessage = string.Format("Sistemul gaseste prea putine similitudini identificand fata ca apartinand altei persoane: ");

            return returnMessage;
            //If return type is bool, then:
            //return atLeast1Time;

        }

    }
}
