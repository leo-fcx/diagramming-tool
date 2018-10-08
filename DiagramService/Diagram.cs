using System;
using System.Collections.Generic;

namespace DiagramService
{
    public class Diagram
    {
        public string name;
        public List<Classifier> classifiers = new List<Classifier>();
        public Diagram(string name, List<Classifier> classifiers)
        {
            this.name = name;
            this.classifiers = classifiers;
        }

        public void AddClassifier(Classifier classifier)
        {
            classifiers.Add(classifier);
        }
    }
}
