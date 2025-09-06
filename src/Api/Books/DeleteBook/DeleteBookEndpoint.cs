namespace Catalog.API.Books.DeleteBook;

public record DeleteBookRequest(string Id);

public record DeleteBookResponse(bool IsSuccess);

public class DeleteBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // http://localhost:15000/books?id=10000001-0000-0000-0000-000000000001
        app.MapDelete("/books", async (
            [AsParameters] DeleteBookRequest request,
            ISender sender) =>
        {
            DeleteBookCommand command = request.Adapt<DeleteBookCommand>();
            DeleteBookResult result = await sender.Send(command);
            DeleteBookResponse response = result.Adapt<DeleteBookResponse>();
            return Results.Ok(response);
        });
    }
}
