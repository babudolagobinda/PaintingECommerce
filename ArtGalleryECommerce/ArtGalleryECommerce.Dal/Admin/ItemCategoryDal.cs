using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ArtGalleryECommerce.Dal.Repository;
using ArtGalleryECommerce.Model.AdminDTO;
using System.Collections.Generic;

namespace ArtGalleryECommerce.Dal.Admin
{
    public class ItemCategoryDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();

        public int SaveItemCategory(ItemCategoryDto itemCategoryDto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveAndUpdateItemCategory", connectionRepository.con);
                cmd.Parameters.AddWithValue("@CategoryId", itemCategoryDto.CategoryId);
                cmd.Parameters.AddWithValue("@GroupId", itemCategoryDto.GroupId);
                cmd.Parameters.AddWithValue("@CategoryName", itemCategoryDto.CategoryName);
                cmd.Parameters.AddWithValue("@CategoryDesc", itemCategoryDto.CategoryDesc);
                cmd.Parameters.AddWithValue("@CategoryImage", itemCategoryDto.CategoryImage);
                cmd.Parameters.AddWithValue("@CreatedBy", itemCategoryDto.CreatedBy);
                cmd.Parameters.AddWithValue("@ModifiedBy", itemCategoryDto.ModifiedBy);
                cmd.Parameters.AddWithValue("@IsActive", itemCategoryDto.IsActive);
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
        public List<ItemCategoryDto> GetAndEditItemCategory(int CategoryId, int IsActive)
        {
            List<ItemCategoryDto> lstItemCategoryDto = new List<ItemCategoryDto>();
            SqlCommand cmd = new SqlCommand("GetAndEditItemCategory", connectionRepository.con);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ItemCategoryDto itemCategoryDto = new ItemCategoryDto();
                itemCategoryDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                itemCategoryDto.GroupName = Convert.ToString(dr["GroupName"]);
                itemCategoryDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                itemCategoryDto.CategoryName = Convert.ToString(dr["CategoryName"]);
                itemCategoryDto.CategoryDesc = Convert.ToString(dr["CategoryDesc"]);
                itemCategoryDto.CategoryImage = Convert.ToString(dr["CategoryImage"]);
                itemCategoryDto.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                itemCategoryDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                itemCategoryDto.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"] != DBNull.Value ? dr["ModifiedBy"] : 0);
                itemCategoryDto.ModifiedDate = Convert.ToString(dr["ModifiedDate"] != DBNull.Value ? dr["ModifiedDate"] : "");
                itemCategoryDto.IsActive = Convert.ToInt32(dr["IsActive"]);
                lstItemCategoryDto.Add(itemCategoryDto);
            }
            connectionRepository.con.Close();
            return lstItemCategoryDto;
        }
        public int DeleteItemCategory(int CategoryId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteItemCategory", connectionRepository.con);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
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
        public List<ItemCategoryDto> GetItemCategoryByGroupId(int GroupId)
        {
            List<ItemCategoryDto> lstItemCategoryDto = new List<ItemCategoryDto>();
            SqlCommand cmd = new SqlCommand("GetItemCategoryByGroupId", connectionRepository.con);
            cmd.Parameters.AddWithValue("@GroupId", GroupId);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ItemCategoryDto itemCategoryDto = new ItemCategoryDto();
                itemCategoryDto.GroupId = Convert.ToInt32(dr["GroupId"]);               
                itemCategoryDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                itemCategoryDto.CategoryName = Convert.ToString(dr["CategoryName"]);
                lstItemCategoryDto.Add(itemCategoryDto);
            }
            connectionRepository.con.Close();
            return lstItemCategoryDto;
        }
    }
}
