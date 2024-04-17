using client.services;
using System.Windows;

namespace client
{
    public partial class EditPostPopup : Window
    {
        private MainService _service;
        private Guid _postId;

        internal EditPostPopup(MainService service, Guid postId)
        {
            InitializeComponent();
            _service = service;
            _postId = postId;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            _service.PostsService.updateDescription(_postId, txtDescription.Text);

            Close();
        }
    }
}
