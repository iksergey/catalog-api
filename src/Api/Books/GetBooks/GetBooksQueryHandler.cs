namespace Catalog.API.Books.GetBooks;

public record GetBooksQuery()
    : IQuery<GetBooksResult>;

public record GetBooksResult(IEnumerable<Book> Books);

internal class GetBooksQueryHandler(IDocumentSession session)
    : IQueryHandler<GetBooksQuery, GetBooksResult>
{
    public async Task<GetBooksResult> Handle(GetBooksQuery query,
    CancellationToken cancellationToken)
    {
        var books = await session.Query<Book>().ToListAsync(cancellationToken);

        return new GetBooksResult(books);
    }
}
