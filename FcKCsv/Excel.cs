using System;
using GemBox.Spreadsheet;
using NLog;

namespace FcKCsv
{
    class Excel : ISave
    {
        public void Save(AverageStudentMarks[] avgStudentMarks, AverageSubjectMarks[] avgSubjectMarks, double averageGroupMark, string path)
        {
            Logger log = LogManager.GetCurrentClassLogger();

            try
            {
                SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

                var workbook = new ExcelFile();

                var studentWorksheet = workbook.Worksheets.Add("Average Student Marks");
                var subjectWorksheet = workbook.Worksheets.Add("Average Subject Marks");
                var groupWorksheet = workbook.Worksheets.Add("Average Group Marks");

                studentWorksheet.Cells["A1"].Value = "Name";
                studentWorksheet.Cells["B1"].Value = "Surname";
                studentWorksheet.Cells["C1"].Value = "Patronymic";
                studentWorksheet.Cells["D1"].Value = "Mark";

                subjectWorksheet.Cells["A1"].Value = "Subject Name";
                subjectWorksheet.Cells["B1"].Value = "Mark";

                groupWorksheet.Cells["A1"].Value = "Group";
                groupWorksheet.Cells["B1"].Value = "Mark";

                int row = 2;
                foreach (var studentMark in avgStudentMarks)
                {
                    studentWorksheet.Cells["A" + row].Value = studentMark.Surname;
                    studentWorksheet.Cells["B" + row].Value = studentMark.Name;
                    studentWorksheet.Cells["C" + row].Value = studentMark.Patronymic;
                    studentWorksheet.Cells["D" + row].Value = studentMark.AverageMark;

                    row++;
                }

                row = 2;
                foreach (var subjectMark in avgSubjectMarks)
                {
                    subjectWorksheet.Cells["A" + row].Value = subjectMark.SubjectName;
                    subjectWorksheet.Cells["B" + row].Value = subjectMark.AverageMark;

                    row++;
                }

                groupWorksheet.Cells["A2"].Value = "Group";
                groupWorksheet.Cells["B2"].Value = averageGroupMark;

                // Save to XLSX file.
                workbook.Save(path);

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
