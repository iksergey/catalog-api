namespace Catalog.API.Books.GetBookById;

public record GetBookByIdQuery(string Id)
    : IQuery<GetBookByIdResult>;

public record GetBookByIdResult(Book Item);

public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id не может быть пустым")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("Id должен быть валидным GUID");
    }
}


internal class GetBookByIdQueryHandler(IDocumentSession session)
    : IQueryHandler<GetBookByIdQuery, GetBookByIdResult>
{
    public async Task<GetBookByIdResult> Handle(GetBookByIdQuery query,
        CancellationToken cancellationToken)
    {
        var isSuccess = Guid.TryParse(query.Id, out var id);

        var book = await session.LoadAsync<Book>(id, cancellationToken);

        if (book is null)
        {
            throw new BookNotFoundException(id);
        }

        return new GetBookByIdResult(book);
    }
}
