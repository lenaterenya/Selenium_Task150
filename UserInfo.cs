namespace Task_150_Final
{
    public class UserInfo
    {
        public UserInfo(string email, string password)
        {
            FirstName = "John";
            LastName = "Smith";
            _email = email;
            _password = password; 
            Address = "Krasnova str.P.O.Box.2";
            City = "NY";
            State = "Oregon";
            PostCode = "34534";
            Phone = "123234534";
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
        }
        
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string AdditionalInformation { get; set; }

    }
}
