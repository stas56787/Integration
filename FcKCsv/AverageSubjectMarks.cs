using System.Runtime.Serialization;

namespace FcKCsv
{
    [DataContract]
    class AverageSubjectMarks
    {
        [DataMember]
        public string SubjectName { get; set; }
        [DataMember]
        public double AverageMark { get; set; }
    }
}
