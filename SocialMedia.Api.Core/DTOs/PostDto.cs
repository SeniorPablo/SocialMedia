﻿using System;

namespace SocialMedia.Api.Core.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
