using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Models
{
    public class UserLogin
    {
        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o usuario.")]
        [StringLength(50)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Digite o email.")]
        [EmailAddress]
        [StringLength(100)]
        [JsonProperty("email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite a senha")]
        [JsonProperty("senha")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Digite o tipo de usuario")]
        [StringLength(50)]
        public string Type { get; set; }
    }
}