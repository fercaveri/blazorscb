using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SurrealCB.Data.Dto;
using SurrealCB.Data.Dto.Account;

namespace SurrealCB.CommonUI.Services
{
    public interface IUserProfileApi
    {
        Task<ApiResponseDto> Update(UserProfileDto userProfile);
        Task<ApiResponseDto> Get();
    }

    public class UserProfileApi : IUserProfileApi
    {
        private readonly HttpClient _httpClient;

        public UserProfileApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ApiResponseDto> Get()
        {
            return _httpClient.GetJsonAsync<ApiResponseDto>("api/user/get");
        }

        public Task<ApiResponseDto> Update(UserProfileDto userProfile)
        {
            return _httpClient.PostJsonAsync<ApiResponseDto>("api/user/update", userProfile);
        }
    }
}
