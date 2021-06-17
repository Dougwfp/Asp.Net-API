using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interno.Models
{
    public class Cliente_c
    {
        [Key]
        [Display(Name = "Id")]
        public int CLIENTE_C_ID { get; set; }

        [Required]
        [Display(Name = "Id Cliente")]
        public int CLIENTE_ID { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        [MaxLength(100)]
        public string CLIENTE_NOME { get; set; }

        public Cliente_c() { }

        public Cliente_c(int cliente_id, string cliente_nome)
        {
            this.CLIENTE_ID = cliente_id;
            this.CLIENTE_NOME = cliente_nome;
        }

        public Cliente_c(int cliente_c_id, int cliente_id, string cliente_nome)
        {
            this.CLIENTE_C_ID = cliente_c_id;
            this.CLIENTE_ID = cliente_id;
            this.CLIENTE_NOME = cliente_nome;
        }
    }
}