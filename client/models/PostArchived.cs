using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    internal class PostArchived
    {
        public Guid archive_id { get; set; }
        public Guid post_id { get; set; }

        public PostArchived(Guid archivedPostId, Guid postId)
        {
            this.archive_id = archivedPostId;
            this.post_id = postId;
        }

       
        public override String ToString()
        {
            return "PostArchived{" +
                    "archivedPostId=" + archive_id +
                    ", postId=" + post_id +
                    '}';
        }
    }
}
