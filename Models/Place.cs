namespace Fin.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Place
    {
        [Key]
        public int Place_id { get; set; }

        [Required]
        public string Place_Name { get; set; }

        public string Place_Description { get; set; }

       
        public string Place_Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase plc_img { get; set; }
        public int Package_FID { get; set; }

        public virtual PACKAGE PACKAGE { get; set; }
    }
}
