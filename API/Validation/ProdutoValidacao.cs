using API.Models;
using FluentValidation;

namespace API.Validation
{
    public class ProdutoValidacao : AbstractValidator<Produto>
    {
        public ProdutoValidacao()
        {
            RuleFor(i => i.Id).NotNull();
            RuleFor(n => n.Nome).NotEmpty();
            RuleFor(c => c.CodigoProduto).NotEmpty();
            RuleFor(v => v.Valor).GreaterThan(0);
            RuleFor(v => v.ValorPromo).GreaterThan(0);
            RuleFor(c => c.Categoria).NotEmpty();
            RuleFor(i => i.Imagem).NotEmpty();
            RuleFor(q => q.Quantidade).GreaterThan(0);
            RuleFor(i => i.Fornecedor).NotNull();
        }
    }
}