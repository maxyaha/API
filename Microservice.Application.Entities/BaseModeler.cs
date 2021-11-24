using Microservice.Companion.Entities.Features.Models;
using Newtonsoft.Json;
using Shareds.DesignPatterns.Model;
using System.ComponentModel.DataAnnotations;

namespace Microservice.Application.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseModeler : PresentModel
    {

        public virtual string UserName { get; set; }

        [Required]
        public virtual PrivacyTypes PrivacyCode { get; set; }
    }

    public abstract class BaseModelerWrapper : PresentModelWrapper
    {

    }
}
