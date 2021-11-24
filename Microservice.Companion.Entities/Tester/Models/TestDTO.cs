using System;

namespace Microservice.Companion.Entities.Tester.Models
{
    [Serializable]
    public class TestDto : BaseModeler
    {
        public TestDto()
        {
          
        }

        public string Name { get; set; }
        public string Age { get; set; }
    }
}
