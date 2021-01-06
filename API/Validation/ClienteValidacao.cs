using API.Models;
using FluentValidation;

namespace API.Validation
{
    public class ClienteValidacao : AbstractValidator<Cliente>
    {
        public ClienteValidacao()
        {
            RuleFor(m => m.Id).NotNull();
            RuleFor(n => n.Nome).NotEmpty();
            RuleFor(customer => customer.Email).EmailAddress();
            RuleFor(s => s.Senha).MinimumLength(5);
            RuleFor(d => d.Documento).NotEmpty();
            RuleFor(d => d.DataCadastro).NotNull();

        }
    }
}