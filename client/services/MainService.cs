﻿using client.models;
using client.repositories;
using System.Net.Http;
using System.Windows.Forms;

namespace client.services
{
    class MainService
    {
        private ILocationService object1;
        private IPostArchivedService object2;
        private IPostReportedService object3;
        private IPostSavedService object4;
        private IPostsService object5;
        private IUserService object6;

        public ILocationService LocationService { get; }

        public IPostArchivedService PostArchivedService { get; }

        public IPostReportedService PostReportedService { get; }

        public IPostSavedService PostSavedService { get; }

        public IPostsService PostsService { get; }

        public IUserService UserService { get; }

        public List<Post> getAllPosts()
        {
            List<Post> posts = PostsService.getAllPosts();
            posts.ForEach(async post =>
            {
                post.user = UserService.getUserById(post.ownerUserID);
                post.mentionedUsersUsernames = new List<String>();

				post.mentionedUsers.ForEach(user =>
                {
                    User? curUser = UserService.getUserById(user);
                    if(curUser != null)
					    post.mentionedUsersUsernames.Add(curUser.Username);
                });
                post.LikesCount = PostsService.getPostLikeCount(post.id);

				if (!post.locationID.Contains("+"))
				{
                    
					Location location = await LocationService.GetLocationById(post.locationID);
					if (location != null)
					{
						post.locationName = location.Name;
					}
				}


			});
            return posts;
        }

        public async Task<List<Post>> getAllSavedPosts()
        {
            List<PostSaved> posts = PostSavedService.getPostSavedList();
            List<Post> allSavedPosts = new List<Post>();

            foreach (PostSaved post in posts)
            {
                Post savedPost = PostsService.getPostById(post.post_id);

                allSavedPosts.Add(savedPost);
            }

            foreach (Post post in allSavedPosts)
            {
                post.user = UserService.getUserById(post.ownerUserID);
                post.mentionedUsersUsernames = new List<String>();

                post.mentionedUsers.ForEach(user =>
                {
                    User? curUser = UserService.getUserById(user);
                    if (curUser != null)
                        post.mentionedUsersUsernames.Add(curUser.Username);
                });
                post.LikesCount = PostsService.getPostLikeCount(post.id);

                if (!post.locationID.Contains("+"))
                {

                    Location location = await LocationService.GetLocationById(post.locationID);
                    
                    if (location != null)
                    {
                        post.locationName = location.Name;
                    }
                }


            }

            return allSavedPosts;
        }

        public async Task<List<Post>> getAllArchivedPosts()
        {
            List<PostArchived> posts = PostArchivedService.getPostArchivedList();
            List<Post> allArchivedPosts = new List<Post>();

            foreach (PostArchived post in posts)
            {
                Post archivedPost = PostsService.getPostById(post.post_id);

                allArchivedPosts.Add(archivedPost);
            }

            foreach (Post post in allArchivedPosts)
            {
                post.user = UserService.getUserById(post.ownerUserID);
                post.mentionedUsersUsernames = new List<String>();

                post.mentionedUsers.ForEach(user =>
                {
                    User? curUser = UserService.getUserById(user);
                    if (curUser != null)
                        post.mentionedUsersUsernames.Add(curUser.Username);
                });
                post.LikesCount = PostsService.getPostLikeCount(post.id);

                if (!post.locationID.Contains("+"))
                {

                    Location location = await LocationService.GetLocationById(post.locationID);

                    if (location != null)
                    {
                        post.locationName = location.Name;
                    }
                }


            }

            return allArchivedPosts;
        }

        public MainService()
        {
            // Create an instance of HttpClient
            HttpClient httpClient = new HttpClient();

            // Pass the HttpClient instance to the LocationRepository constructor
            LocationRepository locationRepository = new LocationRepository(httpClient);

            // Instantiate other repositories and services
            PostArchivedRepository postArchivedRepository = new PostArchivedRepository();
            PostSavedRepository postSavedRepository = new PostSavedRepository();
            PostReportedRepository postReportedRepository = new PostReportedRepository();
            PostsRepository postsRepository = new PostsRepository();
            UserRepository userRepository = new UserRepository();

            // Instantiate other services with their respective repositories
            LocationService = new LocationService(locationRepository);
            PostArchivedService = new PostArchivedService(postArchivedRepository);
            PostReportedService = new PostReportedService(postReportedRepository);
            PostSavedService = new PostSavedService(postSavedRepository);
            PostsService = new PostsService(postsRepository);
            UserService = new UserService(userRepository);
        }

        public MainService(ILocationService object1, IPostArchivedService object2, IPostReportedService object3, IPostSavedService object4, IPostsService object5, IUserService object6)
        {
            this.object1 = object1;
            this.object2 = object2;
            this.object3 = object3;
            this.object4 = object4;
            this.object5 = object5;
            this.object6 = object6;
        }
    }
}
