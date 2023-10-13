using System.Collections.Concurrent;
using System.Reflection;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;

namespace Application.Common.Extensions;

public static class RequestExtensions
{
    private static readonly ConcurrentDictionary<Type, HandlerCode> HandlerCodeCache = new();

    public static HandlerCode GetHandlerCode<TRequest>(this TRequest request)
        where TRequest : IBaseRequest
    {
        return GetHandlerCodeOfType(request.GetType());
    }

    public static HandlerCode GetHandlerCode<TRequest>()
        where TRequest : IBaseRequest
    {
        return GetHandlerCodeOfType(typeof(TRequest));
    }

    private static HandlerCode GetHandlerCodeOfType(Type type)
    {
        return HandlerCodeCache.GetOrAdd(type, t
            => t.GetCustomAttribute<HandlerCodeAttribute>()!.HandlerCode);
    }
}