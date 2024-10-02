using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_esig.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Pais { get; set; }
        public string Usuario { get; set; }
        public string Telefone { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public int CargoId { get; set; }
    }
}