using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2E_Testing.Models
{
    public class TestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }
    }
}
