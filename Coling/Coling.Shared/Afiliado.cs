using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    [Table("afiliado")]
    public partial class Afiliado
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idpersona")]
        public int Idpersona { get; set; }

        [Column("fechaafiliacion", TypeName = "datetime")]
        public DateTime Fechaafiliacion { get; set; }

        [Column("codigoafiliado")]
        [StringLength(50)]
        public string Codigoafiliado { get; set; } = null!;

        [Column("nrotituloprovisional")]
        [StringLength(50)]
        public string Nrotituloprovisional { get; set; } = null!;

        [Column("estado")]
        [StringLength(20)]
        public string? Estado { get; set; }

        [ForeignKey("Idpersona")]
        [InverseProperty("Afiliados")]
        public virtual Persona IdpersonaNavigation { get; set; } = null!;

        [InverseProperty("IdafiliadoNavigation")]
        public virtual ICollection<ProfesionAfiliado> Profesionafiliados { get; set; } = new List<ProfesionAfiliado>();
    }
}
