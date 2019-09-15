namespace Auth.JWT
{
    public class JwtUserDto
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string AccountStatus { get; set; }
        public string AccessToken { get; set; }
        public string AssignedTeam { get; set; }
        public string UserPricipalName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
