using Classes;
using Classes.Enums;
using Model.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class NinjaEquipment : IModificationHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        // If we don't want to define FK (at start I didn't have NinjaId)
        // we can use required attribute to say that Ninja is required
        // and we are getting 1 to many relationship (without it it was 0..1 to many)
        [Required]
        public Ninja Ninja { get; set; }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}
