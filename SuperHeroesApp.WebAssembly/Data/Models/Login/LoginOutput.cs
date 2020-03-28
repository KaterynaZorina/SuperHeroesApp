namespace SuperHeroesApp.WebAssembly.Data.Models.Login
{
    public class LoginOutput
    {
        public bool IsSuccessful { get; set; }

        public string ErrorMessage { get; set; }

        public LoginOutput Success()
        {
            IsSuccessful = true;
            ErrorMessage = null;

            return this;
        }

        public LoginOutput Error(string errorMessage)
        {
            IsSuccessful = false;
            ErrorMessage = errorMessage;
            
            return this;
        }
    }
}