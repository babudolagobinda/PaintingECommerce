using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryECommerce.Model.UserDTO;
using ArtGalleryECommerce.Dal.Repository;
using System.Data;
using System.Data.SqlClient;



namespace ArtGalleryECommerce.Dal.User
{
    public class UserSignUpDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();
        public int SaveUserDetails(UserSignUpDto userSignUp)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveUserDetails", connectionRepository.con);
                cmd.Parameters.AddWithValue("@UserId", userSignUp.UserId);
                cmd.Parameters.AddWithValue("@Name", userSignUp.Name);
                cmd.Parameters.AddWithValue("@EmailId", userSignUp.EmailId);
                cmd.Parameters.AddWithValue("@Password", userSignUp.Password);
                cmd.Parameters.AddWithValue("@MobileNo", userSignUp.MobileNo);
                cmd.Parameters.AddWithValue("@Gender", userSignUp.Gender);
                cmd.Parameters.AddWithValue("@IsActive", userSignUp.IsActive);
                cmd.CommandType = CommandType.StoredProcedure;
                connectionRepository.con.Open();
                int i = cmd.ExecuteNonQuery();
                connectionRepository.con.Close();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserSignUpDto UserSignIn(UserSignUpDto userSignUp)
        {
            UserSignUpDto userSignUpDto = new UserSignUpDto();
            SqlCommand cmd = new SqlCommand("GetUserDetails", connectionRepository.con);
            cmd.Parameters.AddWithValue("@EmailId", userSignUp.EmailId);
            cmd.Parameters.AddWithValue("@Password", userSignUp.Password);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                userSignUpDto.UserId = Convert.ToInt32(dr["UserId"]);
                userSignUpDto.Name = Convert.ToString(dr["Name"]);
                userSignUpDto.EmailId = Convert.ToString(dr["EmailId"]);
                userSignUpDto.Password = Convert.ToString(dr["Password"]);
                userSignUpDto.MobileNo = Convert.ToString(dr["MobileNo"]);
                userSignUpDto.Gender = Convert.ToString(dr["Gender"]);
            }
            connectionRepository.con.Close();
            return userSignUpDto;
        }
        public UserSignUpDto GetUserDetailsByUserId(int UserId)
        {
            UserSignUpDto userSignUpDto = new UserSignUpDto();
            SqlCommand cmd = new SqlCommand("GetUserDetailsByUserId", connectionRepository.con);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                userSignUpDto.UserId = Convert.ToInt32(dr["UserId"]);
                userSignUpDto.Name = Convert.ToString(dr["Name"]);
                userSignUpDto.EmailId = Convert.ToString(dr["EmailId"]);
                userSignUpDto.Password = Convert.ToString(dr["Password"]);
                userSignUpDto.ConfirmPassword = Convert.ToString(dr["Password"]);
                userSignUpDto.MobileNo = Convert.ToString(dr["MobileNo"]);
                userSignUpDto.Gender = Convert.ToString(dr["Gender"]);
            }
            connectionRepository.con.Close();
            return userSignUpDto;
        }
        public List<UserSignUpDto> GetAllUserList()
        {
            List<UserSignUpDto> lstUserSignUpDto = new List<UserSignUpDto>();
            SqlCommand cmd = new SqlCommand("GetAllUserList", connectionRepository.con);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserSignUpDto userSignUpDto = new UserSignUpDto();
                userSignUpDto.UserId = Convert.ToInt32(dr["UserId"]);
                userSignUpDto.Name = Convert.ToString(dr["Name"]);
                userSignUpDto.EmailId = Convert.ToString(dr["EmailId"]);
                userSignUpDto.Password = Convert.ToString(dr["Password"]);
                userSignUpDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                userSignUpDto.MobileNo = Convert.ToString(dr["MobileNo"]);
                userSignUpDto.Gender = Convert.ToString(dr["Gender"]);
                userSignUpDto.IsActive = Convert.ToInt32(dr["IsActive"]);
                lstUserSignUpDto.Add(userSignUpDto);
            }
            connectionRepository.con.Close();
            return lstUserSignUpDto;
        }
        public int UpdateIsActiveUser(int UserId,int IsActive)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateIsActiveUser", connectionRepository.con);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.CommandType = CommandType.StoredProcedure;
                connectionRepository.con.Open();
                int i = cmd.ExecuteNonQuery();
                connectionRepository.con.Close();
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
