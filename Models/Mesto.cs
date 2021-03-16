using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebProg_Projekat_2021.Models
{
    [Table("Mesto")]
    public class Mesto
    {
        [Key]
        [Column("MestoId")]
        public int MestoId { get; set; }
 
        [Column("Broj")]
        public int Broj { get; set; }

        [Column("Vozilo")]
        public virtual Vozilo Vozilo { get; set; }

        [Column("Parking")]
        [JsonIgnore]
        public virtual Parking Parking { get; set; }
    }   
}