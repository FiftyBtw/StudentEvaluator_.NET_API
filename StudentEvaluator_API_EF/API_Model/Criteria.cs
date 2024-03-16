using System.Text.Json.Serialization;

namespace Client_Model
{
    public abstract class Criteria
    {
        private readonly long _id;
        public long Id { get { return _id; } }
        public string Name { get; set; } = "";
        public long ValueEvaluation { get; set; } = 0;
        public long TemplateId { get; set; }
        
        public Criteria(long id,string name,long valueEvaluation,long templateId) { 
            _id = id;
            Name = name;
            ValueEvaluation = valueEvaluation;
            TemplateId = templateId;
        }
        
        protected Criteria() { }
        
        [JsonConstructor]
        public Criteria(long id, string name, long valueEvaluation, long templateId,string criteriaType)
        {
            _id = id;
            Name = name;
            ValueEvaluation = valueEvaluation;
            TemplateId = templateId;
        }
    }
}
