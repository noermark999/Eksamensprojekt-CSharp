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
using System.Windows.Shapes;

namespace Eksamensprojekt
{
    /// <summary>
    /// Interaction logic for AddCarWindow.xaml
    /// </summary>
    public partial class AddCarWindow : Window
    {
        public Car Car { get; set; }
        CarBLL carBLL = new CarBLL();
        public AddCarWindow()
        {
            InitializeComponent();
            Car = new Car();
        }

        private void BtnAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            var AddPassengerWindow = new AddPassengerWindow();
            if (AddPassengerWindow.ShowDialog() == true)
            {
                
                if (Car != null)
                {
                    try
                    {
                        carBLL.AddPassengerToCar(AddPassengerWindow.Passenger, Car);
                        ListPassengers.Items.Add(AddPassengerWindow.Passenger);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                    AddPassengerWindow.Passenger = null;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Car.Name = TbName.Text;
            Car.Numberplate = TbNumberplate.Text;
            Window.GetWindow(this).DialogResult = true;
            Window.GetWindow(this).Close();
        }
    }
}
