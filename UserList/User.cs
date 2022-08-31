namespace WF.Model
{ 
    [Serializable]
    public class Users
    {
        public struct User
        {
            public string Name { get; set; }
            public string Sex { get; set; }
            public int Age { get; set; }
        }
        public List<User> UserList { get; set; } = new List<User>();
    }
}
