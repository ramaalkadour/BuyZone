using BuyZone.Domain;
using BuyZone.WAF.Domain.Entities;
using BuyZone.WAF.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BuyZone.WAF.Application.Repot;

public class GetStaticByTypeOfAttackHandler : IRequestHandler<GetStaticByTypeOfAttackQuery.Request, GetStaticByTypeOfAttackQuery.Response>
{
    private readonly IRepository _repository;

    public GetStaticByTypeOfAttackHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetStaticByTypeOfAttackQuery.Response> Handle(GetStaticByTypeOfAttackQuery.Request request, CancellationToken cancellationToken)
    {
        var query =  _repository.Query<Domain.Entities.Logs>();
       
        if (request.TypeOfAttack.HasValue)
        {
            query = query.Where(l => l.TypeOfAttack == request.TypeOfAttack.Value);
        }
        var groupedByMonth = query
            .GroupBy(l => l.DateCreated.Month)
            .Select(g => new GetStaticByTypeOfAttackQuery.Response.Month
            {
                MonthNumber = g.Key,
                NumberOfAttempts = g.Count()
            })
            .OrderBy(m => m.MonthNumber)
            .ToList();

        // إنشاء الاستجابة
        var response = new GetStaticByTypeOfAttackQuery.Response
        {
            Months = groupedByMonth
        };

        return response;
    }
}