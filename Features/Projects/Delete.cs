using DotnetPlayground.DbConfig;
using Mediator;

namespace DotnetPlayground.Features.Projects
{

    public record DeleteRequest(Guid id) : IRequest;

    public class Handler : IRequestHandler<DeleteRequest>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async ValueTask<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            if(request != null)
            {
                var entity = _dataContext.Projects.FirstOrDefault(x => x.Id == request.id);

                if (entity != null)
                {
                    _dataContext.Projects.Remove(entity);
                    await _dataContext.SaveChangesAsync();
                }
            }

            return await new ValueTask<Unit>();
        }
    }


}
