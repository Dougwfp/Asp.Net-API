using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InternoApi.Models
{
    [Table("TRANSPORT", Schema = "Geral")]
    public class Transport
    {
        [Key]
        public int TRANSP_ID { get; set; }
        public string TRANSP_NOME { get; set; }
        public string TRANSP_CNPJ { get; set; }
    }
}