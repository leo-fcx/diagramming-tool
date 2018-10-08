using System;

namespace DiagramService.Behaviors
{
    public interface CRUDBehavior
    {
        void Create();
        void Read();
        void Update();
        void Delete();
    }
}
