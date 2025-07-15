using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.WAF.Application.Logs.Queries.GetById;

public class GetLogByIdHandler:IRequestHandler<GetLogByIdQuery.Request,GetLogByIdQuery.Response>
{
    private readonly IRepository _repository;

    public GetLogByIdHandler(IRepository repository)
    {
        _repository = repository;
    }

    public Task<GetLogByIdQuery.Response> Handle(GetLogByIdQuery.Request request, CancellationToken cancellationToken)
    {
        return _repository.Query<Domain.Entities.Logs>()
            .Where(l => l.Id == request.Id)
            .Select(GetLogByIdQuery.Response.Selector())
            .FirstAsync(cancellationToken);
    }
}