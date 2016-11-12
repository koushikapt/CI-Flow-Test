using System.ComponentModel.DataAnnotations;

namespace App.Core.Models.View
{
    /// <summary>
    /// Login View Model.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the user login email.
        /// </summary>
        /// <value>
        /// The user login email.
        /// </value>
        [Required]
        [MinLength(3)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user login password.
        /// </summary>
        /// <value>
        /// The user login password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user should be remebered.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember me]; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
