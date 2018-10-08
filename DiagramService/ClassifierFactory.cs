using System;

namespace DiagramService
{
    static class ClassifierFactory
    {
        public static Classifier Get(ClassifierType type)
        {
            switch (type)
            {
                case ClassifierType.CLASS:
                    return new Classifier(ClassifierType.CLASS);
                default:
                    return new Classifier(ClassifierType.INTERFACE);
            }
        }
    }
}
