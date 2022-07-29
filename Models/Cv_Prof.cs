namespace Personnel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cv_Prof")]
    public class Cv_Prof
    {
        [Key]
        public int id_cv_Prof { get; set; }

        [StringLength(5000)]
        public string apropos { get; set; }

        [StringLength(5000)]
        public string langues { get; set; }

        [StringLength(5000)]
        public string centre_interet { get; set; }
        public bool status { get; set; }
   
        [Column(TypeName = "date")]
        public DateTime? date_creation { get; set; }

        public long id_professeur { get; set; }
    }
}
