using BuyZone.WAF.Domain.Enums;
using MediatR;

public class GetStaticByTypeOfAttackQuery
{
    public class Request : IRequest<Response>
    {
        public TypeOfAttack? TypeOfAttack { get; set; }
    }

    public class Response
    {
        public List<DayStatistics> Days { get; set; }

        public class DayStatistics
        {
            public DateTime Date { get; set; }
            public int NumberOfAttempts { get; set; }
        }
    }
}