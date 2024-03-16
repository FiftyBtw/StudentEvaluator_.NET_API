namespace Client_Model
{
    public class Teacher : User
    {
        public List<Template> Templates { get; set; } = [];
        public Teacher() { }

        public Teacher(long id, string username, string password, string[] roles):base(id, username, password, roles)
        {

        }
        public Teacher(long id, string username, string password, string[] roles,List<Template> templates) : base(id, username, password, roles)
        {
            Templates = templates;  
        }
    }
}
