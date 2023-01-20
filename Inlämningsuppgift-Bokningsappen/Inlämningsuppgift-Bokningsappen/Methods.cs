using Inlämningsuppgift_Bokningsappen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Inlämningsuppgift_Bokningsappen
{
    public class Methods
    {
        //Enum för Meny
        enum Menu
        {
            Boka_Rum = 1,
            Avboka_Rum,
            Visa_Bokningschema,
            Admin
        }
        //Enum för Admin Meny
        enum AdminMenu
        {
            Lägg_Till_Rum = 1,
            Lägg_Till_Gäst,
            Lägg_Till_Dag,
            Queries,
            Tillbaka
        }
        //Metod för Meny(-er)
        public static void ShowMenu()
        {
            while(true)
            {
                Console.WriteLine("Välkommen till Emmas Bed and Breakfast!" + "\n" +
                                  "Vänligen välj ett utav menyvalen nedan :)" + "\n");
                foreach (int i in Enum.GetValues(typeof(Menu)))
                {
                    Console.WriteLine($"{Enum.GetName(typeof(Menu), i).Replace('_', ' ')} = {i}");
                }

                Menu menu = (Menu)99;
                int nr;
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr))
                {
                    menu = (Menu)nr;
                    Console.Clear();
                }
                switch (menu)
                {
                    case Menu.Boka_Rum:
                        BookRoom();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case Menu.Avboka_Rum:
                        CancelBooking();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case Menu.Visa_Bokningschema:
                        ShowBookings();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case Menu.Admin:
                        foreach (int i in Enum.GetValues(typeof(AdminMenu)))
                        {
                            Console.WriteLine($"{Enum.GetName(typeof(AdminMenu), i).Replace('_', ' ')} = {i}");
                        }

                        AdminMenu adminMenu = (AdminMenu)99;
                        int nr2;
                        if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out nr2))
                        {
                            adminMenu = (AdminMenu)nr2;
                            Console.Clear();
                        }
                        switch(adminMenu)
                        {
                            case AdminMenu.Lägg_Till_Rum:
                                CreateRoom();
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case AdminMenu.Lägg_Till_Gäst:
                                CreateGuest();
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case AdminMenu.Lägg_Till_Dag:
                                CreateDay();
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case AdminMenu.Queries:
                                Queries();
                                Console.ReadKey();
                                Console.Clear();
                                break;
                            case AdminMenu.Tillbaka:
                                break;
                        }
                        break;
                }
            }
        }
        public static void Queries()
        {
            var db = new MyDbContext();

            Console.WriteLine("Välj en query: " + "\n" +
                              "1. Antal sängar per rum i ordning" + "\n" +
                              "2. Rum med och utan balkong" + "\n");

            var choise = int.Parse(Console.ReadLine());

            switch(choise)
            {
                case 1:
                    GetDapperData.Beds();
                    break;
                case 2:
                    GetDapperData.Balconies();
                    break;
            }
        }
        //Metod för Admin att lägga till rum i databasen
        public static void InsertRoom(string roomname,int beds,bool balcony,bool booked, int dayID)
        {
            using (var db = new MyDbContext())
            {
                var newRoom = new Room
                {
                    Name = roomname,
                    Beds = beds,
                    Balcony = balcony,
                    Booked = booked,
                    DayId = dayID
                };
                var RoomList = db.Rooms;
                RoomList.Add(newRoom);
                db.SaveChanges();
                Console.WriteLine("Nu har " + roomname + " skapats! :)");
            }
        }
        //Metod för Admin att skapa ett rum
        public static void CreateRoom()
        {
            Console.WriteLine("Rumsnamn: ");
            var roomname = Console.ReadLine();
            Console.WriteLine("Antal sängar: ");
            var beds = int.Parse(Console.ReadLine());
            Console.WriteLine("Balkong? (J/N)");
            var balcony = Console.ReadLine();
            Console.WriteLine("Ange dagID: ");
            var dayID = int.Parse(Console.ReadLine());
            bool hasBalcony = false;
            if(balcony == "J")
            {
                hasBalcony = true;
            }
            else if (balcony == "N")
            {
                hasBalcony = false;
            }
            var booked = false;

            InsertRoom(roomname,beds,hasBalcony,booked,dayID);
        }
        //Metod för Admin att lägga till en gäst i databasen
        public static void InsertGuest(string guestName)
        {
            using (var db = new MyDbContext())
            {
                var newGuest = new Guest
                {
                    Name = guestName
                };
                var GuestList = db.Guests;
                GuestList.Add(newGuest);
                db.SaveChanges();
                Console.WriteLine("Nu har " + guestName + " lagts in i databasen! :)-|-<");
            }
        }
        //Metod för Admin att skapa en gäst
        public static void CreateGuest()
        {
            Console.WriteLine("Namn: ");
            var guestname = Console.ReadLine();

            InsertGuest(guestname);
        }
        //Metod som visar en vy över bokningsschema
        public static void ShowBookings()
        {
            var myDb = new MyDbContext();
            foreach(var day in myDb.Days)
            {
                Console.WriteLine(day.DayName + "\n");
                Console.WriteLine("Hotellrum: " + "\t" + "Id: " + "\t" + "Sängar: " + "\t" + "Balkong: " + "\t" + "Bokad: " + "\t" + "GästID:");

                foreach(var room in myDb.Rooms)
                {
                    if(day.Id == room.DayId)
                    {
                        Console.WriteLine(room.Name + "\t" + room.Id + "\t" + room.Beds + "\t\t" + room.Balcony + "\t\t" + room.Booked + "\t" + room.GuestId);
                    }
                }
                Console.WriteLine("------------------------------------------------------------------");
            }
        }
        //Metod för Admin att skapa en dag
        public static void CreateDay()
        {
            Console.WriteLine("Dagnamn: ");
            var day = Console.ReadLine();

            InsertDay(day);
        }
        //Metod för Admin att lägga till en dag i databasen
        public static void InsertDay(string day)
        {
            using (var db = new MyDbContext())
            {
                var newDay = new Day
                {
                    DayName = day
                };
                var DayList = db.Days;
                DayList.Add(newDay);
                db.SaveChanges();
                Console.WriteLine("Nu har " + day + " lagts in i databasen! :)");
            }
        }
        //Metod som visar alla valbara gäster
        public static void ShowGuest()
        {
            var myDb = new MyDbContext();
            foreach(var guest in myDb.Guests)
            {
                Console.WriteLine("GästID: " + guest.Id + "\t" + "Namn: " + guest.Name);
            }
        }
        //Metod för att boka rum
        public static void BookRoom()
        {
            ShowBookings();

            Console.WriteLine("Vilket rum vill du boka? (Ange RumsID)");
            string roomId = Console.ReadLine();
            int id;
            if(int.TryParse(roomId, out id))
            {
                var myDB = new MyDbContext();
                var findRoom = (from r in myDB.Rooms
                                where r.Id == id
                                select r).SingleOrDefault();
                if(findRoom == null)
                {
                    Console.WriteLine("Rummet finns inte :(");
                }
                else if(findRoom != null && findRoom.Booked == false)
                {
                    ShowGuest();
                    Console.WriteLine("Vilken gäst vill du boka? (Ange GästID)");
                    int guestID = int.Parse(Console.ReadLine());
                    findRoom.Booked = true;
                    findRoom.GuestId = guestID;
                    myDB.SaveChanges();
                    Console.WriteLine("Bokningen lyckades! :D");
                }
                else if(findRoom != null && findRoom.Booked == true)
                {
                    Console.WriteLine("Rummet är redan bokat :'(");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Fel inmatning!");
            }
        }
        //Metod för att avboka rum
        public static void CancelBooking()
        {
            ShowBookings();

            Console.WriteLine("Vilket rum vill du avboka? (Ange RumsID)");
            string roomId = Console.ReadLine();
            int id;
            if (int.TryParse(roomId, out id))
                {
                var myDB = new MyDbContext();
                var findRoom = (from r in myDB.Rooms
                                where r.Id == id
                                select r).SingleOrDefault();
                if (findRoom == null)
                {
                    Console.WriteLine("Rummet finns inte :(");
                }
                else if (findRoom != null && findRoom.Booked == true)
                { 
                    findRoom.Booked = false;
                    findRoom.GuestId = null;
                    myDB.SaveChanges();
                    Console.WriteLine("Avbokningen lyckades! :D");
                }
                else if (findRoom != null && findRoom.Booked == false)
                {
                    Console.WriteLine("Rummet är redan tomt! :O");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Fel inmatning!");
            }
        }
    }
}
