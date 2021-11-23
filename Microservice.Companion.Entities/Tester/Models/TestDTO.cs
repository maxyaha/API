using System;

namespace Microservice.Companion.Entities.Tester.Models
{
    [Serializable]
    public class TestDTO : BaseModeler
    {
        public TestDTO()
        {
          
        }

        public string Name { get; set; }
        public string Age { get; set; }
    }
}
