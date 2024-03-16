namespace Client_Model
{
    public class SliderCriteria : Criteria
    {
        public long Value { get; set; }
        public SliderCriteria(){}
        public SliderCriteria(long id, string name, long valueEvaluation, long templateId,long value) : base(id, name, valueEvaluation, templateId)
        {
            Value = value;
        }

        public override string ToString()
        {
            return "SliderCriteria : " + Id + ",, Nom : " + Name + ", Value : " + ValueEvaluation +", SliderValue : " +Value+", (" + TemplateId + ")\n\n";
        }
    }
}
