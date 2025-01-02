using System;

namespace WorkflowNet.Core.Interfaces
{
    public interface IVariable: IBaseEntity
    {
        T Get<T>();
        void Set<T>(T value);
        Type Type { get; }
    }
}
