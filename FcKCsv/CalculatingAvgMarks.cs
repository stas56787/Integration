using System.Collections.Generic;

namespace FcKCsv
{
    class CalculatingAvgMarks
    {
        public static void AvgStudentMarks(AverageStudentMarks[] avgStudentMarks, DataRecord[] dataRecords)
        {
            int k = 0;
            foreach (var record in dataRecords)
            {
                avgStudentMarks[k] = new AverageStudentMarks();
                avgStudentMarks[k].Surname = record.Surname;
                avgStudentMarks[k].Name = record.Name;
                avgStudentMarks[k].Patronymic = record.Patronymic;
                k++;
            }

            foreach (var student in avgStudentMarks)
            {
                double mark = 0;

                foreach (DataRecord record in dataRecords)
                {
                    if (record.Name == student.Name && record.Surname == student.Surname && record.Patronymic == student.Patronymic)
                    {
                        mark = (record.Python + record.Java + record.Javascript + record.Php);
                    }
                }

                student.AverageMark = mark / 4;
            }
        }

        public static void AvgSubjectMarks(AverageSubjectMarks[] avgSubjectMarks, DataRecord[] dataRecords)
        {
            avgSubjectMarks[0] = new AverageSubjectMarks();
            avgSubjectMarks[0].SubjectName = "Python";
            avgSubjectMarks[1] = new AverageSubjectMarks();
            avgSubjectMarks[1].SubjectName = "Java";
            avgSubjectMarks[2] = new AverageSubjectMarks();
            avgSubjectMarks[2].SubjectName = "Javascript";
            avgSubjectMarks[3] = new AverageSubjectMarks();
            avgSubjectMarks[3].SubjectName = "Php";


            int i = 0;

            foreach (var subject in avgSubjectMarks)
            {
                double mark = 0;
                int j = 0;

                foreach (DataRecord record in dataRecords)
                {
                    if (subject.SubjectName == "Python")
                    {
                        mark += record.Python;
                        j++;
                    }
                    if (subject.SubjectName == "Java")
                    {
                        mark += record.Java;
                        j++;
                    }
                    if (subject.SubjectName == "Javascript")
                    {
                        mark += record.Javascript;
                        j++;
                    }
                    if (subject.SubjectName == "Php")
                    {
                        mark += record.Php;
                        j++;
                    }
                }

                subject.AverageMark = mark / j;

                i++;
            }
        }

        public static double AvgGroupMark(DataRecord[] dataRecords)
        {
            double mark = 0;
            int i = 0;

            foreach (DataRecord record in dataRecords)
            {
                mark += (record.Python + record.Java + record.Javascript + record.Php) / 4;

                i++;
            }

            mark = mark / i;

            return mark;
        }
    }
}
