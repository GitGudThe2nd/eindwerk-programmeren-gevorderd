using Domein.Controllers;
using Domein.Models;
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

namespace Gui
{
    /// <summary>
    /// Interaction logic for PlannerWindow.xaml
    /// </summary>
    public partial class PlannerWindow : Window
    {
        private DomeinController _dc;
        private List<Evenement> _evenementen;

        public PlannerWindow(DomeinController dc)
        {
            InitializeComponent();
            _dc = dc;

            _evenementen = _dc.GeefPlannerEvenementen();
            listBoxEvenementen.ItemsSource = _evenementen;
        }

        private void BtnZoeken_Click(object sender, RoutedEventArgs e)
        {
            listBoxEvenementen.ItemsSource = _evenementen.Where(x => x.Naam.ToLower().Contains(TxtZoek.Text.ToLower()));
        }

        private void BtnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if(listBoxEvenementen.SelectedItem != null)
            {
                _dc.VerwijderEvenementVanPlanner((Evenement)listBoxEvenementen.SelectedItem);
                _evenementen = _dc.GeefPlannerEvenementen();
                listBoxEvenementen.ItemsSource = _evenementen.Where(x => x.Naam.ToLower().Contains(TxtZoek.Text.ToLower()));
            }
        }
    }

}
