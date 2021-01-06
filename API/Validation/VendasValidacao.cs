using API.Models;
using FluentValidation;

namespace API.Validation
{
    public class VendasValidacao : AbstractValidator<Venda>
    {
        public VendasValidacao()
        {
            RuleFor(i => i.Id).NotNull().NotEmpty();
            RuleFor(n => n.ClienteId).NotNull().NotEmpty();
            RuleFor(c => c.DataCompra).NotEmpty();
            RuleFor(c => c.TotalCompra).NotEmpty();
        }
    }
}