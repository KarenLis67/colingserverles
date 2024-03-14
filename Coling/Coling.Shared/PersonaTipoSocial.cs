using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    [Table("personatipoSocial")]
    public partial class PersonaTipoSocial
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("idtiposocial")]
        public int Idtiposocial { get; set; }

        [Column("idpersona")]
        public int Idpersona { get; set; }

        [Column("estado")]
        [StringLength(20)]
        public string? Estado { get; set; }

        [ForeignKey("Idpersona")]
        [InverseProperty("PersonatipoSociales")]
        public virtual Persona IdpersonaNavigation { get; set; } = null!;

        [ForeignKey("Idtiposocial")]
        [InverseProperty("PersonatipoSociales")]
        public virtual TipoSocial IdtiposocialNavigation { get; set; } = null!;
    }
}
