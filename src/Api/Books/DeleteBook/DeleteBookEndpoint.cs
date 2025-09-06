namespace Catalog.API.Books.DeleteBook;

public record DeleteBookResponse(bool IsSuccess);

public class DeleteBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // http://localhost:15000/books/10000001-0000-0000-0000-000000000001
        app.MapDelete("/books/{id}", async (
            string id,
            ISender sender) =>
        {
            DeleteBookCommand command = new DeleteBookCommand(id);
            DeleteBookResult result = await sender.Send(command);
            DeleteBookResponse response = result.Adapt<DeleteBookResponse>();
            return Results.Ok(response);
        });
    }
}
