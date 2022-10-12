using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace CustomIconRust
{
    class Program
    {
        static List<string> default_file = new List<string>();
        static List<string> newfile = new List<string>();
        static string path = "./upload file here";
        static int attempt;

        static void Main(string[] args)
        {
            Console.Clear();
            Headers();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            SetupUploadingImage();
        }

        static void SetupUploadingImage(bool starting = false)
        {
            if (starting)
            {
                Message("Want to retry yourself ? If (yes) press key");
                Console.ReadKey();
            }

            string[] files = Directory.GetFiles(path, "*.png");
            if (files.Length >= 1)
            {
                if(files.Length == 1)
                {
                    SetupWaitingUpload(files[0]);
                } else
                {
                    SetupAnyFiles(files);
                }
            } else
            {
                Message("No file was found!", ConsoleColor.Red);
                SetupUploadingImage(true);
            }
        }

        static void SetupAnyFiles(string[] files)
        {
            Message($"{files.Length} file found in the directory, choose the file to upload according to the number", ConsoleColor.Green);
            Console.WriteLine("");
            int i = 1;

            foreach (var item in files)
            {
                string[] split = item.Split('\\');
                Message($"{i} {split[split.Length - 1]}");
                i++;
            }

            string _string = Console.ReadLine();
            int fileNumber;
            if (_string.Length != 0)
            {
                if (!int.TryParse(_string, out fileNumber))
                {
                    SetupAnyFiles(files);
                    return;
                }

                if((fileNumber - 1) < 0)
                {
                    Message("An error occurred during lindex of the table!");
                    Console.Write("Press key to retry..");
                    Console.ReadKey();

                    Main(null);
                    return;
                }

                SetupWaitingUpload(files[fileNumber - 1]);
            }
        }

        static void SetupWaitingUpload(string path)
        {
            Message("");

            Message("Recovery of time files...");
            Thread.Sleep(1000);
            default_file = getTempAllFiles();
            Message($"{default_file.Count} files to recover...", ConsoleColor.Green);

            Thread.Sleep(5000);
            Console.Clear();
            Headers();
            Message("Waiting for upload !");
            Message("");

            attempt = 1;
            CheckTheNewFileTemp(path);
        }

        static void CheckTheNewFileTemp(string _file)
        {
            bool boucle_search = true;
            bool boucle_copy = true;
            while (boucle_search)
            {
                AttemptChecking(attempt);
                foreach (var item in getTempAllFiles())
                {
                    if (!default_file.Contains(item) && !newfile.Contains(item))
                    {
                        newfile.Add(item);
                        string _str = item.Replace(Path.GetTempPath(), "");

                        if (_str.Substring(0, 3) == "tmp" && _str.Split('.')[1] == "tmp")
                        {
                            Console.Clear();
                            Headers();
                            Message("The file was found, it is ready to be modified", ConsoleColor.Green);

                            while(boucle_copy)
                            {
                                if (Directory.GetFiles(item).Length == 3)
                                {
                                    File.Copy(_file, item + "/icon.png", true);
                                    File.Copy(_file, item + "/icon_background.png", true);

                                    Console.Clear();
                                    Headers();
                                    Message("The new image file has been published", ConsoleColor.Green);
                                    boucle_copy = false;
                                    boucle_search = false;
                                }
                            }
                        }
                    }
                }
                attempt++;
            }
        }

        static void AttemptChecking(int attemp)
        {
            Message($"- {attemp} attempt to search for file...");
        }

        static void Message(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void GenerateBar(int length = 5)
        {
            string str = "";
            for (int i = 0; i < length; i++)
            {
                str += "-";
            }
            Console.WriteLine(str);
        }

        static string DisplayMessage(string message)
        {
            Console.Write($"$ {message}");
            return Console.ReadLine();
        }

        static void Headers()
        {
            Console.WriteLine("Powered by Pwenill, https://github.com/pwenill/custom-icon-rust");
            Console.WriteLine("Copyright 2022 All right reserved");
            Console.WriteLine("");
            Console.WriteLine("1 - Insert your image in the file 'upload file here'");
            Console.WriteLine("2 - Start 'Custom Icon Rust.exe'");
            Console.WriteLine("3 - Wait until he gets the temporary file");
            Console.WriteLine("4 - When you see the attempt message published your item from rust via the workshop");
            Console.WriteLine("5 - Your personal image has been published in the workshop steam");
            Console.WriteLine("");
            GenerateBar(60);
            Console.WriteLine("");
        }

        static List<string> getTempAllFiles()
        {
            List<string> list = new List<string>();

            foreach (var item in Directory.GetFiles(Path.GetTempPath()))
                list.Add(item);

            foreach (var item in Directory.GetDirectories(Path.GetTempPath()))
                list.Add(item);

            return list;
        }

        static List<string> getFiles(string path)
        {
            List<string> list = new List<string>();

            foreach (var item in Directory.GetFiles(path))
                list.Add(item);

            return list;
        }
    }
}
