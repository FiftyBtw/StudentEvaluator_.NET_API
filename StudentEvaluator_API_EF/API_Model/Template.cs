using System.Collections.ObjectModel;


namespace Client_Model
{
    public class Template
    {
        private readonly long _id;

        public long Id
        {
            get => _id;
        }

        public string Name { get; set; }
        public IReadOnlyCollection<Criteria> Criterias
        {
            get;
        }

        private List<Criteria> _criterias = [];

        public Template(long id, string name, ICollection<Criteria> criterias) {
            _id = id;
            Name = name;
            Criterias = new ReadOnlyCollection<Criteria>(_criterias);
            _criterias.AddRange(criterias);
        }

        public override string ToString()
        {
            string template = "Template : " + Id + ", " + Name + "\n" + "\nCriterias :\n";
            foreach (var criteria in Criterias)
            {
                template += criteria.ToString();
            }
            return template;
        }

    }
}
