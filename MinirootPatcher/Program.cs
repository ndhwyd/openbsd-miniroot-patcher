using System;
using System.IO;

namespace MinirootPatcher
{
    class Program
    {
        private static string _minirootPath;
        private static string _idbloaderPath;
        private static string _ubootPath;

        private static void ParseUserArguments(string[] args)
        {
            if (args.Length == 0)
            {
                var myName = AppDomain.CurrentDomain.FriendlyName;
                Console.WriteLine($"Usage: \n" +
                                  $"{myName} -m X:\\minirootXX.img \n" +
                                  $"{myName} -i X:\\idbloader.img \n" +
                                  $"{myName} -u X:\\u-boot.itb");
                Environment.Exit(0);
            }

            for (var i = 0; i < args.Length; ++i)
            {
                switch (args[i])
                {
                    case "-m":
                        _minirootPath = args[i + 1];
                        if (!File.Exists(_minirootPath))
                            ErrorExit(_minirootPath);
                        break;
                    case "-i":
                        _idbloaderPath = args[i + 1];
                        if (!File.Exists(_idbloaderPath))
                            ErrorExit(_idbloaderPath);
                        break;
                    case "-u":
                        _ubootPath = args[i + 1];
                        if (!File.Exists(_ubootPath))
                            ErrorExit(_ubootPath);
                        break;
                    default:
                        break;
                }
            }
        }

        static void ErrorExit(string file)
        {
            Console.WriteLine($"ERROR: Can't find the file: {file}");
            Environment.Exit(0);
        }
        static void Main(string[] args)
        {
            ParseUserArguments(args);

            using (var miniroot = new MemoryStream(File.ReadAllBytes(_minirootPath)))
            {
                var idbloader = File.ReadAllBytes(_idbloaderPath);
                miniroot.Seek(0x00008000, SeekOrigin.Begin);
                miniroot.Write(idbloader, 0, idbloader.Length);
                
                var uboot = File.ReadAllBytes(_ubootPath);
                miniroot.Seek(0x00800000, SeekOrigin.Begin);
                miniroot.Write(uboot, 0, uboot.Length);

                var minirootFile = miniroot.ToArray();
                File.WriteAllBytes("miniroot.img", minirootFile);
            }
        }
    }
}
