using System;
using System.Data;
using System.Data.SqlClient;
using ArtGalleryECommerce.Dal.Repository;
using ArtGalleryECommerce.Model.AdminDTO;
using System.Collections.Generic;

namespace ArtGalleryECommerce.Dal.Admin
{
    public class ItemGroupDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();
        public int SaveItemGroup(ItemGroupDto itemGroupDto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveAndUpdateItemGroup", connectionRepository.con);
                cmd.Parameters.AddWithValue("@GroupId", itemGroupDto.GroupId);
                cmd.Parameters.AddWithValue("@GroupName", itemGroupDto.GroupName);
                cmd.Parameters.AddWithValue("@GroupDesc", itemGroupDto.GroupDesc);
                cmd.Parameters.AddWithValue("@GroupImage", itemGroupDto.GroupImage);
                cmd.Parameters.AddWithValue("@CreatedBy", itemGroupDto.CreatedBy);
                cmd.Parameters.AddWithValue("@ModifiedBy", itemGroupDto.ModifiedBy);
                cmd.Parameters.AddWithValue("@IsActive", itemGroupDto.IsActive);
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
        public List<ItemGroupDto> GetAndEditItemGroup(int GroupId, int IsActive)
        {
            List<ItemGroupDto> lstItemGroupDto = new List<ItemGroupDto>();
            SqlCommand cmd = new SqlCommand("GetAndEditItemGroup", connectionRepository.con);
            cmd.Parameters.AddWithValue("@GroupId", GroupId);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ItemGroupDto itemGroupDto = new ItemGroupDto();
                itemGroupDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                itemGroupDto.GroupName = Convert.ToString(dr["GroupName"]);
                itemGroupDto.GroupDesc = Convert.ToString(dr["GroupDesc"]);
                itemGroupDto.GroupImage = Convert.ToString(dr["GroupImage"]);
                itemGroupDto.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                itemGroupDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                itemGroupDto.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"] != DBNull.Value ? dr["ModifiedBy"] : 0);
                itemGroupDto.ModifiedDate = Convert.ToString(dr["ModifiedDate"] != DBNull.Value ? dr["ModifiedDate"] : "");
                itemGroupDto.IsActive = Convert.ToInt32(dr["IsActive"]);
                lstItemGroupDto.Add(itemGroupDto);
            }
            connectionRepository.con.Close();
            return lstItemGroupDto;
        }
        public int DeleteItemGroup(int GroupId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteItemGroup", connectionRepository.con);
                cmd.Parameters.AddWithValue("@GroupId", GroupId);
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
