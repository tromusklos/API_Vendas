using API.Models;
using FluentValidation;

namespace API.Validation
{
    public class FornecedorValidacao : AbstractValidator<Fornecedor>
    {
        public FornecedorValidacao()
        {
            RuleFor(i => i.Id).NotNull();
            RuleFor(n => n.Nome).NotEmpty();
            RuleFor(c => c.Cnpj).IsValidCNPJ();
        }
    }
}