using MediatR;

namespace CodeSnippet.Application.Abstractions.MediatR;
public interface IQuery<TResult> : IRequest<TResult>
{
}
