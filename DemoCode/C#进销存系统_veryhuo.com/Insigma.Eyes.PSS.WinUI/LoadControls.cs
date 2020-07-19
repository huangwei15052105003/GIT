using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Insigma.Eyes.PSS.WinUI.Controls;

namespace Insigma.Eyes.PSS.WinUI
{
    public class LoadControls
    {
        public static void LoadInventory(Control parent)
        {
            parent.Controls.Clear();
            Inventory inventory = new Inventory();
            inventory.Dock = DockStyle.Fill;
            parent.Controls.Add(inventory);
        }
        public static void LoadPurchase(Control parent)
        {
            parent.Controls.Clear();
            Purchase purchase = new Purchase();
            purchase.Dock = DockStyle.Fill;
            parent.Controls.Add(purchase);
        }
        public static void LoadSales(Control parent)
        {
            parent.Controls.Clear();
            Sales sales = new Sales();
            sales.Dock = DockStyle.Fill;
            parent.Controls.Add(sales);
        }
    }
}
