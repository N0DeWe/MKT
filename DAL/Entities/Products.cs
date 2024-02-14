namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Information_about_sales = new HashSet<Information_about_sales>();
            Information_about_suppliers = new HashSet<Information_about_suppliers>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        [Required]
        [StringLength(50)]
        public string product_name { get; set; }

        [Required]
        [StringLength(777)]
        public string technical_specifications { get; set; }

        public int count_of_products { get; set; }

        [Column(TypeName = "money")]
        public decimal product_price { get; set; }

        public double? discount { get; set; }

        public int category_FK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Information_about_sales> Information_about_sales { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Information_about_suppliers> Information_about_suppliers { get; set; }

        public virtual Сategory Сategory { get; set; }
    }
}
