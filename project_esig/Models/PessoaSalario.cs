using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace project_esig.Models
{
    [Table("pessoa_salario", Schema = "C##KAIO")]
    public class PessoaSalario
    {
        [Key]
        public int PessoaID { get; set; }
        public string Nome { get; set; }
        public decimal Salario { get; set; }
    }
}