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
    public class StockMasterDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();

        public int SaveStockMaster(StockMasterDto stockMasterDto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveAndUpdateStockMaster", connectionRepository.con);
                cmd.Parameters.AddWithValue("@StockId", stockMasterDto.StockId);
                cmd.Parameters.AddWithValue("@GroupId", stockMasterDto.GroupId);
                cmd.Parameters.AddWithValue("@CategoryId", stockMasterDto.CategoryId);
                cmd.Parameters.AddWithValue("@ItemId", stockMasterDto.ItemId);
                cmd.Parameters.AddWithValue("@Quantity", stockMasterDto.Quantity);
                cmd.Parameters.AddWithValue("@CreatedBy", stockMasterDto.CreatedBy);
                cmd.Parameters.AddWithValue("@ModifiedBy", stockMasterDto.ModifiedBy);
                cmd.Parameters.AddWithValue("@IsActive", stockMasterDto.IsActive);
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

        public List<StockMasterDto> GetAndEditStockMaster(int StockId, int IsActive)
        {
            List<StockMasterDto> lstStockMasterDto = new List<StockMasterDto>();
            SqlCommand cmd = new SqlCommand("GetAndEditStockMaster", connectionRepository.con);
            cmd.Parameters.AddWithValue("@StockId", StockId);
            cmd.Parameters.AddWithValue("@IsActive", IsActive);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                StockMasterDto stockMasterDto = new StockMasterDto();
                stockMasterDto.StockId = Convert.ToInt32(dr["StockId"]);
                stockMasterDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                stockMasterDto.GroupName = Convert.ToString(dr["GroupName"]);
                stockMasterDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                stockMasterDto.CategoryName = Convert.ToString(dr["CategoryName"]);
                stockMasterDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                stockMasterDto.ItemName = Convert.ToString(dr["ItemName"]);
                stockMasterDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                stockMasterDto.Quantity = Convert.ToInt32(dr["Quantity"]);
                stockMasterDto.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                stockMasterDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                stockMasterDto.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"] != DBNull.Value ? dr["ModifiedBy"] : 0);
                stockMasterDto.ModifiedDate = Convert.ToString(dr["ModifiedDate"] != DBNull.Value ? dr["ModifiedDate"] : "");
                stockMasterDto.IsActive = Convert.ToInt32(dr["IsActive"]);
                lstStockMasterDto.Add(stockMasterDto);
            }
            connectionRepository.con.Close();
            return lstStockMasterDto;
        }
        public int DeleteStockMaster(int StockId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteStockMaster", connectionRepository.con);
                cmd.Parameters.AddWithValue("@StockId", StockId);
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
