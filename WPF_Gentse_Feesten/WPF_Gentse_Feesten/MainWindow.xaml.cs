using Domein.Controllers;
using Domein.Models;
using Domein.Repositories;
using Gui;
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

namespace WPF_Gentse_Feesten
{
    public partial class MainWindow : Window
    {
        private DomeinController _dc;

        public List<Evenement> Evenementen;
        public List<Evenement> TopLevelEvenementen
        {
            get { return Evenementen.Where(x => x.GuidParentEvenement == null).ToList(); }
        }

        public static readonly DependencyProperty SelectedEvenementProperty =
            DependencyProperty.Register(nameof(SelectedEvenement), typeof(Evenement), typeof(MainWindow), new PropertyMetadata(null));

        public Evenement SelectedEvenement
        {
            get { return (Evenement)GetValue(SelectedEvenementProperty); }
            set { SetValue(SelectedEvenementProperty, value); }
        }


        public MainWindow(DomeinController dc)
        {
            InitializeComponent();

            _dc = dc;
            _dc.EnsurePopulated();
            Evenementen = _dc.GeefEvenementen();
            listBoxTopLevelEvenementen.ItemsSource = TopLevelEvenementen;
        }

        private void BtnZoeken_Click(object sender, RoutedEventArgs e)
        {
            listBoxTopLevelEvenementen.ItemsSource = TopLevelEvenementen.Where(x => x.Naam.ToLower().Contains(TxtZoek.Text.ToLower()));
        }

        private void listBoxTopLevelEvenementen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxTopLevelEvenementen.SelectedItem != null)
            {
                SelectedEvenement = (Evenement)listBoxTopLevelEvenementen.SelectedItem;
                listBoxKindEvenementen.ItemsSource = SelectedEvenement.KindEvenementen;
            }
        }

        private void BtnOpenPlanner_Click(object sender, RoutedEventArgs e)
        {
            var plannerWindow = new PlannerWindow(_dc);
            plannerWindow.ShowDialog();
        }

        private void listBoxKindEvenementen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxKindEvenementen.SelectedIndex != -1)
            {
                SelectedEvenement = (Evenement)listBoxKindEvenementen.SelectedItem;
                listBoxKindVanKindEvenementen.ItemsSource = SelectedEvenement.KindEvenementen;
            }
        }

        private void listBoxKindVanKindEvenementen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxKindVanKindEvenementen.SelectedIndex != -1)
            {
                SelectedEvenement = (Evenement)listBoxKindVanKindEvenementen.SelectedItem;
            }
        }

        private void BtnToevoegenAanPlanner_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedEvenement!= null)
            {
                if (_dc.VoegEvenementToeAanPlanner(SelectedEvenement))
                {
                    MessageBox.Show($"Evenement {SelectedEvenement.Naam} toegevoegd aan planner!");
                }
                else
                {
                    MessageBox.Show($"Evenement {SelectedEvenement.Naam} overlapt met een evenement in je planner! Deze is niet toegevoegd");
                }
            }
        }
    }
}
