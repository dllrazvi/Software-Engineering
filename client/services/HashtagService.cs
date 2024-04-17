using client.models;
using client.modules;
using client.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.services
{
    internal class HashtagService
    {
        private HashtagRepository hashtagRepository;

        public HashtagService(HashtagRepository hashtagRepository)
        {
            this.hashtagRepository = hashtagRepository;
        }

        public HashtagService()
        {
            this.hashtagRepository = new HashtagRepository();
        }

        public bool addPostToHashtag(Guid postId, String hashtagName)
        {
            String post_id = postId.ToString().ToUpper();
            if (!this.hashtagRepository.searchPostHashtagPair(post_id, searchHashtag(hashtagName).Id.ToString().ToUpper()))
            {
                return hashtagRepository.addHashtagPost(post_id, this.searchHashtag(hashtagName).Id.ToString().ToUpper());
            }
            return false;
        }

        public bool removePostFromHashtag(Guid postId, String hashtagName)
        {
            String post_id = postId.ToString().ToUpper();
            if (this.hashtagRepository.searchPostHashtagPair(post_id, searchHashtag(hashtagName).Id.ToString().ToUpper()))
            {
                //return hashtagRepository.addHashtagPost(post_id, this.searchHashtag(hashtagName).Id.ToString().ToUpper());
                return hashtagRepository.removePostFromHashtag(postId.ToString(), this.searchHashtag(hashtagName).Id.ToString().ToUpper());
            }
            return false;
        }

        public bool addHashtag(String name)
        {
            Hashtag newHashtag = new Hashtag(name);

            if (searchHashtag(name) == null)
            {
                return this.hashtagRepository.addHashtag(newHashtag);
            }
            return false;
        }

        public bool removeHashtag(String id)
        {
            return hashtagRepository.removeHashtag(id);
        }

        public Hashtag searchHashtag(String name)
        {
            return this.hashtagRepository.searchHashtag(name);
        }

        public List<Post> getAllPostsFromHashtag(String name)
        {
            return this.hashtagRepository.getAllPosts(searchHashtag(name).Id.ToString());
        }
    }
}
