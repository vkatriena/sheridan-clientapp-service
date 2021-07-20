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
    /// Interaction logic for FrmViewOrders.xaml
    /// </summary>
    public partial class FrmViewOrders : Window
    {
        private FrmOrders _frmOrders;
        public FrmViewOrders(FrmOrders frm)
        {
            InitializeComponent();
            LoadView();
            _frmOrders = frm;
        }

        private void LoadView()
        {
            OrderModel[] lst;
            List<OrderModel> items = new List<OrderModel>();
            using (ServiceClient client = new ServiceClient())
            {
                lst = client.GetOrders();
                foreach (OrderModel item in lst)
                {
                    items.Add(new OrderModel()
                    {
                        OrderID = item.OrderID,
                        CustomerID = item.CustomerID,
                        OrderDate = item.OrderDate.Date,
                        FilledDate = item.FilledDate,
                        Status = item.Status,
                        Amount = item.Amount

                    });
                }
                var descendingList = from order in lst
                                     orderby order.OrderDate descending
                                     select order;
                lvOrders.ItemsSource = descendingList;
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            
            OrderModel item = (OrderModel)lvOrders.SelectedItem;
            int orderID = item.OrderID;
            int customerID = item.CustomerID;
            DateTime orderDate = item.OrderDate;
            DateTime filledDate = (DateTime)item.FilledDate;
            string status = item.Status;
            int amount = item.Amount;
            _frmOrders.SetValues(orderID, customerID, orderDate, filledDate, status, amount);
            this.Close();

        }
    }
}
