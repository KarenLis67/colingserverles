using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared.Interfaz
{
    public interface IEstudios
    {
        public string TituloRecibido { get; set; }
        public string Anio { get; set; }
        public string Estado { get; set; }
    }
}
