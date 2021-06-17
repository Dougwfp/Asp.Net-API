using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interno.Models
{
    public class Transport
    {
        [Key]
        [Display(Name = "Id")]
        public int TRANSP_ID { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        [MaxLength(100)]
        public string TRANSP_NOME { get; set; }

        [Required]
        [Display(Name = "CNPJ")]
        [MinLength(18)]
        public string TRANSP_CNPJ { get; set; }

        public Transport() { }

        public Transport(string transp_nome, string transp_cnpj)
        {
            this.TRANSP_NOME = transp_nome;
            this.TRANSP_CNPJ = transp_cnpj;
        }

        public Transport(int transp_id, string transp_nome, string transp_cnpj)
        {
            this.TRANSP_ID = transp_id;
            this.TRANSP_NOME = transp_nome;
            this.TRANSP_CNPJ = transp_cnpj;
        }
    }
}