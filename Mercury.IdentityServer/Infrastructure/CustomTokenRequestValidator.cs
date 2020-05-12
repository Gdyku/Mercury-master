using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercury.IdentityServer.Infrastructure
{
    public class CustomTokenRequestValidator : ICustomTokenRequestValidator
    {
        /// <summary>
        /// Custom validation logic for a token request.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The validation result
        /// </returns>
        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            await Task.FromResult(true);
        }
    }
}
