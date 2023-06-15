namespace Fin.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PACKAGE")]
    public partial class PACKAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PACKAGE()
        {
            Activities = new HashSet<Activity>();
            Bookings = new HashSet<Booking>();
            COMMENTs = new HashSet<COMMENT>();
            Places = new HashSet<Place>();
            WishLists = new HashSet<WishList>();
        }

        [Key]
        public int Package_id { get; set; }

        [Required]
        public string Package_title { get; set; }

        [Required]
        public string Package_duration { get; set; }

        public int No_of_Members { get; set; }

        [Required]
        public string Package_description { get; set; }

        public int FID_Business_User { get; set; }

        public int Category_FID { get; set; }

       
        public string Package_Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase pack_img { get; set; }

        public int Package_Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Internet { get; set; }

        [Required]
        [StringLength(50)]
        public string Room_Service { get; set; }

        [Required]
        [StringLength(50)]
        public string Parking_Space { get; set; }

        [Required]
        [StringLength(50)]
        public string Meal_Service { get; set; }

        [Required]
        [StringLength(50)]
        public string Package_Style { get; set; }

        [Required]
        [StringLength(50)]
        public string Travel_Guider { get; set; }

        [Required]
        [StringLength(50)]
        public string Transport { get; set; }

        public DateTime Start_date { get; set; }

        public DateTime End_date { get; set; }

        [Required]
        [StringLength(50)]
        public string Discount { get; set; }

        public int Destination_FID { get; set; }

        public string Departure_Address { get; set; }

        public int? Sentimental_score { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual Business_User Business_User { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTs { get; set; }

        public virtual Destination Destination { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Place> Places { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
