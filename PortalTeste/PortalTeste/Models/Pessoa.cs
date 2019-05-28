using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTeste.Models
{
    public class Pessoa
    {
        [Key]
        public int id_pessoa { get; set; }
        [Required(ErrorMessage ="O campo nome é obrigátorio.")]
        public String nome_pessoa { get; set; }
        [Required(ErrorMessage = "O campo sexo é obrigátorio.")]
        public String sexo_pessoa { get; set; }
        [Required(ErrorMessage = "O campo tipo pessoa é obrigátorio.")]
        public int id_tipo_pessoa { get; set; }
        [Required(ErrorMessage = "O campo data de nascimento é obrigátorio.")]
        public DateTime data_nasc_pessoa { get; set; }
        //O campo data de cadastro e com a data de hoje, que e insirida no DAO.
        public DateTime data_cadastro_pessoa { get; set; }
    }
}