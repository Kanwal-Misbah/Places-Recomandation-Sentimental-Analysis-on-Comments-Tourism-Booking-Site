namespace Fin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMMENT")]
    public partial class COMMENT
    {
        public int CommentId { get; set; }

        public DateTime ComentedOn { get; set; }

        [Required]
        public string ComentDescription { get; set; }

        public int Rating { get; set; }

        public int Package_FID { get; set; }

        public int User_FID { get; set; }

        public virtual PACKAGE PACKAGE { get; set; }

        public virtual User User { get; set; }
    }
}
