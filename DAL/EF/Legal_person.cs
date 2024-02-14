namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Legal_person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int legal_person_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Legal_person_TIN { get; set; }

        [Required]
        [StringLength(50)]
        public string Legal_person_CRS { get; set; }

        [Required]
        [StringLength(50)]
        public string Legal_person_MSRN { get; set; }
    }
}
