namespace Api.Behaviors;

public class TimeoutBehavior<TRequest, TResponse>
    (ILogger<TimeoutBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
         RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var timer = new Stopwatch();
        timer.Start();
        var response = await next();
        timer.Stop();

        string info = $" > Запрос: {typeof(TRequest).Name} " +
            $"выполняется {timer.Elapsed.TotalMilliseconds} ms";
        if (timer.Elapsed.Seconds > 2)
        {
            logger.LogWarning(info);
        }
        else
        {
            logger.LogInformation(info);
        }

        return response;
    }
}
