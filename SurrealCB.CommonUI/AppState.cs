using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SurrealCB.CommonUI.Services;
using SurrealCB.Data.Dto;
using SurrealCB.Data.Dto.Account;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace SurrealCB.CommonUI
{
    public class AppState
    {
        public event Action OnChange;
        private readonly IUserProfileApi _userProfileApi;

        public UserProfileDto UserProfile { get; set; }

        public AppState(IUserProfileApi userProfileApi)
        {
            _userProfileApi = userProfileApi;
        }

        public bool IsNavOpen
        {
            get
            {
                if (UserProfile == null)
                {
                    return true;
                }
                return UserProfile.IsNavOpen;
            }
            set
            {
                UserProfile.IsNavOpen = value;
            }
        }
        public bool IsNavMinified { get; set; }

        public async Task UpdateUserProfile()
        {
            await _userProfileApi.Update(UserProfile);
        }

        public async Task<UserProfileDto> GetUserProfile()
        {
            if (UserProfile != null && UserProfile.UserId != 0)
            {
                return UserProfile;
            }

            ApiResponseDto apiResponse = await _userProfileApi.Get();

            if (apiResponse.StatusCode == Status200OK)
            {
                return JsonConvert.DeserializeObject<UserProfileDto>(apiResponse.Result.ToString());
            }
            return new UserProfileDto();
        }

        public async Task UpdateUserProfileGold(int count)
        {
            UserProfile.Gold = count;
            await UpdateUserProfile();
            NotifyStateChanged();
        }

        public async Task<int> GetUserProfileGold()
        {
            if (UserProfile == null)
            {
                UserProfile = await GetUserProfile();
                return UserProfile.Gold;
            }

            return UserProfile.Gold;
        }

        public async Task UpdateUserProfileExp(int count)
        {
            UserProfile.Exp = count;
            await UpdateUserProfile();
            NotifyStateChanged();
        }

        public async Task<int> GetUserProfileExp()
        {
            if (UserProfile == null)
            {
                UserProfile = await GetUserProfile();
                return UserProfile.Exp;
            }

            return UserProfile.Exp;
        }

        public async Task SaveLastVisitedUri(string uri)
        {
            if (UserProfile ==  null)
            {
                UserProfile = await GetUserProfile();
            }
            if (UserProfile != null)
            {
                UserProfile.LastPageVisited = uri;
                await UpdateUserProfile();
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}