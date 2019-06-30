using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryECommerce.Model.AdminDTO;
using ArtGalleryECommerce.Dal.Repository;
using System.Data;
using System.Data.SqlClient;

namespace ArtGalleryECommerce.Dal.Admin
{
    public class MainBannerDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();

        public int SaveAndUpdateMainBanner(MainBannerDto mainBannerDto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveAndUpdateMainBanner", connectionRepository.con);
                cmd.Parameters.AddWithValue("@BannerId", mainBannerDto.BannerId);
                cmd.Parameters.AddWithValue("@ItemId", mainBannerDto.ItemId);
                cmd.Parameters.AddWithValue("@BannerName", mainBannerDto.BannerName);
                cmd.Parameters.AddWithValue("@BannerDesc", mainBannerDto.BannerDesc);
                cmd.Parameters.AddWithValue("@BannerImage", mainBannerDto.BannerImage);
                cmd.Parameters.AddWithValue("@CreatedBy", mainBannerDto.CreatedBy);
                cmd.Parameters.AddWithValue("@ModifiedBy", mainBannerDto.ModifiedBy);
                cmd.Parameters.AddWithValue("@IsActive", mainBannerDto.IsActive);
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
        public List<MainBannerDto> GetAllMainBanner(int BannerId,int IsActive)
        {
            List<MainBannerDto> lstMainBannerDto = new List<MainBannerDto>();
            SqlCommand cmd = new SqlCommand("GetAndEditMainBanner", connectionRepository.con);
            cmd.Parameters.AddWithValue("@BannerId", BannerId);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MainBannerDto mainBannerDto = new MainBannerDto();
                mainBannerDto.BannerId = Convert.ToInt32(dr["BannerId"]);
                mainBannerDto.BannerName = Convert.ToString(dr["BannerName"]);
                mainBannerDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                mainBannerDto.ItemName = Convert.ToString(dr["ItemName"]);
                mainBannerDto.BannerDesc = Convert.ToString(dr["BannerDesc"]);
                mainBannerDto.BannerImage = Convert.ToString(dr["BannerImage"]);
                mainBannerDto.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                mainBannerDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                mainBannerDto.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"] != DBNull.Value ? dr["ModifiedBy"] : 0);
                mainBannerDto.ModifiedDate = Convert.ToString(dr["ModifiedDate"] != DBNull.Value ? dr["ModifiedDate"] : "");
                mainBannerDto.IsActive = Convert.ToInt32(dr["IsActive"]);
                lstMainBannerDto.Add(mainBannerDto);
            }
            connectionRepository.con.Close();
            return lstMainBannerDto;
        }
        public int DeleteMainBanner(int BannerId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteMainBanner", connectionRepository.con);
                cmd.Parameters.AddWithValue("@BannerId", BannerId);
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
