using System;
using System.Collections.Generic;
using DiagramService;

namespace DiagramAPI.Models
{
    public class Classifier
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public ClassifierType Type { get; set; }
        public virtual Diagram Diagram { get; set; }
        public virtual ICollection<Relationship> Relationships { get; set; }
    }
}
