﻿namespace LPMS.Domain.Models.RnRModels.UserManagementModels
{
    public class RefreshTokenRequest
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}