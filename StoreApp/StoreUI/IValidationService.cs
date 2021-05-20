namespace StoreUI
{
    /// <summary>
    /// Standardized validation service for basic user input
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Takes in a prompt, and keeps asking that prompt until the user returns a valid string
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
         string ValidateString(string prompt);

         /// <summary>
         /// Takes in a prompt, and keeps asking that prompt until the user returns a valid int
         /// </summary>
         /// <param name="prompt"></param>
         /// <returns></returns>
         int ValidateInt(string prompt);

        /// <summary>
        /// Takes in a prompt, and keeps asking that prompt until the user returns a valid double
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
         double ValidatePrice(string prompt);
    }
}