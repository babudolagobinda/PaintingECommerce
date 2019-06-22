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
    public class UserHomeDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();

        public List<ProductListDto> GetTopProductList()
        {
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            SqlCommand cmd = new SqlCommand("GetTopProductList", connectionRepository.con);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ProductListDto productListDto = new ProductListDto();
                productListDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                productListDto.ItemName = Convert.ToString(dr["ItemName"]);
                productListDto.ItemDesc = Convert.ToString(dr["ItemDesc"]);
                productListDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                productListDto.Mrp = Convert.ToDecimal(dr["Mrp"]);
                productListDto.Discount = Convert.ToDecimal(dr["Discount"]);
                productListDto.Price = Convert.ToDecimal(dr["Price"]);
                productListDto.Height = Convert.ToDecimal(dr["Height"]);
                productListDto.HeightType = Convert.ToString(dr["HeightType"]);
                productListDto.Width = Convert.ToDecimal(dr["Width"]);
                productListDto.WidthType = Convert.ToString(dr["WidthType"]);
                lstProductListDto.Add(productListDto);
            }
            connectionRepository.con.Close();
            return lstProductListDto;
        }
    }
}
