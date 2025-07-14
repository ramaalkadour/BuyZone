using MediatR;
using MediatR;
using System.Collections.Generic;
namespace BuyZone.Application.Report.Queries.GetReportQuery;


public class GetReportQuery 
{
    public class Request:IRequest<Response>
    {
        
    }

    public class Response
    {
        public int CustomerCount { get; set; }
        public int OrderCount { get; set; }
    }
}

