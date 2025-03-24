namespace Catalog.API.Books.GetBooks;

public record GetBooksResponse(IEnumerable<Book> Books);

public class GetBooksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // http://localhost:15000/books
        app.MapGet("/books", async (ISender sender) =>
        {
            GetBooksResult result = await sender.Send(new GetBooksQuery());
            GetBooksResponse response = result.Adapt<GetBooksResponse>();
            return Results.Ok(response);
        });
    }
}