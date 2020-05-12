using IdentityServer4.Models;
using IdentityServer4.Services;
using Mercury.DAL.Entities;
using Mercury.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mercury.IdentityServer.Infrastructure
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.Claims
              .SingleOrDefault(c => c.Type == "sub");

            var user = await _userRepository.Get(u => u.Email == sub.Value);

            if(user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim (ClaimTypes.Name, user.Email),
                    new Claim ("Id", user.Id.ToString())
                };

                context.IssuedClaims.AddRange(claims);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var idClaim = context.Subject.Claims.SingleOrDefault(c => c.Type == "Id");

            User user = null;

            if(idClaim == null)
            {
                var sub = context.Subject.Claims
                    .SingleOrDefault(c => c.Type == "sub");

                user = await _userRepository.Get(u => u.Email == sub.Value);
            }
            else
            {
                bool isParsed = long.TryParse(idClaim.Value, out long id);

                if (isParsed)
                {
                    user = await _userRepository.Get(u => u.Id == id);
                    context.IsActive = true;
                }
            }

            context.IsActive = user != null ? true : false;
        }
    }
}
