using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interno.Models
{
    public class Cliente
    {
        [Key]
        [Display(Name = "Id")]
        public int CLIENTE_ID { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        [MaxLength(100)]
        public string CLIENTE_NOME { get; set; }

        [Required]
        [Display(Name = "CNPJ")]
        [MinLength(18)]
        public string CLIENTE_CNPJ { get; set; }

        public Cliente() { }

        public Cliente(string cliente_nome, string cliente_cnpj)
        {
            this.CLIENTE_NOME = cliente_nome;
            this.CLIENTE_CNPJ = cliente_cnpj;
        }

        public Cliente(int cliente_id, string cliente_nome, string cliente_cnpj)
        {
            this.CLIENTE_ID = cliente_id;
            this.CLIENTE_NOME = cliente_nome;
            this.CLIENTE_CNPJ = cliente_cnpj;
        }
    }
}