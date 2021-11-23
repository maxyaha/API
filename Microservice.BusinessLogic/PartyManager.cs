//using Microservice.Companion.Entities.Parties.Maps;
//using Microservice.Companion.Entities.Parties.Models;
//using Microservice.DataAccress.Entites.Parties.Models;
//using Microservice.DataAccress.Parties.Repositories;
//using Shareds.DesignPatterns.Repository.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Microservice.BusinessLogic
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public interface IPartyManager
//    {

//        Task<IEnumerable<PartyDTO>> Party();
//        Task<IEnumerable<PartyRoleDTO>> PartyRole(PartyTypes code);
//        Task<IEnumerable<PartyRoleRelationshipDTO>> PartyRoleRelationship(Guid? id);
//        Task<PartyRoleDTO> PartyRole(Guid? id);
//        Task<PartyTypeDTO> PartyType(PartyTypes code);

//    }

//    public class PartyManager : IPartyManager
//    {
//        private readonly IPartyRepositoryAsync repoParty;
//        private readonly IPartyRoleRepositoryAsync repoPartyRole;
//        private readonly IPartyRoleRelationshipRepositoryAsync repoPartyRoleRelationship;
//        private readonly IPartyTypeRepositoryAsync repoPartyType;



//        public PartyManager(IPartyRepositoryAsync repoParty
//            , IPartyRoleRepositoryAsync repoPartyRole
//            , IPartyRoleRelationshipRepositoryAsync repoPartyRoleRelationship
//            , IPartyTypeRepositoryAsync repoPartyType)
//        {
//            this.repoParty = repoParty;
//            this.repoPartyRole = repoPartyRole;
//            this.repoPartyRoleRelationship = repoPartyRoleRelationship;
//            this.repoPartyType = repoPartyType;
//        }

//        public async Task<IEnumerable<PartyDTO>> Party()
//        {
//            var queries = await this.repoParty.GetAllAsync().ConfigureAwait(false);

//            return queries.Select(new PartyMapper().ToDataTransferObject);
//        }

//        public async Task<IEnumerable<PartyRoleDTO>> PartyRole(PartyTypes code)
//        {
//            var queries = await this.repoPartyRole.FindByAsync(o => o.PartyType.Code == (int)code)
//                .ConfigureAwait(false);

//            foreach (var query in queries)
//            {
//                query.PartyType = await this.repoPartyType.GetSingleAsync(o => o.ID == query.PartyTypeID)
//                    .ConfigureAwait(false);
//                query.ParentPartyRoleRelationships = await this.repoPartyRoleRelationship.FindByAsync(o => o.ParentID == query.ID)
//                    .ConfigureAwait(false) as ICollection<PartyRoleRelationship>;
//                query.ChildsPartyRoleRelationships = await this.repoPartyRoleRelationship.FindByAsync(o => o.ChildsID == query.ID)
//                    .ConfigureAwait(false) as ICollection<PartyRoleRelationship>;
//            }
//            return queries.Select(new PartyRoleMapper().ToDataTransferObject);
//        }

//        public async Task<PartyRoleDTO> PartyRole(Guid? id)
//        {
//            if (id is null)
//                return null;

//            var query = await this.repoPartyRole.GetSingleAsync(o => o.ID == id)
//                .ConfigureAwait(false);

//            query.PartyType = await this.repoPartyType.GetSingleAsync(o => o.ID == query.PartyTypeID)
//                .ConfigureAwait(false);
//            query.ParentPartyRoleRelationships = await this.repoPartyRoleRelationship.FindByAsync(o => o.ParentID == query.ID)
//                .ConfigureAwait(false) as ICollection<PartyRoleRelationship>;
//            query.ChildsPartyRoleRelationships = await this.repoPartyRoleRelationship.FindByAsync(o => o.ChildsID == query.ID)
//                .ConfigureAwait(false) as ICollection<PartyRoleRelationship>;

//            return new PartyRoleMapper().ToDataTransferObject(query);
//        }

//        public async Task<IEnumerable<PartyRoleRelationshipDTO>> PartyRoleRelationship(Guid? id)
//        {
//            var queries = await this.repoPartyRoleRelationship.FindByAsync(o => o.ChildsPartyRole.ID == (id ?? Guid.Empty))
//                .ConfigureAwait(false) as ICollection<PartyRoleRelationship>;

//            return queries.Select(new PartyRoleRelationshipMapper().ToDataTransferObject);
//        }

//        public async Task<PartyTypeDTO> PartyType(PartyTypes code)
//        {
//            var query = await this.repoPartyType.GetSingleAsync(o => o.Code == (int)code)
//                .ConfigureAwait(false);

//            return new PartyTypeMapper().ToDataTransferObject(query);
//        }
//    }
//}
