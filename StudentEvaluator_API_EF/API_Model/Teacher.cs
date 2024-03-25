namespace Client_Model
{
    public class Teacher : User
    {
        public List<Template> Templates { get; set; } = [];
        public Teacher() { }

        public Teacher(string id, string username, string password):base(id, username, password)
        {

        }
        public Teacher(string id, string username, string password,List<Template> templates) : base(id, username, password)
        {
            Templates = templates;  
        }
    }
}
