using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EzChess.forme;

namespace WebAPI.Models.Entities
{
    public class Placement
    {
        [Key]
        // Conserve la correspondance avec la colonne de la base de données
        [Column("position")]
        public string Position { get; set; } = default!;
        
        public Guid? FormeId { get; set; }
        
        [ForeignKey("FormeId")]
        public Forme Forme { get; set; } = default!;
    }
}
