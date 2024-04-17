using client.models;
using client.repositories;
using client.services;
using client.modules;
using System.Configuration;
using System.Data;
using System.Windows;

namespace client
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application

	{
		App()
		{
			MainService mainService = new MainService();
			MainWindow m = new MainWindow(mainService);
			m.Show();
		}
	}

}
