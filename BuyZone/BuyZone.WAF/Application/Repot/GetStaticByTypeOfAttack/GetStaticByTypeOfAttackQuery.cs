using BuyZone.WAF.Domain.Enums;
using MediatR;

namespace BuyZone.WAF.Application.Repot;

public class GetStaticByTypeOfAttackQuery
{
    public class Request:IRequest<Response>
    {
        public TypeOfAttack ? TypeOfAttack { get; set; }
    }
    public class Response
    {
        public List<Month> Months { get; set; }
        public class Month
        {
            public int MonthNumber { get; set; }
            public int NumberOfAttempts { get; set; }
        }
    }
}