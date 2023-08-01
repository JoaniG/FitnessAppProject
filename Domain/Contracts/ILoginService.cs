using DTO.UserDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ILoginService
    {
        Task<TokenDTO> Login(LoginDTO credentials);
        Task<TokenDTO> RefreshTokenAsync(string refreshToken);
        void RevokeRefreshTokenAsync(string refreshToken);
        TokenDTO Register(RegisterDTO registerDTO);
        void ChangePassword(PasswordChangeDTO passwordChangeDTO);
        bool CheckPassword(string password);

    }
}
