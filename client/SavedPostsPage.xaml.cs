using client.models;
using client.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace client
{
    /// <summary>
    /// Interaction logic for SavedPostsPage.xaml
    /// </summary>
    /// 
    public partial class SavedPostsPage : Page
    {
        private MainService service;
        public ObservableCollection<Post> Posts { get; set; }

        internal SavedPostsPage(MainService mainService)
        {
            InitializeComponent();
            service = mainService;
            _ = InitializeAsync();
            // Fetch posts from the repository
        }

        private async Task InitializeAsync()
        {
            Posts = new ObservableCollection<Post>(await service.getAllSavedPosts());
            PostsControl.Items.Clear();
            DataContext = this;

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage(service);
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.Content = loginPage;
            }
        }

        private void ReportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            Post parentPost = comboBox.DataContext as Post;
            ReportModalWindow reportModal = new ReportModalWindow(service, parentPost.id);
            reportModal.Owner = Window.GetWindow(this);
            reportModal.ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Name == "ReportMenuItem")
                {
                    Post parentPost = comboBox.DataContext as Post;
                    ReportModalWindow reportModal = new ReportModalWindow(service, parentPost.id);
                    reportModal.Owner = Window.GetWindow(this);
                    reportModal.ShowDialog();
                    comboBox.SelectedIndex = -1;
                }

                if (selectedItem.Name == "SaveMenuItem")
                {
                    Post parentPost = comboBox.DataContext as Post;
                    service.PostSavedService.addPostSaved(new PostSaved(Guid.NewGuid(), parentPost.id, Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77")));
                    comboBox.SelectedIndex = -1;
                }

                if (selectedItem.Name == "ArchiveMenuItem")
                {
                    Post parentPost = comboBox.DataContext as Post;
                    service.PostArchivedService.addPostArchived(new PostArchived(Guid.NewGuid(), parentPost.id));
                    comboBox.SelectedIndex = -1;
                }

                if (selectedItem.Name == "DeleteMenuItem")
                {
                    Post parentPost = comboBox.DataContext as Post;
                    service.PostsService.removePost(parentPost.id);
                    comboBox.SelectedIndex = -1;
                }

                if (selectedItem.Name == "EditMenuItem")
                {
                    Post parentPost = comboBox.DataContext as Post;
                    EditPostPopup editPostPopup = new EditPostPopup(service, parentPost.id);
                    editPostPopup.Owner = Window.GetWindow(this);
                    editPostPopup.ShowDialog();
                }
            }
        }

        private void shareButton1_Click(object sender, RoutedEventArgs e)
        {
            SharePostWindow sharePost = new SharePostWindow();
            sharePost.Owner = Window.GetWindow(this);
            sharePost.ShowDialog();
        }

        private void openPostButton1_Click(object sender, RoutedEventArgs e)
        {
            PostWindow post = new PostWindow();
            post.Owner = Window.GetWindow(this);
            post.ShowDialog();
        }

        private void openPostButton2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LikesCount_Click(object sender, RoutedEventArgs e)
        {
            // Get the Button that triggered the event
            Button button = sender as Button;

            // Get the DataContext of the Button, which should be the Post object
            if (button?.DataContext is Post post)
            {
                // Access the Post object's properties, such as its Id
                Guid postId = post.id;
                service.PostsService.addReactionToPost(postId, post.ownerUserID, 0);
                // Now you can use the postId as needed
            }
        }
    }
}
