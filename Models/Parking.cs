using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProg_Projekat_2021.Models
{
    [Table("Parking")]
    public class Parking
    {
        [Key]
        [Column("ParkingId")]
        public int ParkingId { get; set; }

        [StringLength(60)]
        [Column("Ime")]
        public string Ime { get; set; }

        [Range(1, 100)]
        [Column("X")]
        public int X { get; set; }

        [Range(1, 100)]
        [Column("Y")]
        public int Y { get; set; }

        [Column("ListaMesta")]
        public virtual List<Mesto> Mesta { get; set; }

        public Parking()
        {
            Mesta=new List<Mesto>();
        }
    }
}