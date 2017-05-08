using System.Collections.Generic;
using AutoMapper;
using YellowJacket.Dashboard.Entities.Agent;
using YellowJacket.Models;

namespace YellowJacket.Dashboard.Mapping
{
    public class MappingProfile: Profile
    {
        #region Constructors

        public MappingProfile()
        {
            CreateMap<AgentModel, AgentEntity>();
            CreateMap<AgentEntity, AgentModel>();

            CreateMap<List<AgentModel>, List<AgentEntity>>();
            CreateMap<List<AgentEntity>, List<AgentModel>>();
        }

        #endregion
    }
}
