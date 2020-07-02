using Common.Lib.Core;
using System;

namespace Empleats.Lib.Models
{
    public class Empleat : Entity
    {
        public string Dni { get; set; }
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Job { get; set; }
        public string Email { get; set; }
        public Double Salary { get; set; }
        public DateTime Antiquity { get; set; }
    }
}
