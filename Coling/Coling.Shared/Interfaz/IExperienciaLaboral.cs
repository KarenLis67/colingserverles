using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared.Interfaz
{
    public interface IExperienciaLaboral
    {
        public string CargoTitulo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFinal { get; set; }
        public string Estado { get; set; }
    }

}
