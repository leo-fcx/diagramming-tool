using System;
using Xunit;
using DiagramService;

namespace DiagramService.Tests
{
    public class ClassifierTest
    {
        private Classifier classifier;

        public ClassifierTest()
        {
            classifier = new Classifier(ClassifierType.INTERFACE);
        }

        [Fact]
        public void ShouldCreateinstanceOfClassType()
        {
            classifier = new Classifier(ClassifierType.CLASS);
            Assert.NotNull(classifier);
        }

        [Fact]
        public void ShouldCreateinstanceOfInterfaceType()
        {
            classifier = new Classifier(ClassifierType.INTERFACE);
            Assert.NotNull(classifier);
        }

        [Fact]
        public void ShouldCreateinstanceOfObjectType()
        {
            classifier = new Classifier(ClassifierType.CLASS);
            Assert.NotNull(classifier);
        }
    }
}
