using MediatR;

namespace CodeSnippet.Application.Abstractions.MediatR;
public interface ICommand<TResult> : IRequest<TResult>
{
}
