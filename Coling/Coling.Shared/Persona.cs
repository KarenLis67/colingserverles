using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    [Table("persona")]
    public partial class Persona
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("ci")]
        [StringLength(20)]
        public string Ci { get; set; } = null!;

        [Column("nombre")]
        [StringLength(70)]
        public string Nombre { get; set; } = null!;

        [Column("apellidos")]
        [StringLength(100)]
        public string Apellidos { get; set; } = null!;

        [Column("fechaNacimiento", TypeName = "datetime")]
        public DateTime? FechaNacimiento { get; set; }

        [Column("foto")]
        [MaxLength(1)]
        public byte[]? Foto { get; set; }

        [Column("estado")]
        [StringLength(20)]
        public string? Estado { get; set; }

        [InverseProperty("IdpersonaNavigation")]
        public virtual ICollection<Afiliado> Afiliados { get; set; } = new List<Afiliado>();

        [InverseProperty("IdpersonaNavigation")]
        public virtual ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();

        [InverseProperty("IdpersonaNavigation")]
        public virtual ICollection<PersonaTipoSocial> PersonatipoSociales { get; set; } = new List<PersonaTipoSocial>();

        [InverseProperty("IdpersonaNavigation")]
        public virtual ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
    }
}
