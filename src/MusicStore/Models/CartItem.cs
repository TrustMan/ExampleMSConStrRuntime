using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; //add line

namespace MusicStore.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public string CartId { get; set; }
        public int AlbumId { get; set; }
        public int Count { get; set; }

        [DataType(DataType.DateTime)] //Delete line
		[Column(TypeName = "datetime")] //add line
        public DateTime DateCreated { get; set; } 

        public virtual Album Album { get; set; }
    }
}