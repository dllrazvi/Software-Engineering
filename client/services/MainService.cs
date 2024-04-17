using client.models;
using client.repositories;
using System.Windows.Forms;

namespace client.services
{
    class MainService
    {
        public LocationService LocationService { get; }

        public PostArchivedService PostArchivedService { get; }

        public PostReportedService PostReportedService { get; }

        public PostSavedService PostSavedService { get; }

        public PostsService PostsService { get; }

        public UserService UserService { get; }

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
            PostArchivedRepository postArchivedRepository = new PostArchivedRepository();
            PostSavedRepository postSavedRepository = new PostSavedRepository();
            PostReportedRepository postReportedRepository = new PostReportedRepository();
            LocationRepository locationRepository = new LocationRepository();
            PostsRepository postsRepository = new PostsRepository();
            UserRepository userRepository = new UserRepository();

            LocationService = new LocationService(locationRepository);
            PostArchivedService = new PostArchivedService(postArchivedRepository);
            PostReportedService = new PostReportedService(postReportedRepository);
            PostSavedService = new PostSavedService(postSavedRepository);
            PostsService = new PostsService(postsRepository);
            UserService = new UserService(userRepository);
        }
    }
}
