using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeyDeHont.Domain
{
    internal class PartidosFactory
    {
        public static List<DatosPartido>  inicialiteParties(PreviousData p, List<DatosPartido> parties)
        {
            double validVotes = p.valid_votes * 0.03;
            int minvotes = (int)Math.Round(validVotes);
            parties[0].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[1].votes = DatosPartido.calculateVotesPartie(0.2475, p);
            parties[2].votes = DatosPartido.calculateVotesPartie(0.1575, p);
            parties[3].votes = DatosPartido.calculateVotesPartie(0.1425, p);
            parties[4].votes = DatosPartido.calculateVotesPartie(0.0375, p);
            parties[5].votes = DatosPartido.calculateVotesPartie(0.0325, p);
            parties[6].votes = DatosPartido.calculateVotesPartie(0.015, p);
            parties[7].votes = DatosPartido.calculateVotesPartie(0.005, p);
            parties[8].votes = DatosPartido.calculateVotesPartie(0.0025, p);
            parties[9].votes = DatosPartido.calculateVotesPartie(0.0025, p);
            List < DatosPartido > updateParties=new List<DatosPartido>(); ;
            foreach (var part in parties)
            {
                if (part.votes >= minvotes) 
                {
                    
                 updateParties.Add(part);
                }
            }
           return updateParties;
        }
        

    }
}
