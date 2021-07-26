using DotNet5Framework.Entities.Dtos.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet5Framework.Business.ValidationRules.FluentValidation
{
    public class ProductAddDtoValidator : AbstractValidator<ProductAddDto>
    {
        public ProductAddDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).Length(10, 100);
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.Price).GreaterThanOrEqualTo(1);
            RuleFor(p => p.Price).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //RuleFor(p => p.Name).Must(StartWithWithA);
        }
        private bool StartWithWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
