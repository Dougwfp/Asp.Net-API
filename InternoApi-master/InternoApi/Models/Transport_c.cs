using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InternoApi.Models
{
    [Table("TRANSPORT_C", Schema = "Geral")]
    public class Transport_c
    {
        [Key]
        public int TRANSP_C_ID { get; set; }
        public int TRANSP_ID { get; set; }
        public string TRANSP_NOME { get; set; }

        public Transport_c() { }

        public Transport_c(int id, string nome)
        {
            this.TRANSP_ID = id;
            this.TRANSP_NOME = nome;
        }
    }
}