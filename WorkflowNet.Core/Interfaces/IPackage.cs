namespace WorkflowNet.Core.Interfaces
{
    public interface IPackage : IPages, IBaseEntity, IVariables
    {
        IPage MainPage { get; set; }
    }
}
