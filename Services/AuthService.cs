using Microsoft.IdentityModel.Tokens;
using movie_project.DataConection;
using movie_project.Entities;
using movie_project.ImageAndEmail.Email;
using movie_project.Payloads.Converters;
using movie_project.Payloads.DTOs;
using movie_project.Payloads.Requests.UserRequests;
using movie_project.Payloads.Responses;
using movie_project.Services.InterfaceService;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using movie_project.ImageAndEmail.Image;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

namespace movie_project.Services
{
    public class AuthService : IAuthService
    {
        private readonly ResponseObject<UserDTO> _responseObjectUserDTO;
        private readonly AppDbContext _context;
        private readonly UserConverter _converter;
        private readonly IConfiguration _configuration;
        private readonly ResponseObject<RefreshTokenDTO> _responseObjectToken;
        public AuthService(IConfiguration configuration)
        {
            _context = new AppDbContext();
            _responseObjectUserDTO = new ResponseObject<UserDTO>();
            _converter = new UserConverter();
            _configuration = configuration;
            _responseObjectToken = new ResponseObject<RefreshTokenDTO>();
        }
        public async Task<string> ChangePassword(int userID, ChangePasswordRequest request)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.ID == userID);
            bool checkPass = BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password);
            if (!checkPass)
            {
                return "The password does not match the current password";
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _context.users.Update(user);
            await _context.SaveChangesAsync();
            return "Password changed successfully";
        }

        public async Task<ResponseObject<UserDTO>> CreateNewPassword(CreateNewPasswordRequest request)
        {
            ConfirmEmail confirmEmail = await _context.confirmEmails
                .Where(x => x.ConfirmCode.Equals(request.Verification))
                .FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return _responseObjectUserDTO
                    .ResponseObjectError(
                    StatusCodes.Status400BadRequest,
                    "Confirmation code is incorrect",
                    null
                );
            }
            if (confirmEmail.ExpiredTime < DateTime.Now)
            {
                return _responseObjectUserDTO
                    .ResponseObjectError(
                    StatusCodes.Status400BadRequest,
                    "Verification code has expired",
                    null
                );
            }
            User user = _context.users.FirstOrDefault(x => x.ID == confirmEmail.UserID);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _context.confirmEmails.Remove(confirmEmail);
            _context.users.Update(user);
            await _context.SaveChangesAsync();
            return _responseObjectUserDTO
                .ResponseObjectSuccess(
                "New password created successfully", 
                _converter.ToDTO(user)
            );
        }

        public async Task<string> ForgotPassword(ForgotAccountPasswordRequest request)
        {
            User user = await _context.users.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            if (user is null)
            {
                return "Email does not exist in the system";
            }
            else
            {
                var confirms = _context.confirmEmails.Where(x => x.UserID == user.ID).ToList();
                _context.confirmEmails.RemoveRange(confirms);
                await _context.SaveChangesAsync();
                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    UserID = user.ID,
                    IsConfirm = false,
                    ExpiredTime = DateTime.Now.AddHours(4),
                    ConfirmCode = "gwenchana" + "_" + GenerateCodeActive().ToString()
                };
                await _context.confirmEmails.AddAsync(confirmEmail);
                await _context.SaveChangesAsync();
                string message = SendEmail(new EmailTo
                {
                    MailTo = request.Email,
                    Topic = "Get the confirmation code to create a new password from here: ",
                    Content = $"Your activation code is: {confirmEmail.ConfirmCode}, this code will expire after 4 hours"
                });
                return "Email confirmation code sent successfully, please check your email";
            }
        }

        public RefreshTokenDTO GenerateAccessToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value!);

            var decentralization = _context.roles.FirstOrDefault(x => x.ID == user.RoleID);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("ID", user.ID.ToString()),
                    new Claim("Username", user.Username),
                    new Claim("RoleID", user.RoleID.ToString()),
                    new Claim(ClaimTypes.Role, decentralization?.RoleName ?? "")
                }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                ExpiredTime = DateTime.Now.AddHours(10),
                UserID = user.ID
            };

            _context.refreshTokens.Add(rf);
            _context.SaveChanges();

            RefreshTokenDTO tokenDTO = new RefreshTokenDTO
            {
                Verification = accessToken,
                RefreshCode = refreshToken
            };
            return tokenDTO;
        }
        private int GenerateCodeActive()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }

        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public async Task<ResponseObject<RefreshTokenDTO>> Login(LoginAccountRequest request)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Username.Equals(request.Username));
            if (user == null)
            {
                return _responseObjectToken.ResponseObjectError(StatusCodes.Status404NotFound, "Account name is incorrect", null);
            }
            bool checkPass = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!checkPass)
            {
                return _responseObjectToken.ResponseObjectError(StatusCodes.Status400BadRequest, "Password is incorrect", null);
            }
            else
            {
                return _responseObjectToken.ResponseObjectSuccess("Logged in successfully", GenerateAccessToken(user));
            }
        }

        public async Task<ResponseObject<UserDTO>> Register(RegisterAccountRequest request)
        {
            if (!Validate.IsValidEmail(request.Email))
            {
                return _responseObjectUserDTO.ResponseObjectError(StatusCodes.Status400BadRequest, "Invalid email format", null);
            }
            if (!Validate.IsValidPhoneNumber(request.PhoneNumber))
            {
                return _responseObjectUserDTO.ResponseObjectError(StatusCodes.Status400BadRequest, "Invalid phone number", null);
            }
            if (await _context.users.SingleOrDefaultAsync(x => x.Username.Equals(request.Username)) != null)
            {
                return _responseObjectUserDTO.ResponseObjectError(StatusCodes.Status400BadRequest, "The account already exists on the system", null);
            }
            if (await _context.users.SingleOrDefaultAsync(x => x.Email.Equals(request.Email)) != null)
            {
                return _responseObjectUserDTO.ResponseObjectError(StatusCodes.Status400BadRequest, "Email already exists on the system", null);
            }
            else
            {
                int imageSize = 2 * 1024 * 768;
                try
                {
                    User user = new User();
                    user.Username = request.Username;
                    user.Email = request.Email;
                    user.PhoneNumber = request.PhoneNumber;
                    user.Name = request.Name;
                    user.IsActive = true;
                    user.PhoneNumber = request.PhoneNumber;
                    user.RankCustomerID = request.RankCustomerID;
                    user.UserStatusID = request.UserStatusID;
                    user.Point = 0;
                    user.RoleID = 4;//ID = 4 => USER
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

                    await _context.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return _responseObjectUserDTO.ResponseObjectSuccess("Sign up Success", _converter.ToDTO(user));
                }
                catch (Exception ex)
                {
                    return _responseObjectUserDTO.ResponseObjectError(StatusCodes.Status500InternalServerError, ex.Message, null);
                }
            }
        }

        public ResponseObject<RefreshTokenDTO> RenewAccessToken(RefreshTokenDTO request)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _configuration.GetSection("AppSettings:SecretKey").Value;

                var tokenValidation = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

                var tokenAuthentication = jwtTokenHandler.ValidateToken(request.Verification, tokenValidation, out var validatedToken);

                if (!(validatedToken is JwtSecurityToken jwtSecurityToken) || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return _responseObjectToken.ResponseObjectError(StatusCodes.Status400BadRequest, "Invalid token", null);
                }

                var refreshToken = _context.refreshTokens.FirstOrDefault(x => x.Token == request.RefreshCode);

                if (refreshToken == null)
                {
                    return _responseObjectToken.ResponseObjectError(StatusCodes.Status404NotFound, "RefreshToken does not exist in the database", null);
                }

                if (refreshToken.ExpiredTime < DateTime.Now)
                {
                    return _responseObjectToken.ResponseObjectError(StatusCodes.Status401Unauthorized, "Tokens have not expired", null);
                }

                var user = _context.users.FirstOrDefault(x => x.ID == refreshToken.UserID);

                if (user == null)
                {
                    return _responseObjectToken.ResponseObjectError(StatusCodes.Status404NotFound, "User does not exist", null);
                }

                var newToken = GenerateAccessToken(user);

                return _responseObjectToken.ResponseObjectSuccess("Refresh token successfully", newToken);
            }
            catch (SecurityTokenValidationException ex)
            {
                return _responseObjectToken.ResponseObjectError(StatusCodes.Status400BadRequest, "Token validation error: " + ex.Message, null);
            }
            catch (Exception ex)
            {
                return _responseObjectToken.ResponseObjectError(StatusCodes.Status500InternalServerError, "An unknown error: " + ex.Message, null);
            }
        }

        public string SendEmail(EmailTo emailTo)
        {
            if (!Validate.IsValidEmail(emailTo.MailTo))
            {
                return "Invalid email format";
            }
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("zzznguyenmy@gmail.com", "nhjynfsgnalczpiv"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("zzznguyenmy@gmail.com");
                message.To.Add(emailTo.MailTo);
                message.Subject = emailTo.Topic;
                message.Body = emailTo.Content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);

                return "Email sent successfully";
            }
            catch (Exception ex)
            {
                return "Error sending email: " + ex.Message;
            }
        }

        public async Task<string> SetRole(int userID)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.ID == userID);

            if (user == null)
            {
                return "Account does not exist";
            }

            user.RoleID = (user.RoleID == 3) ? 4 : 3;

            try
            {
                _context.users.Update(user);
                await _context.SaveChangesAsync();
                return "Account permissions changed successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "Error while changing account permissions";
            }
        }
    }
}
