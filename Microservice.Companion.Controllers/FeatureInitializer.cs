using Microservice.Companion.Controllers.Commands.Features;
using Microservice.Companion.Entities.Features.Maps;
using Microservice.Companion.Entities.Features.Models;
using Microservice.DataAccress.Entites.Features.Models;
using Microservice.DataAccress.Features;
using Microservice.DataAccress.Features.Repositories;
using Shareds.DesignPatterns.CQRS.Commands;
using Shareds.DesignPatterns.CQRS.Repositories;
using Shareds.DesignPatterns.Repository.DatabaseContext;
using Shareds.DesignPatterns.Repository.Extensions;
using Shareds.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Companion.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FeatureInitializer : ObjectInitializer<CommandBus>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        protected override async void SeedAsync(CommandBus command)
        {
            var types = await PrivacyTypeSeed().ConfigureAwait(false);
            types.ForEach(o => command.Send(new PrivacyTypeCommand(o)).ConfigureAwait(false));
        }

        private static async Task<List<PrivacyTypeDTO>> PrivacyTypeSeed()
        {
            var types = new List<PrivacyTypeDTO>();

            var repository = new PrivacyTypeRepositoryAsync(new Context<FeatureStoreContext>(), new Logger());

            var enums = Enum.GetValues(typeof(PrivacyTypes)).Cast<PrivacyTypes>();

            foreach (var @enum in enums)
            {
                var query = await repository.GetSingleAsync(o => o.Code == (int)@enum).ConfigureAwait(false)
                    ?? new PrivacyType { ID = Guid.Empty, Code = (int)@enum, Name = @enum.ToString() };

                switch (query)
                {
                    case PrivacyType type when type.ID == Guid.Empty:
                        types.Add(new PrivacyTypeMapper().ToDataTransferObject(query));
                        break;
                    default:
                        break;
                }
            }
            return types;
        }
    }
}
