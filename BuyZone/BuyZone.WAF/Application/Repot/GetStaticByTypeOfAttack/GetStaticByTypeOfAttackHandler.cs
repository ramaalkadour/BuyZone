using BuyZone.Domain;
using BuyZone.WAF.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetStaticByTypeOfAttackHandler : IRequestHandler<GetStaticByTypeOfAttackQuery.Request, GetStaticByTypeOfAttackQuery.Response>
{
    private readonly IRepository _repository;

    public GetStaticByTypeOfAttackHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetStaticByTypeOfAttackQuery.Response> Handle(GetStaticByTypeOfAttackQuery.Request request, CancellationToken cancellationToken)
    {
        var tenDaysAgo = DateTime.UtcNow.Date.AddDays(-10);

        var query = _repository.Query<Logs>()
            .Where(l => l.DateCreated >= tenDaysAgo);

        if (request.TypeOfAttack.HasValue)
        {
            query = query.Where(l => l.TypeOfAttack == request.TypeOfAttack.Value);
        }

        var groupedByDay = await query
            .GroupBy(l => l.DateCreated.Date)
            .Select(g => new GetStaticByTypeOfAttackQuery.Response.DayStatistics
            {
                Date = g.Key,
                NumberOfAttempts = g.Count()
            })
            .OrderBy(d => d.Date)
            .ToListAsync(cancellationToken);

        var response = new GetStaticByTypeOfAttackQuery.Response
        {
            Days = groupedByDay
        };

        return response;
    }
}