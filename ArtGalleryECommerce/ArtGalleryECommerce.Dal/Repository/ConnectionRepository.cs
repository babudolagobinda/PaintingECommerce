using System.Data;
using System.Data.SqlClient;

namespace ArtGalleryECommerce.Dal.Repository
{
    public  class ConnectionRepository
    {
        public SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
    }
}
