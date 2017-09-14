using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlossaryDBContext.DBClasses
{
    public class Glossary
    {
        public Glossary()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TermId { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}
