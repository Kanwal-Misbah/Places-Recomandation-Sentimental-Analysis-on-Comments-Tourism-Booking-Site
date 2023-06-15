namespace Fin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        [Key]
        public int Booking_id { get; set; }

        public DateTime Booking_date { get; set; }

        public int Package_FID { get; set; }

        public int User_FID { get; set; }

        public int No_of_Members { get; set; }

        [Required]
        public string Departure { get; set; }

        public int Total_Payment { get; set; }

        [Required]
        [StringLength(50)]
        public string Payment_status { get; set; }

        public virtual PACKAGE PACKAGE { get; set; }

        public virtual User User { get; set; }
    }
}
