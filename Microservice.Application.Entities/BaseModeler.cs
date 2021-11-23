using Shareds.DesignPatterns.Model;

namespace Microservice.Application.Entities
{
    public enum Personal
    {
        Publish = 1,
        Private = 2
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseModeler : PresentModel
    {
        public string UserName { get; set; }
        public Personal Personal { get; set; }
    }
}
