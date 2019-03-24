using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommonLayer.UserModels;
using BusinessLayer.Interfaces;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CoreApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfigurationRoot _configuration;
        IUserBusiness _repository;
        IUserRoleBusiness _repositoryRole;
        readonly string SecretKey;
        readonly string Issuer;
        readonly string Audience;
        readonly int ExpireMinute;
        public AuthController(IUserBusiness repository, IUserRoleBusiness repositoryRole)
        {
            _repository = repository;
            _repositoryRole = repositoryRole;
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            SecretKey = _configuration.GetSection("TokenSettings:SecretKey").Value;
            Issuer = _configuration.GetSection("TokenSettings:Issuer").Value;
            Audience = _configuration.GetSection("TokenSettings:Audience").Value;
            ExpireMinute = Convert.ToInt32(_configuration.GetSection("TokenSettings:ExpireMinute").Value);

        }
        [Route("v1/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await Task.Run(()=> _repository.Login(model));
            if (user.ResultStatus)
            {
                var userroleresult= await Task.Run(() => _repositoryRole.Get(user.ResultEntity.refUserRole));
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.ResultEntity.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
                var token = new JwtSecurityToken(
                    issuer: Issuer,
                    audience: Audience,
                    expires: DateTime.UtcNow.AddMinutes(ExpireMinute),
                    claims: claims,
                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                    );

                var tokenmodel = new TokenModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    FullName=user.ResultEntity.FirstName + " " + user.ResultEntity.LastName,
                    UserRoleId=user.ResultEntity.refUserRole,
                    UserRoleName=userroleresult.ResultEntity.Name
                };

                _repository.UpdateToken(user.ResultEntity.Id, tokenmodel.Token);
                return Ok(tokenmodel);
            }
            return Unauthorized();
        }
    }
}