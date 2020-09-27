namespace Eyelove
{
    public class Users
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public int Search_age1 { get; set; }
        public int Search_age2 { get; set; }
        public string Search_sex { get; set; }

        public Users()
        {
        }

        public Users(string name, int age, string comments)
        {
            Name = name;
            Age = age;
            Comments = comments;
        }
        public Users(string name, string sex, int age, string location, string email, string comments)
        {
            Name = name;
            Sex = sex;
            Age = age;
            Location = location;
            Email = email;
            Comments = comments;
        }
        public Users(string password, int search_age1, int search_age2, string search_sex)
        {
            Password = password;
            Search_age1 = search_age1;
            Search_age2 = search_age2;
            Search_sex = search_sex;
        }
        public Users(int id, string sex, int age, string location, string search_sex, int search_age1, int search_age2)
        {
            ID = id;
            Sex = sex;
            Age = age;
            Location = location;
            Search_age1 = search_age1;
            Search_age2 = search_age2;
            Search_sex = search_sex;
        }
        public Users(int id, string name, int age, string comments)
        {
            ID = id;
            Name = name;
            Age = age;
            Comments = comments;
        }

        public Users(int id)
        {
            ID = id;
        }
    }
}
