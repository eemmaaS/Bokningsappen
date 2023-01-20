using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Inlämningsuppgift_Bokningsappen.Models;

namespace Inlämningsuppgift_Bokningsappen.Models
{
    public class GetDapperData
    {
        public static void Beds()
        {
            string connString = "Server=tcp:bokningsappenemma.database.windows.net,1433;Initial Catalog=BokningsappenEmma;Persist Security Info=False;User ID=BokningsappenEmma;Password=Pumpa123;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var sql = $"SELECT Name, Beds FROM Rooms GROUP BY Name,Beds";
            var roomBeds = new List<Room>();

            using(var connection = new SqlConnection(connString))
            {
                connection.Open();
                roomBeds = connection.Query<Room>(sql).ToList();
                connection.Close();
            }
            Console.WriteLine("Antal sängar per rum i ordning: " + "\n");
            foreach(var room in roomBeds)
            {
                Console.WriteLine("Hotellrum: " + room.Name + "\t" + "Antal sängar: " + room.Beds);
            }
        }
        public static void Balconies()
        {
            string connString = "Server=tcp:bokningsappenemma.database.windows.net,1433;Initial Catalog=BokningsappenEmma;Persist Security Info=False;User ID=BokningsappenEmma;Password=Pumpa123;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var sql = $"SELECT Name,Balcony FROM Rooms GROUP BY Name, Balcony ORDER BY Balcony desc";
            var roomBalconies = new List<Room>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                roomBalconies = connection.Query<Room>(sql).ToList();
                connection.Close();
            }
            Console.WriteLine("Rum med och utan balkong: " + "\n");
            foreach (var room in roomBalconies)
            {
                Console.WriteLine("Hotellrum: " + room.Name + "\t" + "Balkong: " + room.Balcony);
            }
        }
    }
}
