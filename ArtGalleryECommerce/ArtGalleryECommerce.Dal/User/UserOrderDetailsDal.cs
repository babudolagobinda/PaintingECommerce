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
    public class UserOrderDetailsDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();

        public int SaveOrderDetails(UserOrderDetailsDto userOrderDetailsDto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveOrderDetails", connectionRepository.con);
                cmd.Parameters.AddWithValue("@OrderNumber", userOrderDetailsDto.OrderNumber);
                cmd.Parameters.AddWithValue("@UserId", userOrderDetailsDto.UserId);
                cmd.Parameters.AddWithValue("@ItemId", userOrderDetailsDto.ItemId);
                cmd.Parameters.AddWithValue("@AddressId", userOrderDetailsDto.AddressId);
                cmd.Parameters.AddWithValue("@Mrp", userOrderDetailsDto.Mrp);
                cmd.Parameters.AddWithValue("@Discount", userOrderDetailsDto.Discount);
                cmd.Parameters.AddWithValue("@Price", userOrderDetailsDto.Price);
                cmd.Parameters.AddWithValue("@CurrencyType", userOrderDetailsDto.CurrencyType);
                cmd.Parameters.AddWithValue("@Quantity", userOrderDetailsDto.Quantity);
                cmd.Parameters.AddWithValue("@TotalAmount", userOrderDetailsDto.TotalAmount);
                cmd.Parameters.AddWithValue("@PaymentType", userOrderDetailsDto.PaymentType);
                cmd.Parameters.AddWithValue("@Status", userOrderDetailsDto.Status);
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
        public List<UserOrderDetailsDto> GetAllOrderTable()
        {
            List<UserOrderDetailsDto> lstUserOrderDetailsDto = new List<UserOrderDetailsDto>();
            SqlCommand cmd = new SqlCommand("select * from [dbo].[OrderTable] order by Id  desc", connectionRepository.con);
            cmd.CommandType = CommandType.Text;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserOrderDetailsDto userOrderDetailsDto = new UserOrderDetailsDto();
                userOrderDetailsDto.Id = Convert.ToInt32(dr["Id"]);
                userOrderDetailsDto.OrderNumber = Convert.ToString(dr["OrderNumber"]);
                userOrderDetailsDto.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                userOrderDetailsDto.Status = Convert.ToInt32(dr["Status"]);
                lstUserOrderDetailsDto.Add(userOrderDetailsDto);
            }
            connectionRepository.con.Close();
            return lstUserOrderDetailsDto;
        }
        public List<UserOrderAndAddressDto> GetUserAddressDetails(string OrderNumber)
        {
            List<UserOrderAndAddressDto> lstUserOrderAndAddressDto = new List<UserOrderAndAddressDto>();
            SqlCommand cmd = new SqlCommand("GetUserAddressDetails", connectionRepository.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNumber", OrderNumber);
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserOrderAndAddressDto userOrderDetailsDto = new UserOrderAndAddressDto();
                userOrderDetailsDto.OrderNumber = Convert.ToString(dr["OrderNumber"]);
                userOrderDetailsDto.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
                userOrderDetailsDto.PaymentType= Convert.ToString(dr["PaymentType"]);
                userOrderDetailsDto.Status = Convert.ToInt32(dr["Status"]);
                userOrderDetailsDto.UserId = Convert.ToInt32(dr["UserId"]);
                userOrderDetailsDto.Name = Convert.ToString(dr["Name"]);
                userOrderDetailsDto.AddressId = Convert.ToInt32(dr["AddressId"]);
                userOrderDetailsDto.MobileNo = Convert.ToString(dr["MobileNo"]);
                userOrderDetailsDto.Pincode = Convert.ToString(dr["Pincode"]);
                userOrderDetailsDto.Address = Convert.ToString(dr["Address"]);
                userOrderDetailsDto.Locality = Convert.ToString(dr["Locality"]);
                userOrderDetailsDto.City = Convert.ToString(dr["City"]);
                userOrderDetailsDto.State = Convert.ToString(dr["State"]);
                userOrderDetailsDto.Country = Convert.ToString(dr["Country"]);
                lstUserOrderAndAddressDto.Add(userOrderDetailsDto);
            }
            connectionRepository.con.Close();
            return lstUserOrderAndAddressDto;
        }
        public List<UserOrderDetailsResponseDto> GetAllOrderDetails(string OrderNumber)
        {
            List<UserOrderDetailsResponseDto> lstUserOrderDetailsResponseDto = new List<UserOrderDetailsResponseDto>();
            SqlCommand cmd = new SqlCommand("GetUserOrderByOrderNumber", connectionRepository.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNumber", OrderNumber);
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserOrderDetailsResponseDto userOrderDetailsResponseDto = new UserOrderDetailsResponseDto();
                userOrderDetailsResponseDto.OrderNumber = Convert.ToString(dr["OrderNumber"]);
                userOrderDetailsResponseDto.ItemName = Convert.ToString(dr["ItemName"]);
                userOrderDetailsResponseDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                userOrderDetailsResponseDto.Price = Convert.ToDecimal(dr["Price"]);
                userOrderDetailsResponseDto.Quantity = Convert.ToInt32(dr["Quantity"]);
                userOrderDetailsResponseDto.EmailId = Convert.ToString(dr["EmailId"]);
                userOrderDetailsResponseDto.Name = Convert.ToString(dr["Name"]);
                lstUserOrderDetailsResponseDto.Add(userOrderDetailsResponseDto);
            }
            connectionRepository.con.Close();
            return lstUserOrderDetailsResponseDto;
        }
        public List<MyOrdersDetailsResponseDto> GetOrderHistoryByUserId(int UserId)
        {
            List<MyOrdersDetailsResponseDto> lstMyOrdersDetailsResponseDto = new List<MyOrdersDetailsResponseDto>();
            SqlCommand cmd = new SqlCommand("GetOrderHistoryByUserId", connectionRepository.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MyOrdersDetailsResponseDto myOrdersDetailsResponseDto = new MyOrdersDetailsResponseDto();
                myOrdersDetailsResponseDto.OrderNumber = Convert.ToString(dr["OrderNumber"]);
                lstMyOrdersDetailsResponseDto.Add(myOrdersDetailsResponseDto);
            }
            connectionRepository.con.Close();
            return lstMyOrdersDetailsResponseDto;
        }
        public List<MyOrdersDetailsResponseDto> GetAllOrderDetailsByOrderNo(int UserId,string OrderNo)
        {
            List<MyOrdersDetailsResponseDto> lstMyOrdersDetailsResponseDto = new List<MyOrdersDetailsResponseDto>();
            SqlCommand cmd = new SqlCommand("GetAllOrderDetailsByOrderNo", connectionRepository.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@OrderNo", OrderNo);
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MyOrdersDetailsResponseDto myOrdersDetailsResponseDto = new MyOrdersDetailsResponseDto();
                myOrdersDetailsResponseDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                myOrdersDetailsResponseDto.GroupName = Convert.ToString(dr["GroupName"]);
                myOrdersDetailsResponseDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                myOrdersDetailsResponseDto.CategoryName = Convert.ToString(dr["CategoryName"]);
                myOrdersDetailsResponseDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                myOrdersDetailsResponseDto.ItemName = Convert.ToString(dr["ItemName"]);
                myOrdersDetailsResponseDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                myOrdersDetailsResponseDto.Mrp = Convert.ToDecimal(dr["Mrp"]);
                myOrdersDetailsResponseDto.Discount = Convert.ToDecimal(dr["Discount"]);
                myOrdersDetailsResponseDto.Price = Convert.ToDecimal(dr["Price"]);
                myOrdersDetailsResponseDto.Width = Convert.ToInt32(dr["Width"]);
                myOrdersDetailsResponseDto.WidthType = Convert.ToString(dr["WidthType"]);
                myOrdersDetailsResponseDto.Height = Convert.ToInt32(dr["Height"]);
                myOrdersDetailsResponseDto.HeightType = Convert.ToString(dr["HeightType"]);
                myOrdersDetailsResponseDto.CurrencyType = Convert.ToString(dr["CurrencyType"]);
                myOrdersDetailsResponseDto.Quantity = Convert.ToInt32(dr["Quantity"]);
                myOrdersDetailsResponseDto.Status = Convert.ToInt32(dr["Status"]);
                myOrdersDetailsResponseDto.OrderId = Convert.ToInt32(dr["OrderId"]);
                myOrdersDetailsResponseDto.OrderNumber = Convert.ToString(dr["OrderNumber"]);
                myOrdersDetailsResponseDto.OrderDate = Convert.ToString(dr["OrderDate"]);
                myOrdersDetailsResponseDto.PaymentDate = Convert.ToString(dr["PaymentDate"]);
                myOrdersDetailsResponseDto.PaymentType = Convert.ToString(dr["PaymentType"]);
                myOrdersDetailsResponseDto.UserId = Convert.ToInt32(dr["UserId"]);
                myOrdersDetailsResponseDto.Name = Convert.ToString(dr["Name"]);
                myOrdersDetailsResponseDto.EmailId = Convert.ToString(dr["EmailId"]);
                myOrdersDetailsResponseDto.MobileNo = Convert.ToString(dr["MobileNo"]);
                lstMyOrdersDetailsResponseDto.Add(myOrdersDetailsResponseDto);
            }
            connectionRepository.con.Close();
            return lstMyOrdersDetailsResponseDto;
        }
        public List<MyOrdersDetailsResponseDto> GetAllUserOrderDetails(string OrderNumber)
        {
            List<MyOrdersDetailsResponseDto> lstMyOrdersDetailsResponseDto = new List<MyOrdersDetailsResponseDto>();
            SqlCommand cmd = new SqlCommand("GetAllUserOrderDetails", connectionRepository.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", OrderNumber);
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MyOrdersDetailsResponseDto myOrdersDetailsResponseDto = new MyOrdersDetailsResponseDto();
                myOrdersDetailsResponseDto.GroupId = Convert.ToInt32(dr["GroupId"]);
                myOrdersDetailsResponseDto.GroupName = Convert.ToString(dr["GroupName"]);
                myOrdersDetailsResponseDto.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                myOrdersDetailsResponseDto.CategoryName = Convert.ToString(dr["CategoryName"]);
                myOrdersDetailsResponseDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                myOrdersDetailsResponseDto.ItemName = Convert.ToString(dr["ItemName"]);
                myOrdersDetailsResponseDto.ItemImage = Convert.ToString(dr["ItemImage"]);
                myOrdersDetailsResponseDto.Mrp = Convert.ToDecimal(dr["Mrp"]);
                myOrdersDetailsResponseDto.Discount = Convert.ToDecimal(dr["Discount"]);
                myOrdersDetailsResponseDto.Price = Convert.ToDecimal(dr["Price"]);
                myOrdersDetailsResponseDto.Width = Convert.ToInt32(dr["Width"]);
                myOrdersDetailsResponseDto.WidthType = Convert.ToString(dr["WidthType"]);
                myOrdersDetailsResponseDto.Height = Convert.ToInt32(dr["Height"]);
                myOrdersDetailsResponseDto.HeightType = Convert.ToString(dr["HeightType"]);
                myOrdersDetailsResponseDto.CurrencyType = Convert.ToString(dr["CurrencyType"]);
                myOrdersDetailsResponseDto.Quantity = Convert.ToInt32(dr["Quantity"]);
                myOrdersDetailsResponseDto.Status = Convert.ToInt32(dr["Status"]);
                myOrdersDetailsResponseDto.OrderId = Convert.ToInt32(dr["OrderId"]);
                myOrdersDetailsResponseDto.OrderNumber = Convert.ToString(dr["OrderNumber"]);
                myOrdersDetailsResponseDto.OrderDate = Convert.ToString(dr["OrderDate"]);
                myOrdersDetailsResponseDto.PaymentDate = Convert.ToString(dr["PaymentDate"]);
                myOrdersDetailsResponseDto.PaymentType = Convert.ToString(dr["PaymentType"]);
                myOrdersDetailsResponseDto.UserId = Convert.ToInt32(dr["UserId"]);
                myOrdersDetailsResponseDto.Name = Convert.ToString(dr["Name"]);
                myOrdersDetailsResponseDto.EmailId = Convert.ToString(dr["EmailId"]);
                myOrdersDetailsResponseDto.MobileNo = Convert.ToString(dr["MobileNo"]);
                myOrdersDetailsResponseDto.AddressId = Convert.ToInt32(dr["AddressId"]);
                lstMyOrdersDetailsResponseDto.Add(myOrdersDetailsResponseDto);
            }
            connectionRepository.con.Close();
            return lstMyOrdersDetailsResponseDto;
        }
        public int ApproveAndDeclineOrderDetails(string OrderNumber,int Status)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("ApproveAndDeclineOrderDetails", connectionRepository.con);
                cmd.Parameters.AddWithValue("@OrderNumber", OrderNumber);
                cmd.Parameters.AddWithValue("@Status", Status);
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
