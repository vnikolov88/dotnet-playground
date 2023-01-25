using DotnetPlayground.DataEntities;
using DotnetPlayground.DbConfig;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace DotnetPlayground.Features.Projects.GetById
{
    public record GetByIdRequest(Guid id) : IRequest<GetByIdResponse>;

    public class GetByIdResponseHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly DataContext _dataContext;

        public GetByIdResponseHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async  ValueTask<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));

            var result = await _dataContext.Projects.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == request.id);

            return result != null ? new GetByIdResponse(result.Id, result.Title, result.Color) : null;
        }


    }

    public record GetByIdResponse(Guid id, String title, String color);
}

