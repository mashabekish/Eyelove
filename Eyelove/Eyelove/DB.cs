using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Eyelove
{
    public static class DB
    {
        public static string ph;
        public static byte[] data;
        static readonly string connString = @"Data Source = LAPTOP-1DEPT1LL\SQLEXPRESS; Initial Catalog = Eyelove; User Id = eyelove; Password = eyelove; Connect Timeout = 60";
        public static string Login(string login)
        {
            string sql = "SELECT Password FROM Users WHERE username = @l";
            string p;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", login);
                cmd.Parameters.Add(lp);
                p = (string)cmd.ExecuteScalar();
                conn.Close();
            }
            return p;
        }

        public static bool Registration(string l)
        {
            bool fl = false;
            string sql = "SELECT COUNT(*) FROM Users WHERE username = @l";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                cmd.Parameters.Add(lp);
                if ((int)cmd.ExecuteScalar() == 0)
                    fl = true;
                conn.Close();
            }
            return fl;
        }

        public static bool ProfileReg(string l, string p, string sname, string ssex, int sage, string slocation, string semail, string scomments, int ssearch_age1, int ssearch_age2, string ssearch_sex)
        {
            bool fl = false;
            string sql = "INSERT INTO Users (username, password, name, sex, age, location, email, photo, comments, search_age1, search_age2, search_sex) VALUES (@l, @p, @sname, @ssex, @sage, @slocation, @semail, @ImageData, @scomments, @ssearch_age1, @ssearch_age2, @ssearch_sex)";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                SqlParameter pp = new SqlParameter("@p", p);
                SqlParameter snamep = new SqlParameter("@sname", sname);
                SqlParameter ssexp = new SqlParameter("@ssex", ssex);
                SqlParameter sagep = new SqlParameter("@sage", sage);
                SqlParameter slocationp = new SqlParameter("@slocation", slocation);
                SqlParameter semailp = new SqlParameter("@semail", semail);
                SqlParameter scommentsp = new SqlParameter("@scomments", scomments);
                SqlParameter ssearch_age1p = new SqlParameter("@ssearch_age1", ssearch_age1);
                SqlParameter ssearch_age2p = new SqlParameter("@ssearch_age2", ssearch_age2);
                SqlParameter ssearch_sexp = new SqlParameter("@ssearch_sex", ssearch_sex);
                cmd.Parameters.Add(lp);
                cmd.Parameters.Add(pp);
                cmd.Parameters.Add(snamep);
                cmd.Parameters.Add(ssexp);
                cmd.Parameters.Add(sagep);
                cmd.Parameters.Add(slocationp);
                cmd.Parameters.Add(semailp);

                cmd.Parameters.Add("@ImageData", SqlDbType.Image, 1000000);
                string filename = ph;
                byte[] imageData;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                cmd.Parameters["@ImageData"].Value = imageData;

                cmd.Parameters.Add(scommentsp);
                cmd.Parameters.Add(ssearch_age1p);
                cmd.Parameters.Add(ssearch_age2p);
                cmd.Parameters.Add(ssearch_sexp);
                if (cmd.ExecuteNonQuery() == 1)
                    fl = true;
                conn.Close();
            }
            return fl;
        }

        public static List<Users> Description(string l)
        {
            List<Users> users = new List<Users>();
            string sql = "SELECT name, age, comments FROM Users WHERE username = @l";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                cmd.Parameters.Add(lp);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    int age = reader.GetInt32(1);
                    string comments = reader.GetString(2);
                    Users user = new Users(name, age, comments);
                    users.Add(user);
                }
                conn.Close();
            }
            return users;
        }

        public static List<Users> Profile(string l)
        {
            List<Users> users = new List<Users>();
            string sql = "SELECT name, sex, age, location, email, comments FROM Users WHERE username = @l";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                cmd.Parameters.Add(lp);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string sex = reader.GetString(1);
                    int age = reader.GetInt32(2);
                    string location = reader.GetString(3);
                    string email = reader.GetString(4);
                    string comments = reader.GetString(5);
                    Users user = new Users(name, sex, age, location, email, comments);
                    users.Add(user);
                }
                conn.Close();
            }
            return users;
        }

        public static bool ProfileSave(string l, string sname, string ssex, int sage, string slocation, string semail, string scomments)
        {
            bool fl = false;
            string sql = "UPDATE Users SET name = @sname, sex = @ssex, age = @sage, location = @slocation, email = @semail, photo = @ImageData, comments = @scomments WHERE username = @l";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                SqlParameter snamep = new SqlParameter("@sname", sname);
                SqlParameter ssexp = new SqlParameter("@ssex", ssex);
                SqlParameter sagep = new SqlParameter("@sage", sage);
                SqlParameter slocationp = new SqlParameter("@slocation", slocation);
                SqlParameter semailp = new SqlParameter("@semail", semail);
                SqlParameter scommentsp = new SqlParameter("@scomments", scomments);
                cmd.Parameters.Add(lp);
                cmd.Parameters.Add(snamep);
                cmd.Parameters.Add(ssexp);
                cmd.Parameters.Add(sagep);
                cmd.Parameters.Add(slocationp);
                cmd.Parameters.Add(semailp);

                cmd.Parameters.Add("@ImageData", SqlDbType.Image, 1000000);
                string filename = ph;
                byte[] imageData;
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                cmd.Parameters["@ImageData"].Value = imageData;

                cmd.Parameters.Add(scommentsp);
                if (cmd.ExecuteNonQuery() == 1)
                    fl = true;
                conn.Close();
            }
            return fl;
        }

        public static List<Users> Setting(string l)
        {
            List<Users> users = new List<Users>();
            string sql = "SELECT password, search_age1, search_age2, search_sex FROM Users WHERE username = @l";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                cmd.Parameters.Add(lp);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string password = reader.GetString(0);
                    int search_age1 = reader.GetInt32(1);
                    int search_age2 = reader.GetInt32(2);
                    string search_sex = reader.GetString(3);
                    Users user = new Users(password, search_age1, search_age2, search_sex);
                    users.Add(user);
                }
                conn.Close();
            }
            return users;
        }

        public static bool SettingSave(string l, int ssearch_age1, int ssearch_age2, string ssearch_sex)
        {
            bool fl = false;
            string sql = "UPDATE Users SET search_age1 = @ssearch_age1, search_age2 = @ssearch_age2, search_sex = @ssearch_sex WHERE username = @l";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                SqlParameter ssearch_age1p = new SqlParameter("@ssearch_age1", ssearch_age1);
                SqlParameter ssearch_age2p = new SqlParameter("@ssearch_age2", ssearch_age2);
                SqlParameter ssearch_sexp = new SqlParameter("@ssearch_sex", ssearch_sex);
                cmd.Parameters.Add(lp);
                cmd.Parameters.Add(ssearch_age1p);
                cmd.Parameters.Add(ssearch_age2p);
                cmd.Parameters.Add(ssearch_sexp);
                if (cmd.ExecuteNonQuery() == 1)
                    fl = true;
                conn.Close();
            }
            return fl;
        }

        public static bool Password(string l, string p)
        {
            bool fl = false;
            string sql = "UPDATE Users SET password = @spassword WHERE username = @l";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                SqlParameter pp = new SqlParameter("@spassword", p);
                cmd.Parameters.Add(lp);
                cmd.Parameters.Add(pp);
                if (cmd.ExecuteNonQuery() == 1)
                    fl = true;
                conn.Close();
            }
            return fl;
        }

        public static bool Delete(string l)
        {
            bool fl = false;
            string sql = "DELETE Users WHERE username = @l";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                cmd.Parameters.Add(lp);
                if (cmd.ExecuteNonQuery() == 1)
                    fl = true;
                conn.Close();
            }
            return fl;
        }

        public static List<Users> Search(string login)
        {
            List<Users> users = new List<Users>();
            string sql = "SELECT id, sex, age, location, search_age1, search_age2, search_sex FROM Users WHERE username = @lp";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter l = new SqlParameter("@lp", login);
                cmd.Parameters.Add(l);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string sex = reader.GetString(1);
                    int age = reader.GetInt32(2);
                    string location = reader.GetString(3);
                    int search_age1 = reader.GetInt32(4);
                    int search_age2 = reader.GetInt32(5);
                    string search_sex = reader.GetString(6);
                    Users user = new Users(id, sex, age, location, search_sex, search_age1, search_age2);
                    users.Add(user);
                }
                conn.Close();
            }
            List<Users> u = new List<Users>();
            sql = "SELECT ID, Name, Age, Photo, Comments " +
                        "FROM Users " +
                        "WHERE ID = " +
                        "(SELECT TOP(1) ID " +
                        "FROM Users " +
                        "WHERE Age BETWEEN @sa1 AND @sa2 " +
                        "AND Sex = @ss " +
                        "AND ID <> @id " +
                        "AND Location = @loc " +
                        "AND @age BETWEEN Search_age1 AND Search_age2 " +
                        "AND Search_sex = @sex " +
                        "AND NOT EXISTS " +
                        "(SELECT * " +
                        "FROM Search " +
                        "WHERE ID_1 = @id AND ID_2 = ID)" +
                        ")";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter id = new SqlParameter("@id", users[0].ID);
                cmd.Parameters.Add(id);
                SqlParameter loc = new SqlParameter("@loc", users[0].Location);
                cmd.Parameters.Add(loc);
                SqlParameter sa1 = new SqlParameter("@sa1", users[0].Search_age1);
                cmd.Parameters.Add(sa1);
                SqlParameter sa2 = new SqlParameter("@sa2", users[0].Search_age2);
                cmd.Parameters.Add(sa2);
                SqlParameter ss = new SqlParameter("@ss", users[0].Search_sex);
                cmd.Parameters.Add(ss);
                SqlParameter a = new SqlParameter("@age", users[0].Age);
                cmd.Parameters.Add(a);
                SqlParameter s = new SqlParameter("@sex", users[0].Sex);
                cmd.Parameters.Add(s);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int i = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int age = reader.GetInt32(2);
                        byte[] photo = (byte[])reader.GetValue(3);
                        string comments = reader.GetString(4);
                        Users us = new Users(i, name, age, comments);
                        u.Add(us);
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        string phh = Path.Combine(path, i + "photo.jpg");
                        using (FileStream fs = new FileStream(phh, FileMode.OpenOrCreate))
                        {
                            fs.Write(photo, 0, photo.Length);
                        }
                    }
                }
                else
                {
                    int i = 0;
                    Users us = new Users(i);
                    u.Add(us);
                }
                conn.Close();
            }
            return u;
        }
        public static void Dislike(string l, int id_2)
        {
            string sql = "SELECT ID FROM Users WHERE username = @l";
            int id_1;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                cmd.Parameters.Add(lp);
                id_1 = (int)cmd.ExecuteScalar();
                conn.Close();
            }
            sql = "INSERT INTO Search VALUES (@id_1, @id_2, @st, ' ')";
            string status = "dislike";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@id_1", id_1);
                SqlParameter pp = new SqlParameter("@id_2", id_2);
                SqlParameter snamep = new SqlParameter("@st", status);
                cmd.Parameters.Add(lp);
                cmd.Parameters.Add(pp);
                cmd.Parameters.Add(snamep);
                cmd.ExecuteNonQuery();
            }
        }
        public static void Like(string l, int id_2)
        {
            string sql = "SELECT ID FROM Users WHERE username = @l";
            int id_1;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@l", l);
                cmd.Parameters.Add(lp);
                id_1 = (int)cmd.ExecuteScalar();
                conn.Close();
            }
            sql = "SELECT COUNT(*) FROM Search WHERE ID_1 = @id_2 AND ID_2 = @id_1 AND Status = 'like'";
            string back = "N";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@id_1", id_1);
                SqlParameter pp = new SqlParameter("@id_2", id_2);
                cmd.Parameters.Add(lp);
                cmd.Parameters.Add(pp);
                if ((int)cmd.ExecuteScalar() != 0)
                    back = "Y";
                conn.Close();
            }
            sql = "INSERT INTO Search VALUES (@id_1, @id_2, @st, @b)";
            string status = "like";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter lp = new SqlParameter("@id_1", id_1);
                SqlParameter pp = new SqlParameter("@id_2", id_2);
                SqlParameter snamep = new SqlParameter("@st", status);
                SqlParameter s = new SqlParameter("@b", back);
                cmd.Parameters.Add(lp);
                cmd.Parameters.Add(pp);
                cmd.Parameters.Add(snamep);
                cmd.Parameters.Add(s);
                cmd.ExecuteNonQuery();
            }
            if (back == "Y")
            {
                sql = "UPDATE Search SET Back = @b WHERE ID_1 = @id_2 AND ID_2 = @id_1";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlParameter lp = new SqlParameter("@id_1", id_1);
                    SqlParameter pp = new SqlParameter("@id_2", id_2);
                    SqlParameter s = new SqlParameter("@b", back);
                    cmd.Parameters.Add(lp);
                    cmd.Parameters.Add(pp);
                    cmd.Parameters.Add(s);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public static List<Search> Dialog(string l)
        {
            List<Search> users = new List<Search>();
            string sql = "SELECT ID_2 FROM Search WHERE Back = 'Y' " +
                "AND ID_1 = " +
                "(SELECT ID " +
                "FROM Users " +
                "WHERE Username = @lp)";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter ll = new SqlParameter("@lp", l);
                cmd.Parameters.Add(ll);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    Search user = new Search(id);
                    users.Add(user);
                }
                conn.Close();
            }
            return users;
        }

        public static Users Info(int id)
        {
            Users us = new Users();
            string sql = "SELECT Name, Age, Photo FROM Users WHERE ID = @id";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlParameter i = new SqlParameter("@id", id);
                cmd.Parameters.Add(i);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    int age = reader.GetInt32(1);
                    byte[] photo = (byte[])reader.GetValue(3);
                    us.Name = name;
                    us.Age = age;
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    string phh = Path.Combine(path, i + "photo.jpg");
                    using (FileStream fs = new FileStream(phh, FileMode.OpenOrCreate))
                    {
                        fs.Write(photo, 0, photo.Length);
                    }
                }
                conn.Close();
            }
            return us;
        }
    }
}