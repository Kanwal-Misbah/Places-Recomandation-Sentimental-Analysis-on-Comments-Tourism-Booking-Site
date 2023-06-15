namespace Fin.Models
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Business_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Business_User()
        {
            PACKAGEs = new HashSet<PACKAGE>();
        }

        [Key]
        public int Bus_User_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Bus_Name { get; set; }

        public string Bus_Address { get; set; }

        [StringLength(50)]
        public string Bank_Name { get; set; }

        [StringLength(50)]
        public string Bank_Account_number { get; set; }

        [StringLength(50)]
        public string Bus_Reg { get; set; }

        [Required]
        [StringLength(50)]
        public string Bus_Email { get; set; }

        [Required]
        [StringLength(10)]
        public string Bus_Password { get; set; }

        public string Bus_logo { get; set; }
        [NotMapped]
        public HttpPostedFileBase bus_log { get; set; }
        public string Bus_Location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACKAGE> PACKAGEs { get; set; }
    }
}
