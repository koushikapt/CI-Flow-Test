using App.Core.Models.View;

namespace App.Core.Services.Membership
{
    /// <summary>
    /// Membership Login/Logout interface service.
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Login to the system.
        /// </summary>
        /// <param name="model">Login view model.</param>
        /// <returns>If successful login.</returns>
        bool Login(LoginViewModel model);

        /// <summary>
        /// Log out of the system.
        /// </summary>
        void Logout();
    }
}
