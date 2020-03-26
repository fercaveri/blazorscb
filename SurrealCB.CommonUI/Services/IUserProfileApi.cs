using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SurrealCB.Data.Dto;
using SurrealCB.Data.Dto.Account;

namespace SurrealCB.UI.Services
{
    public interface IUserProfileApi
    {
        Task<ApiResponseDto> Upsert(UserProfileDto userProfile);
        Task<ApiResponseDto> Get();
    }
}
