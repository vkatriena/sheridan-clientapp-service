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
using ClientApplication.ServiceReference1;

namespace ClientApplication.Forms
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
            CustomerModel[] lst;
            List<CustomerModel> items = new List<CustomerModel>();
            using (ServiceClient client = new ServiceClient())
            {
                lst = client.GetCustomers();
                foreach (CustomerModel item in lst)
                {
                    items.Add(new CustomerModel()
                    {
                        CustomerID = item.CustomerID,
                        CustomerName = item.CustomerName,
                        YTDOrders = item.YTDOrders,
                        YTDSales = item.YTDSales
                    });
                }
                lstCustomer.ItemsSource = items;
            }
        }


        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            using (ServiceClient client = new ServiceClient())
            {

                if (string.IsNullOrEmpty(tbName.Text))
                    MessageBox.Show("Please enter Customer name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (string.IsNullOrEmpty(tbOrders.Text))
                    MessageBox.Show("Please enter YTD Orders", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (string.IsNullOrEmpty(tbSales.Text))
                    MessageBox.Show("Please enter YTS Sales", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    try
                    {
                        ReadCustomer();
                        client.InsertCustomer(name, orders, sales);
                        MessageBox.Show("Customer Added", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        ShowData();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid entry entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void ReadCustomer()
        {
            name = tbName.Text;
            orders = Int32.Parse(tbOrders.Text);
            sales = Int32.Parse(tbSales.Text);
        }
        private void ClearText()
        {
            tbName.Text = "";
            tbOrders.Text = "";
            tbSales.Text = "";
            lstCustomer.SelectedIndex = -1;
            lblId.Content = "Customer ID:";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            using (ServiceClient client = new ServiceClient())
            {
                if (lstCustomer.SelectedIndex == -1)
                    MessageBox.Show("Please select an item from the list below first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if (string.IsNullOrEmpty(tbName.Text))
                        MessageBox.Show("Please enter Customer name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if (string.IsNullOrEmpty(tbOrders.Text))
                        MessageBox.Show("Please enter YTD Orders", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if (string.IsNullOrEmpty(tbSales.Text))
                        MessageBox.Show("Please enter YTS Sales", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        try
                        {
                            ReadCustomer();
                            client.UpdateCustomer(customerID, name, orders, sales);
                            MessageBox.Show("Customer Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            ShowData();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Invalid entry entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (ServiceClient client = new ServiceClient())
            {
                if (lstCustomer.SelectedIndex == -1)
                    MessageBox.Show("Please select an item from the list below first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    CustomerModel item = (CustomerModel)lstCustomer.SelectedItem;
                    client.DeleteCustomer(item.CustomerID);
                    MessageBox.Show("Customer Deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    ShowData();
                    ClearText();
                }
            }
        }

        private void lstCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCustomer.SelectedIndex != -1)
            {
                CustomerModel item = (CustomerModel)lstCustomer.SelectedItem;
                tbName.Text = item.CustomerName;
                tbOrders.Text = item.YTDOrders.ToString();
                tbSales.Text = item.YTDSales.ToString();
                customerID = item.CustomerID;
                lblId.Content = $"Customer ID: {customerID}";
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearText();
        }

    }
}
