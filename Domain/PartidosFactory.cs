using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeyDeHont.Domain
{
    internal class PartidosFactory
    {
        public List<DatosPartido>  inicialiteParties(PreviousData p, List<DatosPartido> parties)
        {
           
            parties[0].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[1].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[2].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[3].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[4].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[5].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[6].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[7].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[8].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            parties[9].votes = DatosPartido.calculateVotesPartie(0.3524, p);
            return parties;
        }
        

    }
}
