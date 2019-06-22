using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ArtGalleryECommerce.Dal.Repository;
using ArtGalleryECommerce.Model.AdminDTO;


namespace ArtGalleryECommerce.Dal.Admin
{
    public class ItemMasterDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();

        public int SaveItemMaster(ItemMasterDto itemMasterDto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveAndUpdateItemMaster", connectionRepository.con);
                cmd.Parameters.AddWithValue("@ItemId", itemMasterDto.ItemId);
                cmd.Parameters.AddWithValue("@GroupId", itemMasterDto.GroupId);
                cmd.Parameters.AddWithValue("@CategoryId", itemMasterDto.CategoryId);
                cmd.Parameters.AddWithValue("@ItemName", itemMasterDto.ItemName);
                cmd.Parameters.AddWithValue("@ItemDesc", itemMasterDto.ItemDesc);
                cmd.Parameters.AddWithValue("@ItemImage", itemMasterDto.ItemImage);
                cmd.Parameters.AddWithValue("@CreatedBy", itemMasterDto.CreatedBy);
                cmd.Parameters.AddWithValue("@ModifiedBy", itemMasterDto.ModifiedBy);
                cmd.Parameters.AddWithValue("@IsActive", itemMasterDto.IsActive);
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

        public List<ItemMasterDto> GetAndEditItemMaster(int ItemId, int IsActive)
        {
            List<ItemMasterDto> lstItemMasterDto = new List<ItemMasterDto>();
            SqlCommand cmd = new SqlCommand("GetAndEditItemMaster", connectionRepository.con);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ItemMasterDto itemMasterDto = new ItemMasterDto();
                itemMasterDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                itemMasterDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                itemMasterDto.GroupName = Convert.ToString(dr["GroupName"]);
                itemMasterDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                itemMasterDto.CategoryName = Convert.ToString(dr["CategoryName"]);
                itemMasterDto.ItemName = Convert.ToString(dr["ItemName"]);
                itemMasterDto.ItemDesc = Convert.ToString(dr["ItemDesc"]);
                itemMasterDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                itemMasterDto.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                itemMasterDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                itemMasterDto.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"] != DBNull.Value ? dr["ModifiedBy"] : 0);
                itemMasterDto.ModifiedDate = Convert.ToString(dr["ModifiedDate"] != DBNull.Value ? dr["ModifiedDate"] : "");
                itemMasterDto.IsActive = Convert.ToInt32(dr["IsActive"]);
                lstItemMasterDto.Add(itemMasterDto);
            }
            connectionRepository.con.Close();
            return lstItemMasterDto;
        }
        public int DeleteItemMaster(int ItemId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteItemMaster", connectionRepository.con);
                cmd.Parameters.AddWithValue("@ItemId", ItemId);
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
        public List<ItemMasterDto> GetItemByCategoryId(int CategoryId)
        {
            List<ItemMasterDto> lstItemMasterDto = new List<ItemMasterDto>();
            SqlCommand cmd = new SqlCommand("GetItemByCategoryId", connectionRepository.con);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ItemMasterDto itemMasterDto = new ItemMasterDto();
                itemMasterDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                itemMasterDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                itemMasterDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                itemMasterDto.ItemName = Convert.ToString(dr["ItemName"]);
                itemMasterDto.ItemDesc = Convert.ToString(dr["ItemDesc"]);
                itemMasterDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                itemMasterDto.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                itemMasterDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                itemMasterDto.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"] != DBNull.Value ? dr["ModifiedBy"] : 0);
                itemMasterDto.ModifiedDate = Convert.ToString(dr["ModifiedDate"] != DBNull.Value ? dr["ModifiedDate"] : "");
                itemMasterDto.IsActive = Convert.ToInt32(dr["IsActive"]);
                lstItemMasterDto.Add(itemMasterDto);
            }
            connectionRepository.con.Close();
            return lstItemMasterDto;
        }
    }
}
