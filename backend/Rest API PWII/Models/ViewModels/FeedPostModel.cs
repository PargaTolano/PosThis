
namespace Rest_API_PWII.Models.ViewModels
{
    public class FeedPostModel : PostViewModel
    {
        public string                   ReposterID          { get; set; }
        public string                   ReposterUserName    { get; set; }
        public bool                     IsRepost            { get; set; }
        public bool                     IsLiked             { get; set; }
        public bool                     IsReposted          { get; set; }
    }
}
