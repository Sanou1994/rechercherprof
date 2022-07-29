namespace Personnel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("autres_experience")]
    public class autres_experience
    {
        [Key]
        public int id_autres_experience { get; set; }

        public DateTime? annee_debut { get; set; }
        [Column(TypeName = "date")]
        public DateTime? annee_fin { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_creation { get; set; }
        public long id_cv { get; set; }
    }
}