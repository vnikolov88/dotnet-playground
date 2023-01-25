using DotnetPlayground.DbConfig;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace DotnetPlayground.Features.Projects.List
{
    public record ListRequest(int page, int totalItemsPerPage) : IRequest<ListResponse>;

    public class Handler : IRequestHandler<ListRequest, ListResponse>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ValueTask<ListResponse> Handle(ListRequest request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));
            
            int page = request.page - 1;

            _dataContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var projects = _dataContext.Projects
               .Skip(page * request.totalItemsPerPage)
                .Take(request.totalItemsPerPage)
                .ToList()
                .Select(r=> new ProjectResponse(r.Id, r.Title, r.Color)).ToList();

            var totalPages = (int)Math.Ceiling(_dataContext.Projects.Count() / (double)request.totalItemsPerPage);

            return new ValueTask<ListResponse>(new ListResponse(projects, totalPages));
        }
    }

    public record ListResponse(List<ProjectResponse> projects, int totalPages);

    public record ProjectResponse(Guid id, string title, string color );
}

