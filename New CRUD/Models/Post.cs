﻿using System.ComponentModel.DataAnnotations;

namespace New_CRUD.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
    }
}
