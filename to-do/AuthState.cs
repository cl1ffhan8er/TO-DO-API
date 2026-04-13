namespace to_do
{
    public static class AuthState
    {
        public static string? Username { get; set; }
        public static int UserId { get; set; }
        public static void SignOut()
        {
            Username = null;
        }
    }
}