using System.Collections.Generic;
using System.Linq;

namespace Hangman.Shared
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
        }

        public int HttpStatusCode { get; set; }

        public ApiResponse(T data)
        {
            this.Data = data;
        }

        public ApiResponse(IEnumerable<string> errors)
        {
            this.Errors = errors.ToList();
        }

        public List<string> Errors { get; set; }

        public T Data { get; set; }

        public bool IsOk => this.Errors == null || this.Errors.Count == 0;

        public void AddError(string error)
        {
            if (error == null)
            {
                return;
            }

            if (this.Errors == null)
            {
                this.Errors = new List<string>();
            }

            this.Errors.Add(error);
        }
    }
}
