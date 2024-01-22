
namespace CodeSnippet.Domain.Primitives;

public record ApiResult (bool success, string message, dynamic result);