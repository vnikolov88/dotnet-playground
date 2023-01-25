using DotnetPlayground.DataEntities;
using DotnetPlayground.DbConfig;
using FluentValidation;
using Mediator;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DotnetPlayground.Features.Projects.CreateOrEdit
{
    public record CreateOrEditRequest(Guid? id, string title, string color) : IRequest<CreateOrEditResponse>;
    public class Handler : IRequestHandler<CreateOrEditRequest, CreateOrEditResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<CreateOrEditRequest> _validator;
        
        public Handler(DataContext context, IValidator<CreateOrEditRequest> validator)
        {
            _validator = validator;
            _dataContext = context;

        }

        public async ValueTask<CreateOrEditResponse> Handle(CreateOrEditRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrEditValidator();
            var result = _validator.Validate(request);

            if (!result.IsValid)
                throw new InvalidOperationException();

            if (request?.id == null)
            {
                #region create
                var newProject = new Project
                {
                    Title = request.title,
                    Color = request.color,
                };
                
                _dataContext.Add(newProject);

                await _dataContext.SaveChangesAsync();

                return await new ValueTask<CreateOrEditResponse>(new CreateOrEditResponse(newProject.Id, newProject.Title, newProject.Color));

                #endregion
            }
            else
            {
                #region update

                var projectToUpdate = await _dataContext.Projects.FindAsync(request.id);
                projectToUpdate. Title = request.title;
                projectToUpdate.Color = request.color;
                
                await _dataContext.SaveChangesAsync();

                return await new ValueTask<CreateOrEditResponse>(new CreateOrEditResponse(projectToUpdate.Id, projectToUpdate.Title, projectToUpdate.Color));

                #endregion
            }
        }
    }

    public record CreateOrEditResponse(Guid? id, string title, string color);


}
