using System;
using System.Collections.Generic;

namespace DiagramAPI.Models
{
    public class Diagram
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Classifier> Classifiers { get; set; }
    }
}
