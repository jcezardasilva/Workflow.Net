namespace WorkflowNet.Core.Interfaces
{
    public interface IPackage : IPages, IVariables
    {
        IPage MainPage { get; set; }
    }
}
