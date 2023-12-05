using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;

namespace LeyDeHont.Domain
{
    internal class DatosPartidoVIEW : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<DatosPartido> parties;
        private const string cnstr = "server=localhost;uid=Jose;pwd=josepablo;database=maptrack";




        public ObservableCollection<DatosPartido> Parties
        {
            get { return parties; }
            set
            {
                parties = value;
                OnPropertyChange("Parties");
            }
        }

        private string acronimo;
        public string Acronimo
        {
            get { return acronimo; }
            set
            {
                acronimo = value;
                OnPropertyChange("Acronimo");
            }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChange("Nombre");
            }
        }

        private string presidente;
        public string Presidente
        {
            get { return presidente; }
            set
            {
                presidente = value;
                OnPropertyChange("Presidente");
            }
        }

        private int votes;
        public int Votes
        {
            get { return votes; }
            set
            {
                votes = value;
                OnPropertyChange("Votes");
            }
        }

        private int seats;
        public int Seats
        {
            get { return seats; }
            set
            {
                seats = value;
                OnPropertyChange("Seats");
            }
        }

        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NewUser()
        {
            string SQL = $"INSERT INTO datospartido (nombre, acronimo, presidente, seats, votos) VALUES ('{Nombre}','{Acronimo}', '{Presidente}', {Seats}, {Votes});";
            // use the MySQL.Data library classes to execute queries
            // Install the MySQL.Data package
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }
        public void UpdateParty()
        {
            string SQL = $"UPDATE datospartido SET nombre = '{Nombre}', acronimo = '{Acronimo}', presidente = '{Presidente}', seats = '{Seats}', votos = '{Votes}' WHERE nombre = '{Nombre}';";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
        }

        public void UpdateAllParties()
        {
            foreach (DatosPartido partido in Parties)
            {
                string SQL = $"UPDATE datospartido SET nombre = '{partido.Nombre}', acronimo = '{partido.Acronimo}', presidente = '{partido.Presidente}', seats = '{partido.Seats}', votos = '{partido.Votes}' WHERE nombre = '{partido.Nombre}';";
                MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);
            }
        }

        public void LoadParties()
        {
            string SQL = $"SELECT  nombre, acronimo, presidente, seats, votos FROM datospartido;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);
            if (dt.Rows.Count > 0)
            {
                if (parties == null) parties = new ObservableCollection<DatosPartido>();
                foreach (DataRow row in dt.Rows)
                {
                    parties.Add(new DatosPartido
                    {
                       
                        Nombre = row[0].ToString(),
                        Acronimo = row[1].ToString(),
                        Presidente = row[2].ToString(),
                        Seats = Convert.ToInt32(row[3]),
                        Votes = Convert.ToInt32(row[4])
                    });
                }
            }
            dt.Dispose();
        }
        public void DeleteParty(string partyName)
        {
            // Realiza la eliminación del partido en la base de datos
            String SQL = $"DELETE FROM datospartido WHERE nombre = '{partyName}';";
            MySQLDataManagement.ExecuteNonQuery(SQL, cnstr);

            // Elimina el partido de la colección local
            var partyToDelete = parties.FirstOrDefault(p => p.Nombre == partyName);
            if (partyToDelete != null)
            {
                parties.Remove(partyToDelete);
            }
        }

    }
}

