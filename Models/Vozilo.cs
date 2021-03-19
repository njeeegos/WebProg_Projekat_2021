using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebProg_Projekat_2021.Models
{
    [Table("Vozilo")]
    public class Vozilo
    {
        [Key]
        [Column("VoziloId")]
        public int VoziloId { get; set; }

        [StringLength(60)] 
        [Required(ErrorMessage="Neophodno je uneti marku vozila!")]
        [Column("Marka")]
        public string Marka { get; set; }

        [StringLength(60)]
        [Required(ErrorMessage="Neophodno je uneti model vozila!")]
        [Column("Model")]
        public string Model { get; set; }

        [Range(1970, 2021)]
        [Required(ErrorMessage="Neophodno je uneti godiste vozila!")]
        [Column("Godiste")]
        public int Godiste { get; set; }

        [Required(ErrorMessage="Neophodno je uneti tip karoserije vozila!")]
        [Column("Tip")]
        public string Tip { get; set; }

        [StringLength(60)]
        [Required(ErrorMessage="Neophodno je uneti boju vozila!")]
        [Column("Boja")]
        public string Boja { get; set; }
    }
}