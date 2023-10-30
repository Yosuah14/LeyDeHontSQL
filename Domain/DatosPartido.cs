using LeyDeHont.Persistence.Manages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static List<DatosPartido> seatsTotalCount(List<DatosPartido>parties, List<DatosPartido> parties2)

        {
            int aux = 0;
           const int SEATS= 37;
           for (int i = 0; i <= SEATS; i++) {

                //Busca el objeto con mayores votos
                int indexOfPartyWithMostVotes = parties.FindIndex(c => c.votes == parties.Max(car => car.votes));
                aux = DatosPartido.seatsCount(parties[indexOfPartyWithMostVotes]);
                parties[indexOfPartyWithMostVotes].votes = aux;


            }
           //Asiciamos los votos inicales
            for (int i = 0; i < parties.Count; i++)
            {
                parties[i].votes = parties2[i].votes;
            }
            return parties;
        }




    }


    }


