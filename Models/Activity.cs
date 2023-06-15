namespace Fin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    [Table("Activity")]
    public partial class Activity
    {
        [Key]
        public int Activity_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Act_Name { get; set; }

        [Required]
        public string Act_Type { get; set; }

        public string Act_description { get; set; }

        
        public string Act_Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ACT_IMG { get; set; }
        public int Package_FID { get; set; }

        public virtual PACKAGE PACKAGE { get; set; }
    }
}
