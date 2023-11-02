using LeyDeHont.Persistence.Manages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeyDeHont.Domain
{
    /*
     * Clase que almacena los datos de un partido politico
     */
    class DatosPartido
    {
        public string Acronimo { get; set; }
        public string Nombre { get; set; }
        public string Presidente { get; set; }
        public int votes {  get; set; }
        public int  seats { get; set; }
        public PartiesManage pm { get; set; }
        public int votesaux { get; set; }
        //Cosntructores
        public DatosPartido()
        {
            pm = new PartiesManage();
        }        public DatosPartido(string acronimo, string nombre, string presidente)
        {
            Acronimo = acronimo;
            Nombre = nombre;
            Presidente = presidente;
            pm = new PartiesManage();
        }

        public DatosPartido(string acronimo, string nombre, string presidente,int votes, int votesaux)
        {
            Acronimo = acronimo;
            Nombre = nombre;
            Presidente = presidente;
            pm = new PartiesManage();
            this.votes = votes;
            this.votesaux = votesaux;
        }
        public DatosPartido(string acronimo, string nombre, string presidente, int votes, int votesaux,int seats)
        {
            Acronimo = acronimo;
            Nombre = nombre;
            Presidente = presidente;
            pm = new PartiesManage();
            this.votes = votes;
            this.seats = seats;
            this.votesaux = votesaux;
        }
        public List<DatosPartido> getListParties()
        {
            return pm.listParties;
        }
        //añadir partidos
        public void addParties(string name, string nPartido, string presidente)
        {
            pm.addPartdio(name, nPartido, presidente);
        }
        //Borrar partidos de la lista
        public void removeParties(DatosPartido p)
        {
            pm.listParties.Remove(p);
        }
        //Calcular el porcentaje 
        public static int calculateVotesPartie(double percent,PreviousData p)
        {
            double votesDouble = p.valid_votes * percent; // Cálculo como número de punto flotante
            int votes = (int)Math.Round(votesDouble); // Redondeo y conversión a int
            return votes;
        }
        //Calcular los escaños
        private static int seatsCount(DatosPartido p)
        {
            //Sumamos un escaño
            p.seats++;
            int votes = p.votes / (p.seats + 1);
            return votes;
        }
        //Metodo para añadir los escaños a cada partido
        public static List<DatosPartido> CalculateSeats(List<DatosPartido> parties)
        {
            //Pongo una constante con 37 escaños
            const int SEATS = 37;
            for (int seat = 0; seat < SEATS; seat++)
            {
                // Encontrar el partido con más votos en la lista calculada
                int indexOfPartyWithMostVotes = parties.FindIndex(p => p.votesaux ==parties.Max(party => party.votesaux));
                // Realizar el cálculo para asignar asientos al partido
                int seatsCount = DatosPartido.seatsCount(parties[indexOfPartyWithMostVotes]);
                parties[indexOfPartyWithMostVotes].votesaux = seatsCount;
            }       
            return parties;
        }
    }
}


