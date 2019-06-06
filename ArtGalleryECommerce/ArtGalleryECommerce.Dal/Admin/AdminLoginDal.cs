using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryECommerce.Model.AdminDTO;
using System.Data;
using System.Data.SqlClient;

namespace ArtGalleryECommerce.Dal.Admin
{
    public class AdminLoginDal
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        public AdminLoginDto GetAdminLogin(AdminLoginDto adminData)
        {
            AdminLoginDto adminLoginDto = new AdminLoginDto();
            SqlCommand cmd = new SqlCommand("GetAdminDetails", con);
            cmd.Parameters.AddWithValue("@UserName", adminData.UserName);
            cmd.Parameters.AddWithValue("@Password", adminData.Password);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                adminLoginDto.AdminId = Convert.ToInt32(dr["AdminId"]);
                adminLoginDto.Name = Convert.ToString(dr["Name"]);
                adminLoginDto.EmailId = Convert.ToString(dr["EmailId"]);
                adminLoginDto.ProfPic = Convert.ToString(dr["ProfPic"]);
                adminLoginDto.UserName = Convert.ToString(dr["UserName"]);
                adminLoginDto.Password = Convert.ToString(dr["Password"]);
                adminLoginDto.UserRole = Convert.ToString(dr["UserRole"]);
            }
            return adminLoginDto;
        }
    }
}
