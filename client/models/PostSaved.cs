using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    internal class PostSaved
    {
        public Guid save_id { get;set; }
        public Guid post_id { get;set; }
        public Guid user_id { get;set; }

        public PostSaved(Guid savedPostId,  Guid postId ,Guid saveUserId)
        {
            this.save_id = savedPostId;
            this.post_id = postId;
            this.user_id = saveUserId;
        }

       
        public override String ToString()
        {
            return "PostSaved{" +
                    "savedPostId=" + save_id +
                    ", saveUserId=" + post_id +
                    ", postId=" + user_id +
                    '}';
        }
    }
}
