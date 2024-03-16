namespace Client_Model
{
    /// <summary>
    /// Represents a student.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class with the specified properties.
        /// </summary>
        /// <param name="id">The student's ID.</param>
        /// <param name="name">The student's name.</param>
        /// <param name="lastname">The student's last name.</param>
        /// <param name="urlPhoto">The URL of the student's photo.</param>
        /// <param name="groupYear">The student's group year.</param>
        /// <param name="groupNumber">The student's group number.</param>
        public Student(long id, string name, string lastname, string urlPhoto, int groupYear, int groupNumber)
        {
            _id = id;
            Name = name;
            Lastname = lastname;
            UrlPhoto = urlPhoto;
            GroupYear = groupYear;
            GroupNumber = groupNumber;
        }

        /// <summary>
        /// Returns a string that represents the current student.
        /// </summary>
        /// <returns>A string that represents the current student.</returns>
        public override string ToString()
        {
            return "Student : " + Id + ", " + Name + " " + Lastname + ", " + GroupYear + "A G" + GroupNumber + ".";
        }
    }
}
