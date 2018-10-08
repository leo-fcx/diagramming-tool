using System;
using DiagramService;

namespace DiagramAPI.Models
{
    public class Relationship
    {
        public long ID { get; set; }
        public RelationshipType Type { get; set; }
        public virtual Classifier Classifier { get; set; }
    }
}
