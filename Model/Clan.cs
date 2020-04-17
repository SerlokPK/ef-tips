using Classes;
using Model.Interfaces;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Clan : IModificationHistory
    {
        public int Id { get; set; }
        public string ClanName { get; set; }
        public List<Ninja> Ninjas { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}
