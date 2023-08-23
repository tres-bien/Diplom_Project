using System.Text.Json.Serialization;

namespace Diplom_Project
{
    public class LoginModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [JsonIgnore]
        public bool IsAdmin { get; set; } = false;
    }
}