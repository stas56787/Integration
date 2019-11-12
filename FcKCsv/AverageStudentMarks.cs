using System.Runtime.Serialization;

namespace FcKCsv
{
    [DataContract]
    class AverageStudentMarks
    {
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Patronymic { get; set; }
        [DataMember]
        public double AverageMark { get; set; }
    }
}
