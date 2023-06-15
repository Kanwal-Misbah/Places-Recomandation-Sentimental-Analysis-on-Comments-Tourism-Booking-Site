namespace Fin.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Destination")]
    public partial class Destination
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Destination()
        {
            PACKAGEs = new HashSet<PACKAGE>();
        }

        [Key]
        public int Destination_Id { get; set; }

        [Required]
        public string Des_Name { get; set; }

        [Required]
        public string Des_Address { get; set; }

        [Required]
        public string Des_description { get; set; }

      
        public string Des_Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase des_img { get; set; }

        public int Category_FID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACKAGE> PACKAGEs { get; set; }
    }
}
