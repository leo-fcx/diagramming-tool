using System;
using System.Collections.Generic;

namespace DiagramService
{
    public sealed class Service
    {
        public List<Diagram> diagrams = new List<Diagram>();

        private Service()
        {
        }

        public void SetDiagrams(List<Diagram> diagrams)
        {
            this.diagrams = diagrams;
        }

        public void AddDiagram(Diagram diagram)
        {
            this.diagrams.Add(diagram);
        }

        public static Service Instance { get { return Nested.instance; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly Service instance = new Service();
        }
    }
}
