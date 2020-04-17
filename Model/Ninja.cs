using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;

namespace Classes
{
    public class Ninja : IModificationHistory
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

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}
