namespace API_Model
{
    public class Student
    {
        private readonly long _id;
        public long Id { get { return _id; } }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string UrlPhoto { get; set; }

        private int _groupeyear;
        public int GroupYear { get { return _groupeyear; } set { _groupeyear = value; } }

        private int _groupnumber;
        public int GroupNumber { get { return _groupnumber; } set { _groupnumber = value; } }
            
        public Student(long id, string name, string lastname, string urlPhoto, int groupYear, int groupNumber)
        {
            _id = id;
            Name = name;
            Lastname = lastname;
            UrlPhoto = urlPhoto;
            GroupYear = groupYear;
            GroupNumber = groupNumber;
        }

        public override string ToString()
        {
            return "Student : " + Id + ", " + Name + " " + Lastname + ", " + GroupYear + "A G" + GroupNumber + ".";
        }
    }
}
