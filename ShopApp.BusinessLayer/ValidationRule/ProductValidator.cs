using FluentValidation;
using ShopApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.BusinessLayer.ValidationRule
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün Adı Alanı Boş Geçemezsiniz");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Lütfen en az 3 karakter girişi yapınız");
            RuleFor(x => x.Name).MaximumLength(60).WithMessage("Lütfen 60 karakterden fazla veri girişi yapmayınız");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Ürün Açıklama Alanı Boş Geçilemez");
            RuleFor(x => x.Description).MinimumLength(10).WithMessage("Lütfen en az 10 karakter girişi yapınız");
            RuleFor(x => x.Description).MaximumLength(10000).WithMessage("Lütfen 10000 karakterden fazla veri girişi yapmayınız");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat Belirtiniz");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(1).WithMessage("Lütfen en az 1 TL fiyat tutarında bir giriş yapınız.");
            RuleFor(x => x.Price).LessThanOrEqualTo(10000).WithMessage("Lütfen 10.000 TL'den fazla bir fiyat tutarı girişi yapmayınız.");
        }
    }
}
