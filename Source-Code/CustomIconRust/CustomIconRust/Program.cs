using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace CustomIconRust
{
    class Program
    {
        static List<string> default_file = new List<string>();
        static List<string> newfile = new List<string>();
        static void Main(string[] args)
        {
            default_file = getTempAllFiles();

            while(true)
            {
                Console.Clear();

                string sbbs = "tmp";
                bool wwh = true;
                bool wwhh = true;

                Console.Write("Fichié de la nouvelle image: ");
                string _file = Console.ReadLine();
                if (_file != "")
                {
                    if (!File.Exists(_file + "/icon.png"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Le fichié {0} n'existe pas !", _file + "/icon.png");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return;
                    }
                    if (!File.Exists(_file + "/icon_background.png"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Le fichié {0} n'existe pas !", _file + "/icon_background.png");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        return;
                    }

                    Console.WriteLine("En attente de publications !!");

                    while (wwh)
                    {
                        foreach (var item in getTempAllFiles())
                        {
                            if (!default_file.Contains(item) && !newfile.Contains(item))
                            {
                                newfile.Add(item);
                                string _str = item.Replace(Path.GetTempPath(), "");

                                if (_str.Substring(0, 3) == sbbs && _str.Split('.')[1] == "tmp")
                                {
                                    Console.WriteLine("Le fichié de item a ete généré et pret a etre modifié");
                                    while (wwhh)
                                    {
                                        if (Directory.GetFiles(item).Length == 3)
                                        {
                                            File.Copy(_file + "/icon.png", item + "/icon.png", true);
                                            File.Copy(_file + "/icon_background.png", item + "/icon_background.png", true);
                                            Console.WriteLine("Les fichiés des nouvelle image ont bien ete modifié !");
                                            wwhh = false;
                                            wwh = false;
                                            Console.ReadKey();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
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
