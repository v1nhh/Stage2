using CommunicationModule.ApplicationCore.Entities;

namespace CloudAPI.ApplicationCore.DTO.Integration
{
    public abstract class GenericResponse
    {
        /// <summary>
        /// The Request that caused the response. Used for e.g. setting the RequestStatus to 'Closed'.
        /// </summary>
        public Request RequestOrigin;
        public string TenantID;
    }
}
