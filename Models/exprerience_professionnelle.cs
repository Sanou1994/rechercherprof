namespace Personnel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("exprerience_professionnelle")]
    public class exprerience_professionnelle
    {
        [Key]
        public int id_exprerience_professionnelle { get; set; }
        [Column(TypeName = "date")]
        public DateTime? annee_debut { get; set; }
        [Column(TypeName = "date")]
        public DateTime? annee_fin { get; set; }
        [StringLength(100)]
        public string titre { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_creation { get; set; }
        public long id_cv { get; set; }
    }
}