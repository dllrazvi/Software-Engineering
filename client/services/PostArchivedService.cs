using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client.models;
using client.repositories;

namespace client.services
{
    internal class PostArchivedService
    {
        PostArchivedRepository postArchivedRepository;

        public PostArchivedService(PostArchivedRepository _postArchivedRepository)
        {
            postArchivedRepository = _postArchivedRepository;
        }

        public bool addPostArchived(PostArchived postArchived)
        {
         
        
            return postArchivedRepository.addPostArchivedToDB(postArchived);
        }

        public bool removePostArchived(PostArchived postArchived)
        {
          return postArchivedRepository.removePostArchivedFromDB(postArchived);
        }


        public List<PostArchived> getPostArchivedList()
        {
            return postArchivedRepository.getAll(); 
        }


    }
}
