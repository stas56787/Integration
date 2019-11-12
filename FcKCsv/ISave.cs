using System;
using System.Collections.Generic;
using System.Text;

namespace FcKCsv
{
    interface ISave
    {
        void Save(AverageStudentMarks[] avgStudentMarks, AverageSubjectMarks[] avgSubjectMarks, double averageGroupMark, string path);
    }
}
