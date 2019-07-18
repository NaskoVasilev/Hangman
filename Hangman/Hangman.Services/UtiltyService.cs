using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Services
{
    public class UtiltyService : IUtilityService
    {
        public string NormalizeName(string category)
        {
            category = category[0].ToString().ToUpper() + category.Substring(1).ToLower();
            return category;
        }
    }
}
