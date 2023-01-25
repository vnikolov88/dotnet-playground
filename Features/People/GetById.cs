using Mediator;

namespace DotnetPlayground.Features.People.GetById
{
    public record GetByIdRequest(Guid id) : IRequest<GetByIdResponse>;

    public class GetByIdResponseHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        public ValueTask<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            return new ValueTask<GetByIdResponse>(new GetByIdResponse(request.id, "Ricardo"));
        }
    }

    public record GetByIdResponse(Guid id, String name);
}
