using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DeliveryApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Nome: ")]
        [Required(ErrorMessage = "Digite o nome do produto")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite os detalhes do produto")]
        [Display(Name = "Detalhes: ")]
        public string Details { get; set; }

        [Display(Name = "Preço: ")]
        [Range(0, 9999999.99, ErrorMessage = "O valor deve estar entre {1} e {2}.")]
        public double Price { get; set; }

        [Display(Name = "Imagem: ")]
        [ValidateNever]
        public string Image { get; set; }

        public int QuantityInCart { get; set; }


        public void Add1QuantityInCart()
        {
            QuantityInCart++;
        }

        public void Remove1QuantityInCart()
        {
            QuantityInCart--;
        }
    }
}
