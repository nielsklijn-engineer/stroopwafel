namespace Ordering.Queries {
    public interface IQueryHandler<TQuery,TResult> {

        public TResult Handle(TQuery query);

    }
}