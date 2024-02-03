using Microsoft.IdentityModel.Tokens;

namespace CodeSnippet.API.Contracts;

public static class ApiRoutes
{
    private const string BaseUrl = "/code-snippet";


    /// <summary>
    /// These role screen routies will be used by admin
    /// </summary>

    public static class Role
    {
        public const string Post    = $"{BaseUrl}/api/v{{version:apiVersion}}/role/create";
        public const string GetAll  = $"{BaseUrl}/api/v{{version:apiVersion}}/role/list";
        public const string GetById = $"{BaseUrl}/api/v{{version:apiVersion}}/role/find";
        public const string Update  = $"{BaseUrl}/api/v{{version:apiVersion}}/role/update";
        public const string Delete  = $"{BaseUrl}/api/v{{version:apiVersion}}/role/remove";
    }
    

    /// <summary>
    /// These user screen routies will be used by admin
    /// </summary>
    public static class User
    {
        public const string Post    = $"{BaseUrl}/api/v{{version:apiVersion}}/user/create"; 
        public const string GetAll  = $"{BaseUrl}/api/v{{version:apiVersion}}/user/list";
        public const string GetById = $"{BaseUrl}/api/v{{version:apiVersion}}/user/find";
        public const string Update  = $"{BaseUrl}/api/v{{version:apiVersion}}/user/update";
        public const string Delete  = $"{BaseUrl}/api/v{{version:apiVersion}}/user/remove";
    }


    /// <summary>
    /// These authentication routies will be used by all type of users
    /// </summary>
    public static class Auth
    {
        public const string Register           = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/register-user";
        public const string Login              = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/login-user";
        public const string Logout             = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/logout-user";
        public const string LoginWithGoogle    = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/login-with-google";
        public const string LoginWithMicrosoft = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/login-with-microsoft";
        public const string LoginWithFacebook  = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/login-with-facebook";
    }
}