using Microsoft.AspNetCore.Http;

namespace Rest_API_PWII.Models.ViewModels
{
    public class UpdateUserViewModel
    {
        public string       UserName    { get; set; }
        public string       Tag         { get; set; }
        public string       Email       { get; set; }
        public IFormFile    ProfilePic  { get; set; }
        public IFormFile    CoverPic    { get; set; }
    }
}
