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
    public class ItemDetailsDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();

        public int SaveItemDetails(ItemDetailsDto itemDetailsDto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveAndUpdateItemDetails", connectionRepository.con);
                cmd.Parameters.AddWithValue("@ItemDetailsId", itemDetailsDto.ItemDetailsId);
                cmd.Parameters.AddWithValue("@ItemId", itemDetailsDto.ItemId);
                cmd.Parameters.AddWithValue("@Width", itemDetailsDto.Width);
                cmd.Parameters.AddWithValue("@WidthType", itemDetailsDto.WidthType);
                cmd.Parameters.AddWithValue("@Height", itemDetailsDto.Height);
                cmd.Parameters.AddWithValue("@HeightType", itemDetailsDto.HeightType);
                cmd.Parameters.AddWithValue("@Mrp", itemDetailsDto.Mrp);
                cmd.Parameters.AddWithValue("@Discount", itemDetailsDto.Discount);
                cmd.Parameters.AddWithValue("@Price", itemDetailsDto.Price);
                cmd.Parameters.AddWithValue("@CreatedBy", itemDetailsDto.CreatedBy);
                cmd.Parameters.AddWithValue("@ModifiedBy", itemDetailsDto.ModifiedBy);
                cmd.Parameters.AddWithValue("@IsActive", itemDetailsDto.IsActive);
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
        public List<ItemDetailsDto> GetAndEditItemDetails(int ItemDetailsId, int IsActive)
        {
            List<ItemDetailsDto> lstItemDetailsDto = new List<ItemDetailsDto>();
            SqlCommand cmd = new SqlCommand("GetAndEditItemDetails", connectionRepository.con);
            cmd.Parameters.AddWithValue("@ItemDetailsId", ItemDetailsId);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ItemDetailsDto itemDetailsDto = new ItemDetailsDto();
                itemDetailsDto.ItemDetailsId= Convert.ToInt32(dr["ItemDetailsId"]);
                itemDetailsDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                itemDetailsDto.ItemName = Convert.ToString(dr["ItemName"]);
                itemDetailsDto.Width = Convert.ToDecimal(dr["Width"]);
                itemDetailsDto.WidthType = Convert.ToString(dr["WidthType"]);
                itemDetailsDto.Height = Convert.ToDecimal(dr["Height"]);
                itemDetailsDto.HeightType = Convert.ToString(dr["HeightType"]);
                itemDetailsDto.Mrp = Convert.ToDecimal(dr["Mrp"]);
                itemDetailsDto.Discount = Convert.ToDecimal(dr["Discount"]);
                itemDetailsDto.Price = Convert.ToDecimal(dr["Price"]);
                itemDetailsDto.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                itemDetailsDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                itemDetailsDto.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"] != DBNull.Value ? dr["ModifiedBy"] : 0);
                itemDetailsDto.ModifiedDate = Convert.ToString(dr["ModifiedDate"] != DBNull.Value ? dr["ModifiedDate"] : "");
                itemDetailsDto.IsActive = Convert.ToInt32(dr["IsActive"]);
                lstItemDetailsDto.Add(itemDetailsDto);
            }
            connectionRepository.con.Close();
            return lstItemDetailsDto;
        }

        public int DeleteItemDetails(int ItemDetailsId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteItemDetails", connectionRepository.con);
                cmd.Parameters.AddWithValue("@ItemDetailsId", ItemDetailsId);
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
