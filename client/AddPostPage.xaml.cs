using client.models;
using client.services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace client
{
	public partial class AddPostPage : Page
	{
		private Dictionary<Guid, string> selectedUsersDictionary = new Dictionary<Guid, string>();
		private string description = "";
		private string filepath = "";
		private string selectedLocation = "";
		private List<User> allUsersList;
		// Simulated list of users (replace with actual user data)

		private MainService service;

		internal AddPostPage(MainService _service)
		{
			InitializeComponent();
			service = _service;
			allUsersList = service.UserService.getAllUsers();
			PopulateUserSearchList(allUsersList);
		}

		
		
		private void NextButton_Click(object sender, RoutedEventArgs e)
		{
			// Step 1: Adding a text description - Hide this panel and show the next panel
			txtDescriptionPanel.Visibility = Visibility.Collapsed; // Hide text description panel
			mediaUploadPanel.Visibility = Visibility.Visible; // Show media upload panel
			description = txtDescription.Text;
			
		}

		private void UploadMediaButton_Click(object sender, RoutedEventArgs e)
		{
			UploadMedia();
		}

		private void UploadMedia()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif|All files (*.*)|*.*";
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); // Set initial directory

			if (openFileDialog.ShowDialog() == true)
			{
				// The user selected a file
				string filePath = openFileDialog.FileName;
				filepath = filePath;

				// Set the source of the image preview control
				imagePreview.Source = new BitmapImage(new Uri(filePath));
				imagePreview.Visibility = Visibility.Visible; // Show the image preview
			}
		}

		private void NextButton2_Click(object sender, RoutedEventArgs e)
		{
			// Step 2: Uploading media - Hide this panel and show the next panel
			mediaUploadPanel.Visibility = Visibility.Collapsed; // Hide media upload panel
			locationPanel.Visibility = Visibility.Visible; // Show location selection panel
		}
		List<Location> locations;
		private async void LocationSearchTextBox_KeyUpAsync(object sender, KeyEventArgs e)
		{
			// Check if the Enter key is pressed
			if (e.Key == Key.Enter)
			{
				// Perform location search based on user input and populate dropdown list
				string searchQuery = txtLocationSearch.Text.Trim();
				if (!string.IsNullOrWhiteSpace(searchQuery))
				{
					// Simulated location search results for demonstration
					locations = await service.LocationService.SearchLocations(searchQuery);

					// Populate the dropdown with location names
					locationDropdown.ItemsSource = locations.Select(loc => loc.Name);
					locationDropdown.Visibility = Visibility.Visible; // Show dropdown list
				}
				else
				{
					locationDropdown.Visibility = Visibility.Collapsed; // Hide dropdown list if search query is empty
				}
			}
		}



		private void LocationDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Update the selected location when the user selects an item from the dropdown list
			string selectedLocationName = locationDropdown.SelectedItem as string;
			if (selectedLocationName != null)
			{
				foreach (Location loc in locations)
				{
					if (loc.Name == selectedLocationName)
					{
						selectedLocation = loc.Id;
						break; 
					}
				}
			}
		}


		private void NextButton3_Click(object sender, RoutedEventArgs e)
		{
			// Step 3: Selecting physical location - Hide this panel and show the next panel
			locationPanel.Visibility = Visibility.Collapsed; // Hide location selection panel
			mentionPanel.Visibility = Visibility.Visible; // Show mention users panel

			// Perform any other necessary actions when moving to the next step
		}

		private void UserSearchTextBox_KeyUp(object sender, RoutedEventArgs e)
		{
			string searchQuery = txtUserSearch.Text.Trim().ToLower(); // Convert search query to lowercase

			// Filter the user list based on the search query
			List<User> filteredUsers = allUsersList.Where(user => user.Username.ToLower().Contains(searchQuery)).ToList();
			PopulateUserSearchList(filteredUsers);
		}


		// Method to update the ListBox with matching users
		private void PopulateUserSearchList(List<User> users)
		{
			// Clear the existing items from the ListBox
			userSearchListBox.Items.Clear();

			// Update the ListBox with matching users
			foreach (User user in users)
			{
				userSearchListBox.Items.Add(user);
			}
		}

		private void NextButton4_Click(object sender, RoutedEventArgs e)
		{
			// Step 4: Mentioning users - Hide this panel and show the next panel
			mentionPanel.Visibility = Visibility.Collapsed; // Hide mention users panel
			postPanel.Visibility = Visibility.Visible; // Show post panel

			// Store the selected users globally
			selectedUsersDictionary.Clear();
			foreach (User selectedUser in selectedUsersListBox.Items)
			{
				selectedUsersDictionary.Add(selectedUser.Id, selectedUser.Username);
			}
		}

		private void UserSearchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Move selected user from user search ListBox to selected users ListBox
			MoveSelectedUser(userSearchListBox, selectedUsersListBox);
		}

		private void SelectedUsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Move selected user from selected users ListBox to user search ListBox
			MoveSelectedUser(selectedUsersListBox, userSearchListBox);
		}

		private void MoveSelectedUser(ListBox sourceListBox, ListBox destinationListBox)
		{
			if (sourceListBox.SelectedItem != null)
			{
				User selectedUser = sourceListBox.SelectedItem as User;

				// Check if the user is already selected
				if (destinationListBox.Items.Contains(selectedUser))
				{
					MessageBox.Show("User already selected.");
					return;
				}

				// Remove from source ListBox
				sourceListBox.Items.Remove(selectedUser);

				// Add to destination ListBox
				destinationListBox.Items.Add(selectedUser);

				// Clear selection
				sourceListBox.SelectedItem = null;

				// No need to update the selected users dictionary here since it's handled in NextButton4_Click
			}
		}
		private void PostButton_Click(object sender, RoutedEventArgs e)
		{
			// Step 5: Posting - Perform posting action
			string mentions = "";
			foreach (var kvp in selectedUsersDictionary)
			{
				mentions += kvp.Value + "\n";
			}
			if (string.IsNullOrEmpty(selectedLocation))
			{
				selectedLocation = "+";
			}

			if(service.PostsService.addPost(Guid.Parse("D6666B72-7E1F-4A8D-9226-38F6DA717A77"), description, selectedUsersDictionary.Keys.ToList(), Guid.Empty, Guid.Empty, filepath, 1, selectedLocation))
			{
				MessageBox.Show("Post was added successfully");
			}else MessageBox.Show("Post could not be created");

		}
	}
}
