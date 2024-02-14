namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Physical_person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int physical_person_id { get; set; }

        [Required]
        [StringLength(100)]
        public string physical_person_name { get; set; }

        [Required]
        [StringLength(50)]
        public string physical_person_pasport_number { get; set; }

        [Required]
        [StringLength(50)]
        public string physical_person_TIN { get; set; }
    }
}
