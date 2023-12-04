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
        private const string cnstr = "server=localhost;uid=Jose;pwd=josepablo;database=partidospoliticos";




        public ObservableCollection<DatosPartido> Parties
        {
            get { return parties; }
            set
            {
                parties = value;
                OnPropertyChange("users");
            }
        }

        private string acronimo;
        public string Acronimo
        {
            get { return acronimo; }
            set
            {
                acronimo = value;
                OnPropertyChange(nameof(Acronimo));
            }
        }

        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChange(nameof(Nombre));
            }
        }

        private string presidente;
        public string Presidente
        {
            get { return presidente; }
            set
            {
                presidente = value;
                OnPropertyChange(nameof(Presidente));
            }
        }

        private int votes;
        public int Votes
        {
            get { return votes; }
            set
            {
                votes = value;
                OnPropertyChange(nameof(Votes));
            }
        }

        private int seats;
        public int Seats
        {
            get { return seats; }
            set
            {
                seats = value;
                OnPropertyChange(nameof(Seats));
            }
        }

        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public void LoadParties()
        {
            string SQL = $"SELECT partidoId, nombre, acronimo, presidente, seats, votos FROM datospartido;";
            DataTable dt = MySQLDataManagement.LoadData(SQL, cnstr);
            if (dt.Rows.Count > 0)
            {
                if (parties == null) parties = new ObservableCollection<DatosPartido>();
                foreach (DataRow row in dt.Rows)
                {
                    parties.Add(new DatosPartido
                    {
                       
                        Nombre = row[1].ToString(),
                        Acronimo = row[2].ToString(),
                        Presidente = row[3].ToString(),
                        Seats = Convert.ToInt32(row[4]),
                        Votes = Convert.ToInt32(row[5])
                    });
                }
            }
            dt.Dispose();
        }

    }
}

