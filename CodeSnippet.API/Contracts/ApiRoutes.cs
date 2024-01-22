using Microsoft.IdentityModel.Tokens;

namespace CodeSnippet.API.Contracts;

public static class ApiRoutes
{
    private const string BaseUrl = "/code-snippet";

    public static class Role
    {
        public const string Post    = $"{BaseUrl}/api/v{{version:apiVersion}}/role/create";
        public const string GetAll  = $"{BaseUrl}/api/v{{version:apiVersion}}/role/list";
        public const string GetById = $"{BaseUrl}/api/v{{version:apiVersion}}/role/find";
        public const string Update  = $"{BaseUrl}/api/v{{version:apiVersion}}/role/update";
        public const string Delete  = $"{BaseUrl}/api/v{{version:apiVersion}}/role/remove";
    }
}