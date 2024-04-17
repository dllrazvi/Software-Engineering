using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

using client.services;
using client.models;

namespace client
{
    /// <summary>
    /// Interaction logic for ReportModalWindow.xaml
    /// </summary>
    public partial class ReportModalWindow : Window
    {
        private MainService _service;
        private Guid _postId;
        private String _selectedReason;
        internal ReportModalWindow(MainService service, Guid postId)
        {
            InitializeComponent();
            _service = service;
            _postId = postId;
        }

        private void ReasonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Check if a ComboBoxItem is selected
            if (ReasonComboBox.SelectedItem != null)
            {
                // Get the selected item content
                _selectedReason = (ReasonComboBox.SelectedItem as ComboBoxItem).Content.ToString();

                // Optionally, you can reset the selection if needed
                // ReasonComboBox.SelectedItem = null;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            _service.PostReportedService.addPostReported(
                new PostReported(
                    Guid.NewGuid(), 
                    _selectedReason,
                    ReasonTextBox.Text,
                    _postId,
                    Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77")
                    )
                );
     
            Window.GetWindow(this).Close();
        }
    }
}
