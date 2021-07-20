using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientWPF.Forms
{
    /// <summary>
    /// Interaction logic for FrmCustomer.xaml
    /// </summary>
    public partial class FrmCustomer : Window
    {
        static string name;
        static int orders;
        static int sales;
        private int customerID;

        public FrmCustomer()
        {
            InitializeComponent();
            ShowData();
        }
        private void ShowData()
        {
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient();
            lstCustomer.DataContext = null;
            lstCustomer.DataContext = client.GetCustomersAsync();
            client.CloseAsync();

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient();          

                client.InsertCustomerAsync(name, orders, sales);
                 client.CloseAsync();
            

        }
        private void ReadCustomer()
        {
            name = tbName.Text;
            orders = Int32.Parse( tbOrders.Text);
            sales = Int32.Parse(tbSales.Text);

        }
    }
}
