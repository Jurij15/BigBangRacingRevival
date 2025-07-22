using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BBRRevival.Server.Controllers
{
    [ApiController]
    [Route("/")]
    public class IndexController : ControllerBase
    {
        private readonly EndpointDataSource _endpointDataSource;

        public IndexController(EndpointDataSource endpointDataSource)
        {
            _endpointDataSource = endpointDataSource;
        }

        [HttpGet("endpoints/all")]
        public IActionResult GetAllEndpoints()
        {
            var endpoints = _endpointDataSource.Endpoints
                    .OfType<RouteEndpoint>()
                    .Select(e => new
                    {
                        RoutePattern = e.RoutePattern.RawText,
                        HttpMethods = e.Metadata
                            .OfType<HttpMethodMetadata>()
                            .FirstOrDefault()?.HttpMethods
                    });

            return Ok(endpoints);
        }

        [HttpGet("endpoints")]
        public ContentResult GetAllEndpointsHtml()
        {
            var endpoints = _endpointDataSource.Endpoints
                        .OfType<RouteEndpoint>()
                        .Select(e => new
                        {
                            Route = e.RoutePattern.RawText,
                            Methods = e.Metadata
                                .OfType<HttpMethodMetadata>()
                                .FirstOrDefault()?.HttpMethods
                        })
                        .Where(e => !string.IsNullOrWhiteSpace(e.Route)) // Avoid null/empty routes
                        .DistinctBy(e => e.Route); // Optional: avoid duplicates

            var sb = new StringBuilder();
            sb.AppendLine("<html><body>");
            sb.AppendLine("<h1>Registered Endpoints</h1>");
            sb.AppendLine("<ul>");

            foreach (var ep in endpoints)
            {
                // Replace route params like {id} with dummy values like 1
                string url = ep.Route.Replace("{", "").Replace("}", "").Replace("id", "1");
                string methods = string.Join(", ", ep.Methods ?? new[] { "ALL" });

                sb.AppendLine($"<li><strong>[{methods}]</strong> <a href='/{url}' target='_blank'>/{url}</a></li>");
            }

            sb.AppendLine("</ul>");
            sb.AppendLine("</body></html>");

            return new ContentResult
            {
                Content = sb.ToString(),
                ContentType = "text/html",
            };
        }
    }
}
