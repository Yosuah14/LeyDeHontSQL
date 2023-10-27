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


    }
}

