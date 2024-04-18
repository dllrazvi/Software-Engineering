using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client.models;
using client.repositories;

namespace client.services
{
    internal interface IPostSavedService
    {
        bool addPostSaved(PostSaved postSaved);
        List<PostSaved> getPostSavedList();
        bool removePostSaved(PostSaved postSaved);
    }

    internal class PostSavedService : IPostSavedService
    {
        IPostSavedRepository postSavedRepository;

        public PostSavedService(IPostSavedRepository _postSavedRepository)
        {
            postSavedRepository = _postSavedRepository;
        }

        public bool addPostSaved(PostSaved postSaved)
        {

            return postSavedRepository.addPostSavedtoDB(postSaved);
        }

        public bool removePostSaved(PostSaved postSaved)
        {

            return postSavedRepository.removePostSavedFromDB(postSaved);

        }

        public List<PostSaved> getPostSavedList()
        {
            return postSavedRepository.getAll();
        }
    }
}
