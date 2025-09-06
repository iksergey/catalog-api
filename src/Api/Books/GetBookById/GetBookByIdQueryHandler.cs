namespace Catalog.API.Books.GetBookById;

public record GetBookByIdQuery(string Id)
    : IQuery<GetBookByIdResult>;

public record GetBookByIdResult(Book Item);

internal class GetBookByIdQueryHandler(IDocumentSession session)
    : IQueryHandler<GetBookByIdQuery, GetBookByIdResult>
{
    public async Task<GetBookByIdResult> Handle(GetBookByIdQuery query,
        CancellationToken cancellationToken)
    {
        var isSuccess = Guid.TryParse(query.Id, out var id);

        if (!isSuccess)
        {
            throw new ValidationException("Всё плохо");
        }

        var book = await session.LoadAsync<Book>(id, cancellationToken);

        if (book is null)
        {
            throw new BookNotFoundException(id);
        }

        return new GetBookByIdResult(book);
    }
}
