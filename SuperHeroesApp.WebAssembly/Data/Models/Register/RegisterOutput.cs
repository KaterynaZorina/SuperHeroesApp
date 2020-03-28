using System.Collections.Generic;

namespace SuperHeroesApp.WebAssembly.Data.Models.Register
{
    public class RegisterOutput
    {
        public bool IsSuccessful { get; set; }

        public List<string> ErrorMessages { get; set; } = new List<string>();
        
        public RegisterOutput Success()
        {
            IsSuccessful = true;
            ErrorMessages = new List<string>();

            return this;
        }

        public RegisterOutput Error(string errorMessage)
        {
            IsSuccessful = false;
            ErrorMessages.Add(errorMessage);
            
            return this;
        }
    }
}