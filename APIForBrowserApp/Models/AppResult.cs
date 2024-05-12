﻿namespace APIForBrowserApp.Models
{
    public class AppResult<T>
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
