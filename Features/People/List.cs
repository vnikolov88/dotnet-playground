using Mediator;

namespace DotnetPlayground.Features.People.List
{
    public record ListRequest() : IRequest<ListResponse>;

    public class Handler : IRequestHandler<ListRequest, ListResponse>
    {
        public ValueTask<ListResponse> Handle(ListRequest request, CancellationToken cancellationToken)
        {
            return new ValueTask<ListResponse>(new ListResponse(new String[]{"teste", "teste"} ));
        }
    }

    public record ListResponse(String[] people);
}
