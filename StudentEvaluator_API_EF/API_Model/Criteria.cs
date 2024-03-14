using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client_Model
{
    public abstract class Criteria
    {
        private readonly long _id;
        public long Id { get { return _id; } }
        public string Name { get; set; }
        public long ValueEvaluation { get; set; }
        public long TemplateId { get; set; }

        public Criteria() { }
        [JsonConstructor]
        public Criteria(long id,string name,long valueEvaluation,long templateId) { 
            _id = id;
            Name = name;
            ValueEvaluation = valueEvaluation;
            TemplateId = templateId;
        }

        public override string ToString()
        {
            return "Criteria : "+Id+", "+Name+", "+ValueEvaluation+", ("+TemplateId+")\n";
        }
    }
}
