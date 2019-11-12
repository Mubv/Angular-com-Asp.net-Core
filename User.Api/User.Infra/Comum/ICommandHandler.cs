namespace User.Infra.Comum
{
    public interface ICommandHandler<T> where T : CommandPadrao
    {
        ICommandResult Handle(T command);
    }
}