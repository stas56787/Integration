using System;
using System.IO;
using System.Runtime.Serialization.Json;
using NLog;

namespace FcKCsv
{
    class Json : ISave
    {
        public void Save(AverageStudentMarks[] avgStudentMarks, AverageSubjectMarks[] avgSubjectMarks, double averageGroupMark, string path)
        {
            Logger log = LogManager.GetCurrentClassLogger();

            try
            {
                DataContractJsonSerializer jsonFormatterSt =
                    new DataContractJsonSerializer(typeof(AverageStudentMarks[]));

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    jsonFormatterSt.WriteObject(fs, avgStudentMarks);
                }

                DataContractJsonSerializer jsonFormatterSu =
                    new DataContractJsonSerializer(typeof(AverageSubjectMarks[]));

                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    jsonFormatterSu.WriteObject(fs, avgSubjectMarks);
                }

                DataContractJsonSerializer jsonFormatterGr = new DataContractJsonSerializer(typeof(double));

                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    jsonFormatterGr.WriteObject(fs, averageGroupMark);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Успешно сохранено.");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                log.Error(e);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возикла ошибка, попробуйте еще раз.");
                Console.ResetColor();
            }
        }
    }
}
