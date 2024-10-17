using Microsoft.AspNetCore.Identity;

namespace finance_api.Domain.Users.Exceptions
{
    public class CouldNotCreateUserException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; set; }
        public CouldNotCreateUserException(IEnumerable<IdentityError> errors) : base($"Could not create the user {string.Join("; ", errors.Select(e => e.Description))}")
        {
            Errors = errors;
        }
    }
}
