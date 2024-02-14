namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Information_about_sales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Information_about_sales_id { get; set; }

        public int sales_count { get; set; }

        [Column(TypeName = "money")]
        public decimal sales_price { get; set; }

        public DateTime sales_date { get; set; }

        public int product_FK { get; set; }

        public virtual Products Products { get; set; }
    }
}
