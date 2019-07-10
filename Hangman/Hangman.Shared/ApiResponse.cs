using System.Collections.Generic;
using System.Linq;

namespace Hangman.Shared
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
        }

        public ApiResponse(T data)
        {
            this.Data = data;
        }

        public ApiResponse(IEnumerable<ApiError> errors)
        {
            if (errors == null || !errors.Any())
            {
                this.Errors = new List<ApiError> { new ApiError("ApiResponse", "Unspecified error.") };
            }
            else
            {
                this.Errors = errors.ToList();
            }
        }

        public ApiResponse(ApiError error)
        {
            this.Errors = new List<ApiError> { error };
        }

        public List<ApiError> Errors { get; set; }

        public T Data { get; set; }

        public bool IsOk => this.Errors == null || this.Errors.Count == 0;

        public void AddError(ApiError error)
        {
            if(error == null)
            {
                return;
            }

            if(this.Errors == null)
            {
                this.Errors = new List<ApiError>();
            }

            this.Errors.Add(error);
        }
    }
}
