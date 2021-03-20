using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class HashtagPost
    {
        public int HashtagID { get; set; }
        
        public int PostID { get; set; }

        /*
         Remove Range para remover registros donde se contenga un Post eliminado con un 

                IQueryable<HastagPost> registrosRemovibles = db.HashtagPosts( x=> x.PostID == idPostEliminado);

                db.RemoveRange( registrosRemovibles );

                Altenativamente se puede usar el Include como parte del query del query builder
         */

        public virtual Hashtag Hashtag { get; set; }

        public virtual Post Post { get; set; }

        public static readonly string ForeignkeyHashtag = "fk_HashtagPost_Hashtag";

        public static readonly string ForeignkeyPost = "fk_HashtagPost_Post";
    }
}
