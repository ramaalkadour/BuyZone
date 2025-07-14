using MediatR;

namespace BuyZone.Application.Order.Queries.GetReport;

public class GetReportQuery
{
    public class Request:IRequest<Response>
    {
        
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