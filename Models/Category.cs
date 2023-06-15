namespace Fin.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            PACKAGEs = new HashSet<PACKAGE>();
        }

        [Key]
        public int Category_ID { get; set; }

        [Required]
        public string Cat_Name { get; set; }

        public string Cat_description { get; set; }

        
        public string Cat_Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase cat_img { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACKAGE> PACKAGEs { get; set; }
    }
}
