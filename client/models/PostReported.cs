using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.models
{
    internal class PostReported
    {
        public Guid report_id { get; set; }
        public Guid post_id { get; set; }
        public String description { get; set; }
        public String reason { get; set;}
        public Guid reporter_id { get; set; }

        public PostReported(Guid reportedPostId, string _reason , string message, Guid postId,  Guid reporterId)
        {
            this.report_id = reportedPostId;
            this.reason = _reason;
            this.description = message;
            this.post_id = postId;
            this.reporter_id = reporterId;
            
        }

        

        public override String ToString()
        {
            return "PostReported{" +
                    "ReportedPostId=" + reporter_id +
                    ", PostId=" + post_id +
                    ", message='" + description + '\'' +
                    ", ReporterId=" + reporter_id +
                    ", Reason=" + reason +
                    '}';
        }
    }
}
