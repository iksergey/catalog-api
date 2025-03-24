
namespace Api.Books.UpdateBook;

public record UpdateBookCommand(
    Guid Id,
    string Title,
    string Name,
    string Description,
    string ImageUrl,
    decimal Price,
    List<string> Category
) : ICommand<UpdateBookResult>;

public record UpdateBookResult(bool IsSuccess);

// TODO UpdateBookCommandValidator

public class UpdateBookCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateBookCommand, UpdateBookResult>
{
    public async Task<UpdateBookResult> Handle(
        UpdateBookCommand command,
        CancellationToken cancellationToken)
    {
        var book = await session.LoadAsync<Book>(command.Id, cancellationToken);

        if (book is null)
        {
            throw new Exception();
        }

        command.Adapt(book);
        session.Update(book);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateBookResult(true);
    }
}
