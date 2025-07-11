using BuyZone.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.WAF.Application.BlackIP.Queries.GetAll;

public class GetAllIpsHandler:IRequestHandler<GetAllIpsQuery.Request,GetAllIpsQuery.Response>
{
    private readonly IRepository _repository;

    public GetAllIpsHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllIpsQuery.Response> Handle(GetAllIpsQuery.Request request, CancellationToken cancellationToken)
    {
        var ips = await _repository.Query<Domain.Entities.BlockIP>()
            .Where(b => b.Status == request.Status)
            .Select(GetAllIpsQuery.Response.IpDto.Selector())
            .ToListAsync(cancellationToken);
        return new GetAllIpsQuery.Response()
        {
            Ips =ips
        };

    }
}