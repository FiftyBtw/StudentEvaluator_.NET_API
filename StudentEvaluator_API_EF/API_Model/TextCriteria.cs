using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Model
{
    public class TextCriteria : Criteria
    {
        public string Text { get; set; }
        public TextCriteria() { }
        public TextCriteria(long id, string name, long valueEvaluation, long templateId,string text) : base(id, name, valueEvaluation, templateId)
        {
            Text = text;
        }
    }

}
