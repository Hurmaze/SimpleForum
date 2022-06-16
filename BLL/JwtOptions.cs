using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BLL
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public int TokenLifeTime { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
        }
    }
}
