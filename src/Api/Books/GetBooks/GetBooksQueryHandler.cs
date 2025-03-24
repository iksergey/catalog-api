namespace Catalog.API.Books.GetBooks;

public record GetBooksQuery(int? PageNumber = 1, int? PageSize = 5)
    : IQuery<GetBooksResult>;

public record GetBooksResult(IEnumerable<Book> Books);

internal class GetBooksQueryHandler(IDocumentSession session)
    : IQueryHandler<GetBooksQuery, GetBooksResult>
{
    public async Task<GetBooksResult> Handle(GetBooksQuery query,
    CancellationToken cancellationToken)
    {
        var books = await session.Query<Book>()
            // .ToListAsync(cancellationToken);
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 5, cancellationToken);

        return new GetBooksResult(books);
    }
}
