﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeyDeHont.Domain
{
    class PreviousData
    {
     

        public int POPULATION { get; set; }
        public int ABSTENTIONS_VOTES { get; set; }
        public double NULL_VOTES { get; set; }
        
        public double valid_votes
        {
            get; set;

        }
 

        // Constructor vacío
        public PreviousData()
        {
            // No se requiere inicialización aquí
        }

        // Constructor con todos los atributos
        public PreviousData(int population, int abstentionsVotes, int nullVotes, double valid_votes)
        {
            POPULATION = population;
            ABSTENTIONS_VOTES = abstentionsVotes;
            NULL_VOTES = nullVotes;
            this.valid_votes = valid_votes;
        }
        public static double CalculateNullVotes(int POPULATION, int ABSTENTIONS_VOTES)
        {
            double nullvotes = (POPULATION - ABSTENTIONS_VOTES) / 20;
            return nullvotes;
        }
        public static int calculatevalidvotes(int POPULATION, int ABSTENTIONS_VOTES,int NULLVOTES)
        {
            int validvotes = (POPULATION - ABSTENTIONS_VOTES) - NULLVOTES;
            return validvotes;
        }


    }


}

