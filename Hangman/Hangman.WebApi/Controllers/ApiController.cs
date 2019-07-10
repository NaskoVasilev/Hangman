using Hangman.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Hangman.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApiController : ControllerBase
	{
        protected ApiResponse<T> Error<T>(string message)
        {
            var response = new ApiResponse<T>();
            response.AddError(message);
            return response;
        }

        protected ApiResponse<T> ModelStateErrors<T>()
        {
            if (this.ModelState == null || this.ModelState.Count == 0)
            {
                var response = new ApiResponse<T>();
                response.AddError("Empty or null model.");
                return response;
            }

            var errors = new List<string>();
            foreach (var item in this.ModelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            return new ApiResponse<T>(errors);
        }
    }
}
