using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTeste.Models
{
    public class Cliente
    {
        [Key]
        public int id_cliente { get; set; }
        public int id_pessoa { get; set; }
        [Required(ErrorMessage="O campo email de login é obrigátorio.")]
        public String login_email_cli { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigátorio.")]
        public String senha_cli { get; set; }
        public bool fisico_cli { get; set; }
        public bool juridico_cli { get; set; }
        [Required(ErrorMessage = "O campo apelido é obrigátorio.")]
        public String apelido_cli { get; set; }
    }
}