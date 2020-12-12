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
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;


namespace doNet5781_03B_4789_9647
{

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 

	public partial class MainWindow : Window
	{
	
		static Random r = new Random(DateTime.Now.Millisecond);
		addWindow wnd;
		ListDrivers wnd1;

		public Bus temp
		{
			get; private set;
		}

		private ObservableCollection<Bus> egged = new ObservableCollection<Bus>(); //list of buses
		public ObservableCollection<Bus> Egged
		{
			get { return egged; }
			set
			{
				;
			}
		}
		private ObservableCollection<Drivers> drivers = new ObservableCollection<Drivers>(); //list of driver


		public MainWindow()
		{
			try
			{
				initBuses(egged); //insert vbuses
				initDrivers(drivers); //insert driver
				InitializeComponent();
				allbuses.ItemsSource = egged;
			}
			catch (Exception)
			{
				MessageBox.Show("");
			}



		}



		private void initBuses(ObservableCollection<Bus> egged) //inser randomaly buses
		{
			int license1;
			DateTime date1;

			for (int i = 0; i < 10; i++) ///restart 10 buses
			{
				date1 = new DateTime(r.Next(1990, DateTime.Today.Year + 1), r.Next(1, 13), r.Next(1, 29)); //random a starting date to the buses
																										  
				do
				{
					if ((date1.Year < 2018)) //if the starting date lee than 2018 we need a licence with 7 digits
					{
						license1 = r.Next(1000000, 10000000); //to random number with 7 digite
					}
					else
					{
						license1 = r.Next(10000000, 100000000); //to random number with 8 digite
					}

				} while (findBuse(egged, license1)); //check if the license exsis

				try //insert the bus to the list
				{
					Bus temp = new Bus(license1, date1); 
					egged.Add(temp);
				}

				catch (Exception)
				{
					i--;
				}

			}


			egged[2].lastTreat = (egged[2].lastTreat.AddYears(-1));
			egged[2].status = Status.Unfit;
			egged[1].lastTreat = DateTime.Now.AddDays(-1);
			egged[3].newKm_from_LastTreatment = 19900;
			egged[4].Fuel = 50;





		}

		private void initDrivers(ObservableCollection<Drivers> drivers) //insert driver
		{

			List<string> names = new List<string>();

			string num1 = "Eli Coen", num2 = "Oria Bat", num3 = "Benel Tavori", num4 = "Itay Ofir", num5 = "Shirel David", num6 = "Noam Oved", num7 = "David Levi", num8 = "Yeonatan Snir", num9 = "Eitan Malka", num10 = "Beny Don";
			names.Add(num1); names.Add(num2); names.Add(num3); names.Add(num4); names.Add(num5); names.Add(num6); names.Add(num7); names.Add(num8); names.Add(num9); names.Add(num10);
			for (int i = 0; i < 10; i++)
			{
				Drivers temp = new Drivers(names[i]);
				drivers.Add(temp);
			}

		}



		private static bool findBuse(ObservableCollection<Bus> buses, int num) //find if specific licence is already exsis at our company bus.
		{
			try
			{
				int temp1;

				foreach (Bus item in buses) //move on the list buses
				{
					string temp = item.License;

					temp = temp.Replace("-", string.Empty); //To remove the hyphens from our license number
					int.TryParse(temp, out temp1);

					if (temp1 == num) //check if the licenes equal
					{
						return true;
					}

				}
				return false;
			}
			catch (Exception)
			{

				MessageBox.Show("problem not recognize");
				return false;
			}



		}

		private void addBus_Click(object sender, RoutedEventArgs e) //to add bus to our company
		{

			try
			{
				wnd = new addWindow();

				bool? result = wnd.ShowDialog();
				if (result == true)
				{
					// in order to check if this bus licence exsis already
					int temp1;
					string a = wnd.myBus.License;
					a = a.Replace("-", string.Empty); //To remove the hyphens from our license number
					int.TryParse(a, out temp1);

					if (!findBuse(egged, temp1)) //if this licence not exsis already so add the bus to our company.
						egged.Add(wnd.myBus);
					else
					{
						throw new ArgumentException("The new licence is already exsis,\n please nter again number licence with 8 digite");
					}

				}
			}
			catch (ArgumentException messege)
			{
				MessageBox.Show(messege.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}


		private void Button_Click(object sender, RoutedEventArgs e) //to send the bus to refulling
		{
			try
			{
				var fxElt = sender as FrameworkElement; //get the licence of the bus to refulling. 
				Bus lineData = fxElt.DataContext as Bus;

				MessageBoxResult result = MessageBox.Show(lineData.fuelString(), "FUELIING", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
				switch (result) //get if the customer want to refull the bus or not
				{
					case MessageBoxResult.Yes:
						{
							((sender as Button).DataContext as Bus).Refuelling(); //gp to refulling the bus
							lineData.NameDriver = ""; 
							allbuses.Items.Refresh();
						}

						break;
					case MessageBoxResult.No:
						break;
				}
			}
			catch (Exception)
			{
				MessageBox.Show("a");
			}
			allbuses.Items.Refresh();
		}



		private void Pick_Button_Click(object sender, RoutedEventArgs e) //to starting travel
		{
			int i = 0;
			Drivers dr = new Drivers(); //driver to the bus
			try
			{ 
				///------------
				/// to choose driver to the travel
				///------------
				
				string name = "";
				int g = 0;
				for (i = 0; i < drivers.Count; i++) //check if there is a driver that can take the travel
				{
					dr = drivers[i];
					if (dr.InTraveling)
						g++;

				}
				if (g == drivers.Count)
					throw new ArgumentException("No drivers available, please wait and try again ");

				else //if there is a driver that can take the travle so radon driver 
                {
					do
					{
						i = r.Next(0, drivers.Count());
						dr = drivers[i];
					} while (drivers[i].InTraveling);
                }
				
				name = drivers[i].Name1; //keep the driver name
			
				StartingTravel smalla = new StartingTravel((sender as Button).DataContext as Bus); //open new window in orser to insert data

				bool ? result = smalla.ShowDialog();

				if(result==true)
                {
					TimeSpan a= drivers[i].SumTime + TimeSpan.FromSeconds(((sender as Button).DataContext as Bus).timeTravel); // calucate the sum of time that this driver did and add the new time to the new travel. in order to check if the driver can take this travel.
										
					((sender as Button).DataContext as Bus).NameDriver = dr.Name1; //connect  the driver to ths bus
					if (wnd1 != null) wnd1.allDriver.Items.Refresh();
					drivers[i].InTraveling = true;
					drivers[i].SumTime += TimeSpan.FromSeconds(((sender as Button).DataContext as Bus).timeTravel); //update the sum time travel of the bus
				}
               

			}
			catch (ArgumentException a)
			{
				MessageBox.Show(a.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			if (wnd1 != null)
			{ wnd1.allDriver.Items.Refresh();}
				allbuses.Items.Refresh();
			//drivers[i].InTraveling = false;


		}


		private void doubleflick(object sender, RoutedEventArgs e) //in order to shoe details on the bus
		{
			var list = (ListView)sender; //to get the bus
			object item = list.SelectedItem;

			Bus_Data temp = new Bus_Data(item); //open window to show bus data
			temp.ShowDialog();

			allbuses.Items.Refresh();
		}



		private void MainWindow_Closing(object sender, CancelEventArgs e)
		{
			Environment.Exit(Environment.ExitCode);
		}


		private void ListDriver_Click(object sender, RoutedEventArgs e) //in order to show the list of drivers and data on them
		{
			
			
			wnd1 = new ListDrivers(drivers, egged);
			
			wnd1.ShowDialog();
			allbuses.Items.Refresh();


		}

	}
}
