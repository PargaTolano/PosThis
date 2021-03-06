using System;

namespace Rest_API_PWII.Models
{
    public class Repost
    {

        public int          RepostID    { get; set; }
        public DateTime?    RepostDate  { get; set; }
        public string       UserID      { get; set; }
        public int          PostID      { get; set; }
        public virtual User User        { get; set; }
        public virtual Post Post        { get; set; }
    }
}
