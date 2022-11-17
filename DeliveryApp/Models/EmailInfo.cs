using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DeliveryApp.Models
{
    public class EmailInfo
    {
        [Display(Name = "Email: ")]
        public string Mail { get; set; }
        [Display(Name = "Nome: ")]
        public string Name { get; set; }
    }
}
