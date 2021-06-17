using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Interno.Models
{
    public class Transport_c
    {
        [Key]
        [Display(Name = "Id")]
        public int TRANSP_C_ID { get; set; }

        [Required]
        [Display(Name = "Id Transportadora")]
        public int TRANSP_ID { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        [MaxLength(100)]
        public string TRANSP_NOME { get; set; }

        public Transport_c() { }

        public Transport_c(int transp_id, string transp_nome)
        {
            this.TRANSP_ID = transp_id;
            this.TRANSP_NOME = transp_nome;
        }

        public Transport_c(int transp_c_id, int transp_id, string transp_nome)
        {
            this.TRANSP_C_ID = transp_c_id;
            this.TRANSP_ID = transp_id;
            this.TRANSP_NOME = transp_nome;
        }
    }
}