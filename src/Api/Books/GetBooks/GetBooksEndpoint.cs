namespace Catalog.API.Books.GetBooks;

public record GetBooksRequest(int? PageNumber = 1, int? PageSize = 5);

public record GetBooksResponse(IEnumerable<Book> Books);

public class GetBooksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // http://localhost:15000/books?PageNumber=1&PageSize=5
        app.MapGet("/books", async (
            [AsParameters] GetBooksRequest request,
            ISender sender) =>
        {
            GetBooksQuery query = request.Adapt<GetBooksQuery>();
            GetBooksResult result = await sender.Send(query);
            GetBooksResponse response = result.Adapt<GetBooksResponse>();
            return Results.Ok(response);
        });
    }
}