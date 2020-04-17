using Classes.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Classes
{
    public class Ninja
    {
        public Ninja()
        {
            EquipmentOwned = new List<NinjaEquipment>();
        }
        public int Id { get; set; }
        // By adding FK that is of type int => not nullable
        // EF knows that clan is required, so we will have one to many relationship
        public int ClanId { get; set; }
        public string Name { get; set; }
        public bool ServedInOniwaban { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Clan Clan { get; set; }
        public List<NinjaEquipment> EquipmentOwned { get; set; }
    }

    public class Clan
    {
        public int Id { get; set; }
        public string ClanName { get; set; }
        public List<Ninja> Ninjas { get; set; }
    }

    public class NinjaEquipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        // If we don't want to define FK (at start I didn't have NinjaId)
        // we can use required attribute to say that Ninja is required
        // and we are getting 1 to many relationship (without it it was 0..1 to many)
        [Required]
        public Ninja Ninja { get; set; }
    }
}
