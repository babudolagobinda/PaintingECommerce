using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryECommerce.Model.UserDTO;
using ArtGalleryECommerce.Dal.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

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
                productListDto.Price = CurrencyConverter(Convert.ToDecimal(dr["Price"]));
                productListDto.Height = Convert.ToDecimal(dr["Height"]);
                productListDto.HeightType = Convert.ToString(dr["HeightType"]);
                productListDto.Width = Convert.ToDecimal(dr["Width"]);
                productListDto.WidthType = Convert.ToString(dr["WidthType"]);
                if (dr["Quantity"] != DBNull.Value)
                {
                    productListDto.Quantity = Convert.ToInt32(dr["Quantity"]);
                }
                else
                {
                    productListDto.Quantity = Convert.ToInt32(0);
                }

                lstProductListDto.Add(productListDto);
            }
            connectionRepository.con.Close();
            return lstProductListDto;
        }
        public List<ProductListDto> GetBottomProductList()
        {
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            SqlCommand cmd = new SqlCommand("GetBottomProductList", connectionRepository.con);
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
                productListDto.Price = CurrencyConverter(Convert.ToDecimal(dr["Price"]));
                productListDto.Height = Convert.ToDecimal(dr["Height"]);
                productListDto.HeightType = Convert.ToString(dr["HeightType"]);
                productListDto.Width = Convert.ToDecimal(dr["Width"]);
                productListDto.WidthType = Convert.ToString(dr["WidthType"]);
                if (dr["Quantity"] != DBNull.Value)
                {
                    productListDto.Quantity = Convert.ToInt32(dr["Quantity"]);
                }
                else
                {
                    productListDto.Quantity = Convert.ToInt32(0);
                }
                lstProductListDto.Add(productListDto);
            }
            connectionRepository.con.Close();
            return lstProductListDto;
        }
        public decimal CurrencyConverter(decimal price)
        {
            double amount = 0;
            decimal converted_amount = 0;
            string currencyValue = "";
            string prevCurrencyValue = "";
            if (HttpContext.Current.Session["currency"] == null)
            {
                currencyValue = "INR";
            }
            else
            {
                currencyValue = HttpContext.Current.Session["currency"].ToString();
            }
            if (HttpContext.Current.Session["PrevCurrency"] == null)
            {
                prevCurrencyValue = "INR";
            }
            else
            {
                prevCurrencyValue = HttpContext.Current.Session["PrevCurrency"].ToString();
            }
            DateTime dateTime = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            if (double.TryParse(Convert.ToString(price), out amount))
            {
                string  url = string.Format("http://currencyconverter.kowabunga.net/converter.asmx/GetConversionAmount?CurrencyFrom={0}&CurrencyTo={1}&RateDate=" + dateTime.Month + '-' + dateTime.Day + '-' + dateTime.Year + "&Amount={2}", prevCurrencyValue, currencyValue, price);


                WebClient client = new WebClient();
                client.Headers.Add("User-Agent: Other");   //that is the simple line!
                string rates = client.DownloadString(url);
                var final = Regex.Replace(rates, "<[^>]+>", string.Empty).Replace("\r\n", "");
                converted_amount = Math.Round(Convert.ToDecimal(final), 2);
            }
            return converted_amount;
        }
        public List<ProductListDto> GetProductListByGroupId(int GroupId)
        {
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            SqlCommand cmd = new SqlCommand("GetProductListByGroupId", connectionRepository.con);
            cmd.Parameters.AddWithValue("@GroupId", GroupId);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ProductListDto productListDto = new ProductListDto();
                productListDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                productListDto.GroupName = Convert.ToString(dr["GroupName"]);
                productListDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                productListDto.CategoryName = Convert.ToString(dr["CategoryName"]);
                productListDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                productListDto.ItemName = Convert.ToString(dr["ItemName"]);
                productListDto.ItemDesc = Convert.ToString(dr["ItemDesc"]);
                productListDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                productListDto.Mrp = Convert.ToDecimal(dr["Mrp"]);
                productListDto.Discount = Convert.ToDecimal(dr["Discount"]);
                productListDto.Price = CurrencyConverter(Convert.ToDecimal(dr["Price"]));
                productListDto.Height = Convert.ToDecimal(dr["Height"]);
                productListDto.HeightType = Convert.ToString(dr["HeightType"]);
                productListDto.Width = Convert.ToDecimal(dr["Width"]);
                productListDto.WidthType = Convert.ToString(dr["WidthType"]);
                if (dr["Quantity"] != DBNull.Value)
                {
                    productListDto.Quantity = Convert.ToInt32(dr["Quantity"]);
                }
                else
                {
                    productListDto.Quantity = Convert.ToInt32(0);
                }
                lstProductListDto.Add(productListDto);
            }
            connectionRepository.con.Close();
            return lstProductListDto;
        }
        public List<ProductListDto> GetProductListByCategoryId(int CategoryId)
        {
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            SqlCommand cmd = new SqlCommand("GetProductListByCategoryId", connectionRepository.con);
            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ProductListDto productListDto = new ProductListDto();
                productListDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                productListDto.GroupName = Convert.ToString(dr["GroupName"]);
                productListDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                productListDto.CategoryName = Convert.ToString(dr["CategoryName"]);
                productListDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                productListDto.ItemName = Convert.ToString(dr["ItemName"]);
                productListDto.ItemDesc = Convert.ToString(dr["ItemDesc"]);
                productListDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                productListDto.Mrp = Convert.ToDecimal(dr["Mrp"]);
                productListDto.Discount = Convert.ToDecimal(dr["Discount"]);
                productListDto.Price = CurrencyConverter(Convert.ToDecimal(dr["Price"]));
                productListDto.Height = Convert.ToDecimal(dr["Height"]);
                productListDto.HeightType = Convert.ToString(dr["HeightType"]);
                productListDto.Width = Convert.ToDecimal(dr["Width"]);
                productListDto.WidthType = Convert.ToString(dr["WidthType"]);
                if (dr["Quantity"] != DBNull.Value)
                {
                    productListDto.Quantity = Convert.ToInt32(dr["Quantity"]);
                }
                else
                {
                    productListDto.Quantity = Convert.ToInt32(0);
                }
                lstProductListDto.Add(productListDto);
            }
            connectionRepository.con.Close();
            return lstProductListDto;
        }
        public ProductListDto GetProductListByItemId(int ItemId)
        {
            ProductListDto productListDto = new ProductListDto();
            SqlCommand cmd = new SqlCommand("GetProductListByItemId", connectionRepository.con);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                productListDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                productListDto.GroupName = Convert.ToString(dr["GroupName"]);
                productListDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                productListDto.CategoryName = Convert.ToString(dr["CategoryName"]);
                productListDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                productListDto.ItemName = Convert.ToString(dr["ItemName"]);
                productListDto.ItemDesc = Convert.ToString(dr["ItemDesc"]);
                productListDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                productListDto.Mrp = Convert.ToDecimal(dr["Mrp"]);
                productListDto.Discount = Convert.ToDecimal(dr["Discount"]);
                productListDto.Price = CurrencyConverter(Convert.ToDecimal(dr["Price"]));
                productListDto.Height = Convert.ToDecimal(dr["Height"]);
                productListDto.HeightType = Convert.ToString(dr["HeightType"]);
                productListDto.Width = Convert.ToDecimal(dr["Width"]);
                productListDto.WidthType = Convert.ToString(dr["WidthType"]);
                if (dr["Quantity"] != DBNull.Value)
                {
                    productListDto.Quantity = Convert.ToInt32(dr["Quantity"]);
                }
                else
                {
                    productListDto.Quantity = Convert.ToInt32(0);
                }

            }
            connectionRepository.con.Close();
            return productListDto;
        }
    }
}
