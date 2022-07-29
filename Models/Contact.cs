namespace Personnel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("contact")]
    public class Contact
    {
        [Key]
        public int id_contact { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Titre { get; set; }
        [Column(TypeName = "date")]
        public DateTime? date_creation { get; set; }
        public long? id_professeur { get; set; }
    }
}