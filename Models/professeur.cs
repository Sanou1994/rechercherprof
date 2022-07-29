namespace Personnel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("professeur")]
    public  class Professeur
    {
        [Key]
        public int id_professeur { get; set; }

        public string prenom { get; set; }

        public string nom { get; set; }

        public string matiere_dispense_1 { get; set; }

        public string matiere_dispense_2 { get; set; }

        public string matiere_dispense_3 { get; set; }

        [StringLength(100)]
        public string adresse { get; set; }

        public string pwd { get; set; }
        public bool status { get; set; }
        public long telephone { get; set; }

        public string photo { get; set; }
        public string email { get; set; }
        [Column(TypeName = "date")]
        public DateTime? date_creation { get; set; }
    }
}
