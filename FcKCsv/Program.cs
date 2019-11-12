using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FcKCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = FindFile.Find(args[0]);
            if (file == "")
            {
                return;
            }

            using (var sr = new StreamReader(file))
            {
                var reader = new CsvReader(sr);
                reader.Configuration.Delimiter = ",";
                //CSVReader will now read the whole file into an enumerable
                IEnumerable<DataRecord> records = reader.GetRecords<DataRecord>();
                var dataRecords = records as DataRecord[] ?? records.ToArray();

                var avgStudentMarks = new AverageStudentMarks[dataRecords.Length];

                CalculatingAvgMarks.AvgStudentMarks(avgStudentMarks, dataRecords);

                var avgSubjectMarks = new AverageSubjectMarks[4];

                CalculatingAvgMarks.AvgSubjectMarks(avgSubjectMarks, dataRecords);

                double averageGroupMark = CalculatingAvgMarks.AvgGroupMark(dataRecords);

                switch (args[1])
                {
                    case "saveJson":
                        Json json = new Json();
                        json.Save(avgStudentMarks, avgSubjectMarks, averageGroupMark, args[2]);
                        break;
                    case "saveXlsx":
                        Excel excel = new Excel();
                        excel.Save(avgStudentMarks, avgSubjectMarks, averageGroupMark, args[2]);
                        break;
                    case "outInputFile":
                        foreach (DataRecord record in dataRecords)
                        {
                            Console.WriteLine("{0} {1} {2}, {3}, {4}, {5}, {6}", record.Surname, record.Name, record.Patronymic, record.Python, record.Java, record.Javascript, record.Php);
                        }
                        break;
                    case "avgStudent":
                        Console.WriteLine("\nСтудент|Средний балл");
                        foreach (var studentMark in avgStudentMarks)
                        {
                            Console.WriteLine(studentMark.Surname + " " + studentMark.Name + " " + studentMark.Patronymic + "|" + studentMark.AverageMark);
                        }
                        break;
                    case "avgSubject":
                        Console.WriteLine("\nПредмет|Средний балл");
                        foreach (var subjectMark in avgSubjectMarks)
                        {
                            Console.WriteLine(subjectMark.SubjectName + "|" + subjectMark.AverageMark);
                        }
                        break;
                    case "avgGroup":
                        Console.WriteLine("\nСредний балл по группе: " + averageGroupMark);
                        break;
                    default:
                        Console.WriteLine("Нет такой команды.");
                        break;
                    }
            }
        }
    }
}