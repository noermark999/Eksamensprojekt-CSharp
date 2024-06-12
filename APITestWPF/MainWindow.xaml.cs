using DTO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace APITestWPF
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

        private void BtnPassenger_Click(object sender, RoutedEventArgs e)
        {
            if (TbPassengerID.Text != null)
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Clear();
                Task<string> task = client.GetStringAsync("https://localhost:44310/api/PassengerAPI/FindPassenger/" + TbPassengerID.Text);

                String msg = task.Result;

                Passenger passenger = JsonSerializer.Deserialize<Passenger>(msg);
                LblPassenger.Content = passenger;
            }
        }

        private async void BtnPostPassenger_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            Passenger passenger = new Passenger { Name = TbName.Text, Gender = TbGender.Text, Birthdate = (DateTime)DpBD.SelectedDate, FerryID = int.Parse(TbFerryID.Text)};


            StringContent content = new StringContent(JsonSerializer.Serialize(passenger), Encoding.UTF8, "application/json");
            HttpResponseMessage result = client.PostAsync("https://localhost:44310/api/PassengerAPI/PostPassengerObject", content).Result;
            if (result.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseContent = await result.Content.ReadAsStringAsync();

                // Deserialize the response content to a Passenger object
                Passenger returnedPassenger = JsonSerializer.Deserialize<Passenger>(responseContent);

                // Use the returned passenger object as needed
                LblPostPassenger.Content = $"Passenger {returnedPassenger.Name} added successfully!";
            }
            else
            {
                LblPostPassenger.Content = $"Error: {result.StatusCode}";
            }
        }

        private void BtnFerry_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            Task<string> task = client.GetStringAsync("https://localhost:44310/api/FerryAPI/FindFerry/" + TbGetFerryID.Text);

            String msg = task.Result;

            Ferry ferry = JsonSerializer.Deserialize<Ferry>(msg);
            LblFerry.Content = ferry;
        }

        private void BtnPassengers_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            Task<string> task = client.GetStringAsync("https://localhost:44310/api/PassengerAPI/GetAllPassengers/" + TbPassengerListFerryID.Text);

            String msg = task.Result;

            List<Passenger> passengers = JsonSerializer.Deserialize<List<Passenger>>(msg);
            ListBoxPassengers.ItemsSource = passengers;
        }
    }
}
