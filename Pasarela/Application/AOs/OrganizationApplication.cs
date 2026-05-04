using Lantik.Pasarela.Application.DTOs;
using Lantik.Pasarela.Application.Interfaces;
using Lantik.Pasarela.Business.BOs;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System.Collections.Generic;

namespace Lantik.Pasarela.Application.AOs
{
    public class OrganizationApplication : IOrganizationApplication
    {

        private OrganizationDTO FillOrganizationDTO(Organization organization)
        {
            OrganizationDTO retorganization;
            retorganization = new OrganizationDTO
            {
                OU_ID = organization.OU_ID,
                OU_NAME = organization.OU_NAME
            };
            return retorganization;
        }

        public ResponseBaseDTO<IList<OrganizationDTO>> ObtenerPorModelo(string ModelName)
        {
            ResponseBaseDTO<IList<OrganizationDTO>> responseDTO = new ResponseBaseDTO<IList<OrganizationDTO>>();

            Logger.Debug("LLamamos al metodo ObtenerPorModelo con valores de ModelName: " + ModelName);
            ResponseBase<IList<Organization>> response = new OrganizationBusiness().ObtenerPorModelo(ModelName);

            IList<OrganizationDTO> retList = new List<OrganizationDTO>();
            OrganizationDTO retOrganization;

            foreach (Organization businessOrganization in response.Data)
            {
                retOrganization = FillOrganizationDTO(businessOrganization);
                retList.Add(retOrganization);
            }

            responseDTO.Data = retList;
            responseDTO.Query_Result.ParseResponse(response.Query_Result);
            return responseDTO;
        }
    }
}
