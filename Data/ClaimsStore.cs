using System.Security.Claims;

namespace Spark.Data
{
    public class ClaimsStore
    {
        public static List<Claim> Claim() 
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new System.Security.Claims.Claim ("User","User"));
            claims.Add(new System.Security.Claims.Claim("Admin", "Admin"));
            return claims;
        }
    }
}
