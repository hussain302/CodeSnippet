using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippet.Infrastructure.Abstractions.Email;

/// <summary>
/// Represents the email service interface.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends the email with the content based on the specified mail request.
    /// </summary>
    /// <param name="mailRequest">The mail request.</param>
    /// <returns>The completed task.</returns>
    //Task SendEmailAsync(MailRequest mailRequest);
}
