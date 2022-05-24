using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows;


namespace aerodrom
{
    public class DB
    {
        public OleDbCommand command;
        public OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=baza.accdb;Persist Security Info=False;");

        public DB()
        {
            command = connection.CreateCommand();
        }

        public void Snimanje(Avion Avion)
        {
            try
            {
                connection.Open();
                command.CommandText = $@"INSERT INTO Avion VALUES ('{Avion.Tip}', '{Avion.SerijskiBroj}', '{Avion.RegistracioniTip}','{Avion.Vlasnik}', {Avion.BrojSedista}, {Avion.Kapacitet}, {Avion.Nosivost}, {Avion.BrojRaketa})";
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Uspesno ste dodali u bazu");
            }
            catch (OleDbException ex)   
            {
                if (connection != null)
                    connection.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public void Izmeni(Avion Avion)
        {
            try
            {
                connection.Open();
                command.CommandText = $@"UPDATE Avion SET Tip = '{Avion.Tip}', SerijskiBroj = '{Avion.SerijskiBroj}', RegistracioniTip = '{Avion.RegistracioniTip}', Vlasnik = '{Avion.Vlasnik}', BrojSedista = {Avion.BrojSedista}, Kapacitet = {Avion.Kapacitet}, Nosivost = {Avion.Nosivost}, BrojRaketa = {Avion.BrojRaketa}";
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Uspesno azurirana baza");
            }
            catch (OleDbException ex)
            {
                if (connection != null)
                    connection.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public void Brisanje(Avion Avion)
        {
            try
            {
                if (MessageBox.Show($"Da li ste sigurni da hocete da obrisete {Avion.SerijskiBroj}?", "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    connection.Open();
                    command.CommandText = $@"DELETE FROM Avion WHERE SerijskiBroj = '{Avion.SerijskiBroj}'";
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Uspesno obrisano");
                }
            }
            catch (OleDbException ex)
            {
                if (connection != null)
                    connection.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable CitanjeP()
        {
            DataTable dt = new DataTable();
            try
            {
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM Avion", connection);

                connection.Open();

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    adapter.Fill(dt);

                connection.Close();
            }
            catch (OleDbException ex)
            {
                if (connection != null)
                    connection.Close();
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
    }
}
