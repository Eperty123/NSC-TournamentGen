﻿using SQLitePCL;

namespace NSC_TournamentGen.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}