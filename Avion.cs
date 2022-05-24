using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace aerodrom
{
    public class Avion : INotifyPropertyChanged
    {
        private string tip;
        private string serijskiBroj;
        private string registracioniTip;
        private string vlasnik;
        private int brojSedista;
        private int kapacitet;
        private int nosivost;
        private int brojRaketa;

        public int BrojRaketa
        {
            get { return brojRaketa; }
            set { brojRaketa = value;
                OnPropertyChanged();
            }
        }


        public int Nosivost
        {
            get { return nosivost; }
            set
            {
                nosivost = value;
                OnPropertyChanged();
            }
        }
        public int Kapacitet
        {
            get { return kapacitet; }
            set
            {
                kapacitet = value;
                OnPropertyChanged();
            }
        }
        public int BrojSedista
        {
            get { return brojSedista; }
            set
            {
                brojSedista = value;
                OnPropertyChanged();
            }
        }
        public string Vlasnik
        {
            get { return vlasnik; }
            set
            {
                vlasnik = value;
                OnPropertyChanged();
            }
        }
        public string RegistracioniTip
        {
            get { return registracioniTip; }
            set
            {
                registracioniTip = value;
                OnPropertyChanged();
            }
        }
        public string SerijskiBroj
        {
            get { return serijskiBroj; }
            set
            {
                serijskiBroj = value;
                OnPropertyChanged();
            }
        }
        public string Tip
        {
            get { return tip; }
            set
            {
                tip = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
