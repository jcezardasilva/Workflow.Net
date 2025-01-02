namespace WorkflowNet.Core.Interfaces
{
    public interface IPage: IVariables, IActions, IBaseEntity
    {
        IAction StartAction { get; set; }
    }
}
