using client.services;
using System.Windows;
using System.Windows.Controls;

namespace client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainService service;
        internal MainWindow(MainService mainService)
        {
            InitializeComponent();
            service = mainService;
            ShowHome();
            NavigationListBox.SelectionChanged += NavigationListBox_SelectionChanged;
        }

        private void NavigationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationListBox.SelectedItem != null)
            {
                string selectedItem = ((ListBoxItem)NavigationListBox.SelectedItem).Content.ToString();
                switch (selectedItem)
                {
                    case "Home":
                        ShowHome();
                        break;
                    case "Notifications":
                        // ShowView2();
                        break;
                    case "Messages":
                        // ShowView3();
                        break;
                    case "Add Post":
                        ShowAddPost();
                        break;
                    case "Saved Posts":
                        ShowSavedPosts();
                        break;
                    case "Archived Posts":
                        ShowArchivedPosts();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ShowHome()
        {
            contentFrame.Content = new HomePage(service,contentFrame);
        }

        private void ShowAddPost()
        {
            contentFrame.Content = new AddPostPage(service);
        }

        private void ShowSavedPosts()
        {
            contentFrame.Content = new SavedPostsPage(service);
        }

        private void ShowArchivedPosts()
        {
            contentFrame.Content = new ArchivedPostsPage(service);
        }
    }
}
