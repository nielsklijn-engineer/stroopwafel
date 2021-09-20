namespace Ordering.Commands {
    public interface ICommandHandler<T> {

        public void Handle(T command);

    }
}