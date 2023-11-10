using GymAppAPI.Models;
using GymAppAPI.Models.Common;
using GymAppAPI.Models.Request;
using GymAppAPI.Models.Response;
using GymAppAPI.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymAppAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private IEmailService _iEmailService;

        public UserService(IOptions<AppSettings> appSettings, IEmailService iEmailService)
        {
            _appSettings = appSettings.Value;
            _iEmailService = iEmailService;
        }


        public UserResponse Auth(AuthRequest oModel)
        {
            UserResponse userResponse = new UserResponse();

            using(var db = new GymAppDbContext())
            {
                using(var transacton = db.Database.BeginTransaction())
                {
                    try
                    {
                        string password = Encrypt.GetSHA256(oModel.Password);

                        var user = db.Users.Where(d => d.Email == oModel.Email &&
                                                  d.Password == password).FirstOrDefault();

                        if(user == null) return null;

                        userResponse.Email = user.Email;
                        userResponse.nickName = user.NickName;
                        userResponse.Token = GetToken(user);   
                        
                        transacton.Commit();
                    }
                    catch (Exception ex)
                    {
                        transacton.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
            return userResponse;
        }

        
        public void CreateAccount(UserRequest oModel)
        {
            using(var db = new GymAppDbContext())
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var userValidation = db.Users.Where(d => d.Email == oModel.email).FirstOrDefault();

                        if (userValidation != null)
                            throw new Exception($"User {oModel.email} already exist");

                        User user = new User();
                        user.Email = oModel.email;
                        user.NickName = oModel.nickName;
                        user.Password = Encrypt.CalculateSHA256(oModel.password);
                        db.Users.Add(user);
                        db.SaveChanges();
                        transaction.Commit();



                        EmailRequest request = new EmailRequest();
                        request.To = "javmarquez13@gmail.com";
                        request.Subject = "Gym for Everyone";
                        request.Body = "<p>Account created successfully. Please follow this clikc to confirm your account <strong>confirmation</strong> of the email.</p>" +
                                       "<p>Link</p>";

                        _iEmailService.SendEmail(request);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
        
        
        
        
        
        

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }
                ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }



    }
}
