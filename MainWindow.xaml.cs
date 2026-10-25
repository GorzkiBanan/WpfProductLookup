using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfProductLookup
{
    // Wyszukuje produkt po EAN lub nazwie.
    public partial class MainWindow : Window
    {
        private readonly List<Product> products = new List<Product>
        {
            new Product("5901234123457", "Kabel instalacyjny YDY 3x1.5", "Kable", "S1", "R2", "P1"),
            new Product("5901234123468", "Gniazdo podwójne białe", "Osprzęt elektryczny", "S1", "R1", "P3"),
            new Product("5901234123477", "Żarówka LED E27 10 W", "Oświetlenie", "S2", "R3", "P2")
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchBox.Text.Trim();
            ResultCard.Visibility = Visibility.Collapsed;

            if (query.Length == 0)
            {
                MessageText.Text = "Wpisz kod EAN lub nazwę produktu.";
                return;
            }

            Product? product = products.FirstOrDefault(item =>
                item.Ean.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                item.Name.Contains(query, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                MessageText.Text = "Nie znaleziono produktu.";
                return;
            }

            ResultName.Text = product.Name;
            ResultEan.Text = "Kod EAN: " + product.Ean;
            ResultCategory.Text = "Kategoria: " + product.Category;
            ResultLocation.Text = "Lokalizacja: " + product.Sector + " / " + product.Rack + " / " + product.Shelf;
            MessageText.Text = "Znaleziono produkt.";
            ResultCard.Visibility = Visibility.Visible;
        }

        private sealed class Product
        {
            public string Ean { get; }
            public string Name { get; }
            public string Category { get; }
            public string Sector { get; }
            public string Rack { get; }
            public string Shelf { get; }

            public Product(string ean, string name, string category, string sector, string rack, string shelf)
            {
                Ean = ean;
                Name = name;
                Category = category;
                Sector = sector;
                Rack = rack;
                Shelf = shelf;
            }
        }
    }
}
