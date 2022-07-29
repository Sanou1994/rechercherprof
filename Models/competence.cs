namespace Personnel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("competence")]
    public class competence
    {
        [Key]
        public int id_competence { get; set; }

        public string annee_debut { get; set; }
        public string annee_fin { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_creation { get; set; }
        public long id_cv { get; set; }
    }
}