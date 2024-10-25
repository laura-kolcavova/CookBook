using Microsoft.Extensions.Logging;

namespace CookBook.Extensions.CSharpExtended.MessageUtils;

public sealed class MessageBuilder
{
    private readonly string _message;

    private MessageBuilder(string message)
    {
        _message = message;
    }

    public static MessageBuilder FromMessage(string message)
    {
        return new MessageBuilder(message);
    }

    public MessageBuilder LogError(ILogger logger)
    {
        logger.LogError(_message);

        return this;
    }

    public MessageBuilder LogError(ILogger logger, Exception? exception)
    {
        logger.LogError(exception, _message);

        return this;
    }

    public TException CreateException<TException>()
        where TException : Exception
    {
        return (TException)Activator.CreateInstance(
            typeof(TException),
            _message)!;
    }

    public TException CreateException<TException>(Exception? innerException)
        where TException : Exception
    {
        return (TException)Activator.CreateInstance(
            typeof(TException),
            _message,
            innerException)!;
    }
}
