﻿namespace ECommerceNet.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

    }
}
