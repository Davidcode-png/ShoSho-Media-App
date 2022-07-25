using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        // Creating a symmetric key that can both be encrypetd and decrypted
        private readonly SymmetricSecurityKey _key;
        // A constructor to get our configuration from the IConfiguration config
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        // Generating Claims
        // A claim is just something about a user, for example a user claims that their username is 'bob'.  We then store this information in a token.   We can trust a token as it is securely signed by the API server, so if a user 'claims' their username is 'bob' then we can believe it
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Properties in a token descriptor
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            //Handles the token, just look at the name :)
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            //Serializes a jwt 
            return tokenHandler.WriteToken(token);
        }
    }
}
