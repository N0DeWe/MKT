namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Information_about_suppliers
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Information_about_supplie_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int supplies_count { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "money")]
        public decimal supplies_price { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime supplies_date { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int supplier_FK { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_FK { get; set; }

        public virtual Products Products { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
