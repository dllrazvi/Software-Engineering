using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client.models;
using client.repositories;
namespace client.services
{
    internal interface IPostReportedService
    {
        bool addPostReported(PostReported postReported);
        List<PostReported> getPostReportedList();
        bool removePostReported(PostReported postReported);
    }

    internal class PostReportedService : IPostReportedService
    {
        IPostReportedRepository postReportedRepository;

        public PostReportedService(IPostReportedRepository _postReportedRepository)
        {
            postReportedRepository = _postReportedRepository;
        }

        public bool addPostReported(PostReported postReported)
        {


            return postReportedRepository.addReportedPostToDB(postReported);
        }

        public bool removePostReported(PostReported postReported)
        {


            return postReportedRepository.removeReportedPostFromDB(postReported);

        }

        public List<PostReported> getPostReportedList()
        {
            return postReportedRepository.getAll();
        }

    }
}
