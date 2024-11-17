using FluentEmail.Core;
using Fortificar.Models;

namespace Fortificar.Service
{
    public interface IEmailService
    {
        Task Send(EmailDados email);
    }
}
