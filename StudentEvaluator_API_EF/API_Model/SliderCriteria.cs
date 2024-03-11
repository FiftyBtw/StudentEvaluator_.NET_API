using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Model
{
    public class SliderCriteria : Criteria
    {
        public long Value { get; set; }
        public SliderCriteria(long id, string name, long valueEvaluation, long templateId,long value) : base(id, name, valueEvaluation, templateId)
        {
            Value = value;
        }
    }
}
