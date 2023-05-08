namespace Firebase.Utils
{
    public class GoogleLoginObject
    {
        public string uid { get; set; }
        public string email { get; set; }
        public bool emailVerified { get; set; }
        public string displayName { get; set; }
        public bool isAnonymous { get; set; }
        public string photoURL { get; set; }
        public Providerdata[] providerData { get; set; }
        public Ststokenmanager stsTokenManager { get; set; }
        public string createdAt { get; set; }
        public string lastLoginAt { get; set; }
        public string apiKey { get; set; }
        public string appName { get; set; }
    }

    public class Ststokenmanager
    {
        public string refreshToken { get; set; }
        public string accessToken { get; set; }
        public long expirationTime { get; set; }
    }

    public class Providerdata
    {
        public string providerId { get; set; }
        public string uid { get; set; }
        public string displayName { get; set; }
        public string email { get; set; }
        public object phoneNumber { get; set; }
        public string photoURL { get; set; }
    }

}
