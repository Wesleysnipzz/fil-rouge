using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EzChess.forme;

namespace WebAPI.Models.Entities
{
    public class Placement
    {
        [Key] // Clé primaire : définit la position unique sur l'échiquier
        public string Position { get; set; } = default!;
        
        public Guid? FormeId { get; set; }     // Clé étrangère reliant à l'entité Forme
        
        [ForeignKey("FormeId")] // Association entre Placement et Forme
        public Forme Forme { get; set; } = default!;
    }
}

