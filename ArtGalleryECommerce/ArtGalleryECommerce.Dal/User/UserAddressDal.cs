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
    public class UserAddressDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();
        public int SaveAndUpdateUserAddress(UserAddressDto userAddressDto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveAndUpdateUserAddress", connectionRepository.con);
                cmd.Parameters.AddWithValue("@AddressId", userAddressDto.AddressId ?? 0);
                cmd.Parameters.AddWithValue("@UserId", userAddressDto.UserId);
                cmd.Parameters.AddWithValue("@Name", userAddressDto.Name);
                cmd.Parameters.AddWithValue("@MobileNo", userAddressDto.MobileNo);
                cmd.Parameters.AddWithValue("@Pincode", userAddressDto.Pincode);
                cmd.Parameters.AddWithValue("@Address", userAddressDto.Address);
                cmd.Parameters.AddWithValue("@Locality", userAddressDto.Locality);
                cmd.Parameters.AddWithValue("@City", userAddressDto.City);
                cmd.Parameters.AddWithValue("@State", userAddressDto.State);
                cmd.Parameters.AddWithValue("@Country", userAddressDto.Country);
                cmd.Parameters.AddWithValue("@IsActive", userAddressDto.IsActive);
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
        public List<UserAddressDto> GetUserAddressByUserId(int AddressId,int UserId,int IsActive)
        {
            List<UserAddressDto> lstUserAddressDto = new List<UserAddressDto>();
            SqlCommand cmd = new SqlCommand("GetUserAddressByUserId", connectionRepository.con);
            cmd.Parameters.AddWithValue("@AddressId", AddressId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserAddressDto userAddressDto = new UserAddressDto();
                userAddressDto.AddressId = Convert.ToInt32(dr["AddressId"]);
                userAddressDto.UserId = Convert.ToInt32(dr["UserId"]);
                userAddressDto.Name = Convert.ToString(dr["Name"]);
                userAddressDto.MobileNo = Convert.ToString(dr["MobileNo"]);
                userAddressDto.Pincode = Convert.ToString(dr["Pincode"]);
                userAddressDto.Address = Convert.ToString(dr["Address"]);
                userAddressDto.Locality = Convert.ToString(dr["Locality"]);
                userAddressDto.City = Convert.ToString(dr["City"]);
                userAddressDto.State = Convert.ToString(dr["State"]);
                userAddressDto.Country = Convert.ToString(dr["Country"]);
                userAddressDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                userAddressDto.IsActive = Convert.ToInt32(dr["IsActive"]);
                lstUserAddressDto.Add(userAddressDto);
            }
            connectionRepository.con.Close();
            return lstUserAddressDto;
        }
        public UserAddressDto GetUserAddressByAddressId(int AddressId, int UserId, int IsActive)
        {
            UserAddressDto userAddressDto = new UserAddressDto();
            SqlCommand cmd = new SqlCommand("GetUserAddressByUserId", connectionRepository.con);
            cmd.Parameters.AddWithValue("@AddressId", AddressId);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                
                userAddressDto.AddressId = Convert.ToInt32(dr["AddressId"]);
                userAddressDto.UserId = Convert.ToInt32(dr["UserId"]);
                userAddressDto.Name = Convert.ToString(dr["Name"]);
                userAddressDto.MobileNo = Convert.ToString(dr["MobileNo"]);
                userAddressDto.Pincode = Convert.ToString(dr["Pincode"]);
                userAddressDto.Address = Convert.ToString(dr["Address"]);
                userAddressDto.Locality = Convert.ToString(dr["Locality"]);
                userAddressDto.City = Convert.ToString(dr["City"]);
                userAddressDto.State = Convert.ToString(dr["State"]);
                userAddressDto.Country = Convert.ToString(dr["Country"]);
                userAddressDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                userAddressDto.IsActive = Convert.ToInt32(dr["IsActive"]);
            }
            connectionRepository.con.Close();
            return userAddressDto;
        }
        public int DeleteUserddressByUserId(int UserId,int AddressId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteUserddressByUserId", connectionRepository.con);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@AddressId", AddressId);
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
