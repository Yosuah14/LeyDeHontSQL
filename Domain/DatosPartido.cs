using LeyDeHont.Persistence.Manages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace LeyDeHont.Domain
{
    class DatosPartido
    {
        public string Acronimo { get; set; }
        public string Nombre { get; set; }
        public string Presidente { get; set; }
        public int votes {  get; set; }
        public int  seats { get; set; }
        public PartiesManage pm { get; set; }
  

        public DatosPartido()
        {

            pm = new PartiesManage();
        }

        public DatosPartido(string acronimo, string nombre, string presidente)
        {
            Acronimo = acronimo;
            Nombre = nombre;
            Presidente = presidente;
            pm = new PartiesManage();
        }

        public DatosPartido(string acronimo, string nombre, string presidente,int votes)
        {
            Acronimo = acronimo;
            Nombre = nombre;
            Presidente = presidente;
            pm = new PartiesManage();
            this.votes = votes;
        }
        public DatosPartido(string acronimo, string nombre, string presidente, int votes,int seats)
        {
            Acronimo = acronimo;
            Nombre = nombre;
            Presidente = presidente;
            pm = new PartiesManage();
            this.votes = votes;
            this.seats = seats;
        }



        public List<DatosPartido> getListParties()
        {
            return pm.listParties;
        }

        public void addParties(string name, string nPartido, string presidente)
        {
            pm.addPartdio(name, nPartido, presidente);
        }

        public void removeParties(DatosPartido p)
        {
            pm.listParties.Remove(p);

        }
        public static int calculateVotesPartie(double percent,PreviousData p)
        {
            double votesDouble = p.valid_votes * percent; // Cálculo como número de punto flotante
            int votes = (int)Math.Round(votesDouble); // Redondeo y conversión a int
            return votes;
        }
        private static int seatsCount(DatosPartido p)
        {
            p.seats++;
            int votes = p.votes / (p.seats + 1);
            return votes;
        }

        public static List<DatosPartido> CalculateSeats(List<DatosPartido> parties)
        {
            const int SEATS = 37;
            for (int seat = 0; seat < SEATS; seat++)
            {
                // Encontrar el partido con más votos en la lista calculada
                int indexOfPartyWithMostVotes = parties.FindIndex(p => p.votes ==parties.Max(party => party.votes));

                // Realizar el cálculo para asignar asientos al partido
                int seatsCount = DatosPartido.seatsCount(parties[indexOfPartyWithMostVotes]);
                parties[indexOfPartyWithMostVotes].votes = seatsCount;
            }       
            return parties;
        }
     









    }


}


