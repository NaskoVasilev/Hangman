using Hangman.Common;
using Hangman.Shared;
using Hangman.Shared.InputModels.User;
using Hangman.Shared.InputModels.Word;
using Hangman.Shared.InputModels.WordCategory;
using Hangman.Shared.ResponseModels;
using Hangman.Shared.ResponseModels.WordCategory;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hangman.Client.Infrastructure
{
    public class ApiClient
    {
        private readonly HttpClient httpClient;

        private readonly JsInterop jsInterop;

        public ApiClient(HttpClient httpClient, JsInterop jsInterop)
        {
            this.httpClient = httpClient;
            this.jsInterop = jsInterop;
        }

        //Post requests
        public Task<ApiResponse<UserResponseModel>> Register(UserRegisterInputModel data) =>
            this.PostJson<UserResponseModel>("user/register", data);

        public Task<ApiResponse<AuthenticatedUserResponseModel>> Login(UserLoginInputModel data) =>
           this.PostJson<AuthenticatedUserResponseModel>("user/login", data);

        public Task<ApiResponse<WordCategoryResponseModel>> CreateCategory(WordCategoryCreateInputModel data) =>
         this.PostJson<WordCategoryResponseModel>("wordCategory/create", data);

        public Task<ApiResponse<bool>> CreateWord(WordCreateInputModel data) =>
        this.PostJson<bool>("word/create", data);

        public Task<ApiResponse<bool>> EditWord(WordEditInputModel data) =>
        this.PostJson<bool>("word/edit", data);

        //Get requests
        public Task<ApiResponse<string>> AboutMe() =>
           this.GetJson<string>("user/me");

        public Task<ApiResponse<string>> GetRandomWord(string level, string categoryId) =>
          this.GetJson<string>($"word/getRandomWord?level={level}&categoryId={categoryId}");

        public Task<ApiResponse<IEnumerable<WordCategoryResponseModel>>> GetAllCategories() =>
         this.GetJson<IEnumerable<WordCategoryResponseModel>>($"wordCategory/all");

        public Task<ApiResponse<string>> GetCategoryNameById(string id) =>
        this.GetJson<string>($"wordCategory/getName?id=" + id);

        public Task<ApiResponse<bool>> IsAdmin() =>
        this.GetJson<bool>($"user/isAdmin");

        public Task<ApiResponse<WordEditResponseModel>> GetWordForEditing(int id) =>
        this.GetJson<WordEditResponseModel>($"word/GetEditModel/" + id);

        public Task<ApiResponse<IEnumerable<WordResponseModel>>> GetAllWords() =>
        this.GetJson<IEnumerable<WordResponseModel>>($"word/all");


        private async Task<ApiResponse<T>> PostJson<T>(string path, object request)
        {
            string url = ConstructUrl(path);

            if (await jsInterop.GetToken() != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await jsInterop.GetToken());
            }

            try
            {
                return await this.httpClient.PostJsonAsync<ApiResponse<T>>(url, request);
            }
            catch (Exception ex)
            {

                var response = new ApiResponse<T>();
                response.AddError(ex.Message);
                return response;
            }
        }

        private async Task<ApiResponse<T>> GetJson<T>(string path)
        {
            string url = ConstructUrl(path);

            if (await jsInterop.GetToken() != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await jsInterop.GetToken());
            }

            try
            {
                return await this.httpClient.GetJsonAsync<ApiResponse<T>>(url);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<T>();
                response.AddError(ex.Message);
                return response;
            }
        }

        private string ConstructUrl(string path)
        {
            return GlobalConstants.ApiBaseUrl + path;
        }
    }
}
