using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("FavoriteBook")]
    public class FavoriteBook
    {
        [Key]
        public int id { get; set; }
        public string book_title { get; set; }
        public string book_description { get; set; }
        public string id_google { get; set; }
    }
}
