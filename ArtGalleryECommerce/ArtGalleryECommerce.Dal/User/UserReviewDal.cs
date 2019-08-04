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
    public class UserReviewDal
    {
        ConnectionRepository connectionRepository = new ConnectionRepository();
        public int SaveUsersReview(UserReviewDto userReviewDto)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SaveUsersReview", connectionRepository.con);
                cmd.Parameters.AddWithValue("@UserId", userReviewDto.UserId);
                cmd.Parameters.AddWithValue("@ItemId", userReviewDto.ItemId);
                cmd.Parameters.AddWithValue("@Review", userReviewDto.Review);
                cmd.Parameters.AddWithValue("@Rating", userReviewDto.Rating);
                cmd.Parameters.AddWithValue("@IsActive", userReviewDto.IsActive);
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
        public List<UserReviewDto> GetAllReviewList(int ItemId)
        {
            List<UserReviewDto> lstUserReviewDto = new List<UserReviewDto>();
            SqlCommand cmd = new SqlCommand("GetAllReviewList", connectionRepository.con);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UserReviewDto userReviewDto = new UserReviewDto();
                userReviewDto.RatingId = Convert.ToInt32(dr["RatingId"]);
                userReviewDto.UserId = Convert.ToInt32(dr["UserId"]);
                userReviewDto.Name = Convert.ToString(dr["Name"]);
                userReviewDto.Review = Convert.ToString(dr["Review"]);
                userReviewDto.Rating = Convert.ToString(dr["Rating"]);
                userReviewDto.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                lstUserReviewDto.Add(userReviewDto);
            }
            connectionRepository.con.Close();
            return lstUserReviewDto;
        }
        public int GetAverageRatingPerItem(int ItemId)
        {
            SqlCommand cmd = new SqlCommand("GetAverageRatingPerItem", connectionRepository.con);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int avgRating = 0;
            while (dr.Read())
            {
                if (dr["AverageRating"] != DBNull.Value)
                {
                    avgRating = Convert.ToInt32(dr["AverageRating"]);
                }
                else
                {
                    avgRating = Convert.ToInt32(0);
                }
            }
            connectionRepository.con.Close();
            return avgRating;
        }
        public int GetTotalCountRatingPerItem(int ItemId)
        {
            SqlCommand cmd = new SqlCommand("GetTotalCountRatingPerItem", connectionRepository.con);
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.CommandType = CommandType.StoredProcedure;
            connectionRepository.con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            int totalRating = 0;
            while (dr.Read())
            {
                if (dr["CountRating"] != DBNull.Value)
                {
                    totalRating = Convert.ToInt32(dr["CountRating"]);
                }
                else
                {
                    totalRating = Convert.ToInt32(0);
                }
            }
            connectionRepository.con.Close();
            return totalRating;
        }
    }
}
