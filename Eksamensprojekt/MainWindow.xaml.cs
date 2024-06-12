using BLL;
using DTO.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eksamensprojekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        FerryBLL ferryBLL = new FerryBLL();
        PassengerBLL passengerBLL = new PassengerBLL();
        CarBLL carBLL = new CarBLL();

        private void BtnFerry_Click(object sender, RoutedEventArgs e)
        {
            ListBoxFerry.Items.Clear();
            foreach (var ferry in  ferryBLL.GetFerryList())
            {
                ListBoxFerry.Items.Add(ferry);
            }
        }

        private void ListBoxFerry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ferry ferry = (Ferry)ListBoxFerry.SelectedItem;
            if (ferry != null)
            {
                TbFerryName.Text = ferry.Name;
                TbFerryPassengers.Text = ferry.Passengers.Count.ToString();
                TbFerryCars.Text = ferry.Cars.Count.ToString();
                TbFerryPrice.Text = ferryBLL.GetFerryPrice(ferry).ToString();
                TbFerryMaxPass.Text = ferry.MaxPassengers.ToString();
                TbFerryMaxCars.Text = ferry.MaxCars.ToString();
                TbFerryPricePass.Text = ferry.PricePassenger.ToString();
                TbFerryPriceCar.Text = ferry.PriceCar.ToString();

                ComboBoxPassengers.ItemsSource = ferry.Passengers;

                ComboBoxCar.ItemsSource = ferry.Cars;
            }
        }

        private void BtnEditFerry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Ferry oldFerry = (Ferry)ListBoxFerry.SelectedItem;
                if (oldFerry != null)
                {
                    Ferry newFerry = new Ferry
                    {
                        FerryID = oldFerry.FerryID,
                        Name = oldFerry.Name,
                        MaxPassengers = int.Parse(TbFerryMaxPass.Text),
                        MaxCars = int.Parse(TbFerryMaxCars.Text),
                        PriceCar = int.Parse(TbFerryPriceCar.Text),
                        PricePassenger = int.Parse(TbFerryPricePass.Text),
                        Cars = oldFerry.Cars,
                        Passengers = oldFerry.Passengers
                    };
                    ferryBLL.EditFerry(newFerry);
                } else
                {
                    MessageBox.Show("Select ferry first");
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ReloadList();
        }

        private void ReloadList()
        {
            ListBoxFerry.Items.Clear();
            foreach (var f in ferryBLL.GetFerryList())
            {
                ListBoxFerry.Items.Add(f);
            }
        }

        private void BtnAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            var AddPassengerWindow = new AddPassengerWindow(); 
            if (AddPassengerWindow.ShowDialog() == true)
            {
                Ferry ferry = (Ferry)ListBoxFerry.SelectedItem;
                if (ferry != null)
                {
                    try
                    {
                        ferryBLL.AddPassengerToFerry(AddPassengerWindow.Passenger, ferry);
                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    AddPassengerWindow.Passenger = null;
                    ReloadList();
                }
            }
        }

        private void BtnAddCar_Click(object sender, RoutedEventArgs e)
        {
            var AddCarWindow = new AddCarWindow();
            if (AddCarWindow.ShowDialog() == true)
            {
                Ferry ferry = (Ferry)ListBoxFerry.SelectedItem;
                if (ferry != null)
                {
                    try
                    {
                        ferryBLL.AddCarToFerry(AddCarWindow.Car, ferry);
                    } catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                    AddCarWindow.Car = null;
                    ReloadList();
                }
            }
        }

        private void BtnRemovePassenger_Click(object sender, RoutedEventArgs e)
        {
            Passenger passenger = (Passenger)ComboBoxPassengers.SelectedItem;
            if (passenger != null)
            {
                passengerBLL.RemovePassenger(passenger.PassengerID);
            }
        }

        private void BtnRemoveCar_Click(object sender, RoutedEventArgs e)
        {
            Car car = (Car)ComboBoxCar.SelectedItem;
            if (car != null)
            {
                carBLL.RemoveCar(car.CarID);
            }
        }
    }
}
