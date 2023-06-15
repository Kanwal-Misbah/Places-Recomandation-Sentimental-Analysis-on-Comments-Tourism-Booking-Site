namespace Fin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WishList")]
    public partial class WishList
    {
        [Key]
        public int Wishlist_id { get; set; }

        public DateTime Wishlist_Date { get; set; }

        public int Package_FID { get; set; }

        public int User_FID { get; set; }

        public virtual PACKAGE PACKAGE { get; set; }

        public virtual User User { get; set; }
    }
}
