using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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


namespace aerodrom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, INotifyPropertChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        public ObservableCollection<Avion> Avioni { get; set; }
        public DB Db { get; set; }
        private Avion avion;

        public Avion Avion
        {
            get { return avion; }
            set
            {
                avion = value;
                OnPropertyChanged();
            }
        }

        // public object DateTable { get; private set; }

        public MainWindow()
        {
            Avioni = new ObservableCollection<Avion>();
            Avion = new Avion();
            Db = new DB();
            InitializeComponent();
            cbTip.Items.Add("Putnicki");
            cbTip.Items.Add("Ratni");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                if (!isTipValid())
                {
                    MessageBox.Show("Broj raketa ne odgovara tipu aviona");
                    return;
                }

                Db.Snimanje(Avion);
                Avion a = new Avion()
                {
                    Tip = Avion.Tip,
                    SerijskiBroj = Avion.SerijskiBroj,
                    RegistracioniTip = Avion.RegistracioniTip,
                    Vlasnik = Avion.Vlasnik,
                    BrojSedista = Avion.BrojSedista,
                    Nosivost = Avion.Nosivost,
                    Kapacitet = Avion.Kapacitet
                };
                Avioni.Add(a);

                Avion.SerijskiBroj = string.Empty;
                Avion.RegistracioniTip = string.Empty;
                Avion.Nosivost = 0;
                Avion.BrojRaketa = 0;
                Avion.BrojSedista = 0;
                Avion.Kapacitet = 0;
                Avion.Vlasnik = string.Empty;
            }
            else
                MessageBox.Show("Sva polja su obavezna");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Db.Brisanje(Avion);
            Avion.SerijskiBroj = string.Empty;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Db.Izmeni(Avion);
            Avion.SerijskiBroj = string.Empty;
            Avion.RegistracioniTip = string.Empty;
            Avion.Nosivost = 0;
            //Avion.BrojRaketa = 0;
            Avion.BrojSedista = 0;
            Avion.Kapacitet = 0;
            Avion.Vlasnik = string.Empty;
        }

        public bool IsValid()
        {
            return (Avion.Nosivost != 0 && !string.IsNullOrEmpty(Avion.SerijskiBroj) && !string.IsNullOrEmpty(Avion.RegistracioniTip) && !string.IsNullOrEmpty(Avion.Vlasnik) && Avion.BrojSedista != 0 && Avion.Nosivost != 0 && Avion.Kapacitet != 0);
        }

        public bool isTipValid()
        {
            if (Avion.Tip == "Putnicki")
            {
                if (Avion.BrojRaketa != 0)
                {
                    return false;
                }
            }
            if (Avion.Tip == "Ratni")
            {
                if (Avion.BrojRaketa == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = Db.CitanjeP();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Avioni.Add(new Avion
                {
                    Tip = dt.Rows[i][0].ToString(),
                    SerijskiBroj = dt.Rows[i][1].ToString(),
                    RegistracioniTip = dt.Rows[i][2].ToString(),
                    Vlasnik = dt.Rows[i][3].ToString(),
                    BrojSedista = (int)dt.Rows[i][4],
                    Kapacitet = (int)dt.Rows[i][5],
                    Nosivost = (int)dt.Rows[i][6],
                    BrojRaketa = (int)dt.Rows[i][7]
                });
            }
        }
    }
}
