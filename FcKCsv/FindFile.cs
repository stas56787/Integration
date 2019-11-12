using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FcKCsv
{
    class FindFile
    {
        public static string Find(string fileName)
        {
            List<string> files = new List<string>();

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                if (drive.ToString() == "C:\\")
                {
                    continue;
                }

                Console.WriteLine("Поиск на диске " + drive);

                string[] arr = SafeEnumerateFiles(drive.ToString(), fileName).ToArray();
                files.AddRange(arr);
            }

            string[] allFiles = files.ToArray();

            if (allFiles.Length == 0)
            {
                Console.WriteLine("Файл с таки именем не найден");
                return "";
            }

            if (allFiles.Length == 1)
            {
                return allFiles[0];
            }

            Console.WriteLine("Найдено более одного файла с таким именем, выберите нужный");
            for (int i = 0; i < allFiles.Length; i++)
            {
                Console.WriteLine(i + ") " + allFiles[i]);
            }

            Console.Write(">");
            int fileIndex = Convert.ToInt32(Console.ReadLine());

            return allFiles[fileIndex];
        }

        private static IEnumerable<string> SafeEnumerateFiles(string path, string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)
        {
            var dirs = new Stack<string>();
            dirs.Push(path);

            while (dirs.Count > 0)
            {
                string currentDirPath = dirs.Pop();
                if (searchOption == SearchOption.AllDirectories)
                {
                    try
                    {
                        string[] subDirs = Directory.GetDirectories(currentDirPath);
                        foreach (string subDirPath in subDirs)
                        {
                            dirs.Push(subDirPath);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        continue;
                    }
                }

                string[] files = null;
                try
                {
                    files = Directory.GetFiles(currentDirPath, searchPattern);
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (DirectoryNotFoundException)
                {
                    continue;
                }

                foreach (string filePath in files)
                {
                    yield return filePath;
                }
            }
        }
    }
}
