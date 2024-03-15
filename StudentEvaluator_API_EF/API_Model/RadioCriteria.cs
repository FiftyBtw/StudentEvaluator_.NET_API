using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Model
{
    public class RadioCriteria : Criteria
    {
        public string SelectedOption { get; set; }
        public string[] Options { get; set; }

        public RadioCriteria() { }
        public RadioCriteria(long id, string name, long valueEvaluation, long templateId,string selectedOptions, string[] options) : base(id, name, valueEvaluation, templateId)
        {
            SelectedOption = selectedOptions;
            Options = options;
        }

        public override string ToString()
        {
            string radioCriteria = "RadioCriteria : " + Id + ", Nom :" + Name + ", Value :" + ValueEvaluation + ", (" + TemplateId + ")\nOptions :";
            foreach(var option in Options) {
                radioCriteria+= option+", ";
            }
            radioCriteria += "Selected options : " + SelectedOption+"\n\n";

            return radioCriteria;
        }
    }
}
