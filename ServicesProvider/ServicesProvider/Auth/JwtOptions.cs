﻿namespace ServicesProvider.Auth
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;

        public int ExpitesHours { get; set; }
    }
}
