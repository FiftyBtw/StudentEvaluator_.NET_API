using API_Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Model
{
    public class Template
    {
        private readonly long _id;
        public long Id { get { return _id; } }
        public string Name { get; set; }
        public IReadOnlyCollection<Criteria> Criterias
        {
            get;
            private set;
        }
        List<Criteria> _criterias = new List<Criteria>();

        public Template(long id, string name, IEnumerable<Criteria> criterias) {
            _id = id;
            Name = name;
            Criterias = new ReadOnlyCollection<Criteria>(_criterias);
            _criterias.AddRange(criterias);
        }

        public override string ToString()
        {
            string template = "Template : " + Id + ", " + Name + "\n" + "\tCriterias :";
            foreach (var criteria in Criterias)
            {
                template += criteria.ToString();
            }
            return template;
        }

    }
}
