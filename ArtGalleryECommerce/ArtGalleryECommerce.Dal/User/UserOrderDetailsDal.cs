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
        public List<UserOrderDetailsDto> GetAllOrderDetails(string OrderNumber)
        {
            List<UserOrderDetailsDto> lstUserOrderDetailsDto = new List<UserOrderDetailsDto>();
            SqlCommand cmd = new SqlCommand("select * from [dbo].[OrderDetails] where OrderNumber=@OrderNumber", connectionRepository.con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@OrderNumber", OrderNumber);
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserOrderDetailsDto userOrderDetailsDto = new UserOrderDetailsDto();
                userOrderDetailsDto.OrderId = Convert.ToInt32(dr["OrderId"]);
                userOrderDetailsDto.ItemId = Convert.ToInt32(dr["ItemId"]);
                userOrderDetailsDto.AddressId = Convert.ToInt32(dr["AddressId"]);
                userOrderDetailsDto.Mrp = Convert.ToDecimal(dr["Mrp"]);
                userOrderDetailsDto.Price = Convert.ToDecimal(dr["Price"]);
                userOrderDetailsDto.Status = Convert.ToInt32(dr["Status"]);
                lstUserOrderDetailsDto.Add(userOrderDetailsDto);
            }
            connectionRepository.con.Close();
            return lstUserOrderDetailsDto;
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
