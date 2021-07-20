using ClientApplication.ServiceReference1;
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


namespace ClientApplication.Forms
{
    /// <summary>
    /// Interaction logic for FrmOrders.xaml
    /// </summary>
    public partial class FrmOrders : Window
    {
        private int _customerId;
        private int _id;
        private DateTime _orderDate;
        private DateTime _filledDate;
        private string _status;
        private int _amount;

        public FrmOrders()
        {
            _id = 0;
            InitializeComponent();
            ViewCustomers();
            tbOrderDate.Text = "MM/DD/YYYY";
            tbFilledDate.Text = "MM/DD/YYYY";
        }

        public void SetValues(int orderID, int customerID, DateTime orderDate, DateTime filledDate, string status, int amount)
        {
            _id = orderID;
            _customerId = customerID;
            _orderDate = orderDate;
            _filledDate = filledDate;
            _status = status;
            _amount = amount;

            lblId.Content = $"Order ID: {_id}";
            tbAmount.Text = _amount.ToString();
            tbFilledDate.Text = _filledDate.ToString();
            tbOrderDate.Text = _orderDate.ToString();
            tbStatus.Text = _status;
            cbCustomers.SelectedValue = customerID;


        }

        private void ReadOrder()
        {
            _orderDate = DateTime.Parse(tbOrderDate.Text);
            _filledDate = DateTime.Parse(tbFilledDate.Text);
            _status = tbStatus.Text;
            _amount = Int32.Parse(tbAmount.Text);
            _customerId = (int)cbCustomers.SelectedValue;
        }

        private void ClearText()
        {
            _id = 0;
            tbOrderDate.Text = "MM/DD/YYYY";
            tbFilledDate.Text = "MM/DD/YYYY";
            tbAmount.Text = "";
            tbStatus.Text = "";
            lblId.Content = $"Order ID: ";
            cbCustomers.SelectedIndex = -1;
        }

        private void ViewCustomers()
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
                        CustomerName = item.CustomerName

                    });
                }
                cbCustomers.ItemsSource = items;
            }
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            FrmViewOrders item = new FrmViewOrders(this);
            item.ShowDialog();
        }

        private void cbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCustomers.SelectedIndex != -1)
            {
                CustomerModel item = (CustomerModel)cbCustomers.SelectedItem;
                _customerId = item.CustomerID;

            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (cbCustomers.SelectedIndex == -1)
                MessageBox.Show("Please select a customer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (string.IsNullOrEmpty(tbOrderDate.Text))
                MessageBox.Show("Please enter Order Date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (string.IsNullOrEmpty(tbStatus.Text))
                MessageBox.Show("Please enter Order Status", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (string.IsNullOrEmpty(tbAmount.Text))
                MessageBox.Show("Please enter Order Amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    ReadOrder();
                    using (ServiceClient client = new ServiceClient())
                    {
                        client.InsertOrder(_customerId, _orderDate, _filledDate, _status, _amount);
                        MessageBox.Show("Order Added", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    }
                    ClearText();

                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Data entered.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_id==0)
                MessageBox.Show("Please select an order from 'View Orders'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (string.IsNullOrEmpty(tbOrderDate.Text))
                MessageBox.Show("Please enter Order Date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (string.IsNullOrEmpty(tbStatus.Text))
                MessageBox.Show("Please enter Order Status", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (string.IsNullOrEmpty(tbAmount.Text))
                MessageBox.Show("Please enter Order Amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    ReadOrder();
                    using (ServiceClient client = new ServiceClient())
                    {
                        client.UpdateOrder(_id, _customerId, _orderDate, _filledDate, _status, _amount);
                    }
                    MessageBox.Show("Order Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Data Entered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (ServiceClient client = new ServiceClient())
            {
                if (_id==0)
                    MessageBox.Show("Please select an order from 'View Orders'", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    client.DeleteOrder(_id);
                    MessageBox.Show("Order Deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    ClearText();

                }
            }

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearText();
        }
    }
}

