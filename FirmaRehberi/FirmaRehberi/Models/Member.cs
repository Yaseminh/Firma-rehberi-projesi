using FirmaRehberi.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmaRehberi.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime ? Birthday { get; set; }
        public string Phonenumber { get; set; }
        public string City { get; set; }
        public bool ? Gender { get; set; }
        public int ? WaitingComments { get; set; }
        public int ? PublishedComments { get; set; }
        public int ? WaitingRequests { get; set; }
        public int ? PublishedRequests { get; set; }
        public string ProfileImageName { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ? ModifiedDate { get; set; }
        public bool EmailConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string PasswordHash { get; set; }
        public DateTime ? LockoutDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AcessFailedCount { get; set; }
        public Member()
        {

        }

        public Member(Members mem)
        {
            Id = mem.Id;
            Name = mem.Name;
            Surname = mem.Surname;
            Email = mem.Email;
            Password = mem.Password;
            Birthday = mem.Birthday;
            Phonenumber = mem.PhoneNumber;
            City = mem.City;
            Gender = mem.Gender;
            WaitingComments = mem.WaitingComments;
            PublishedComments = mem.PublishedComments;
            WaitingRequests = mem.WaitingRequests;
            PublishedRequests = mem.PublishedRequests;
            ProfileImageName = mem.ProfileImageName;
            AddedDate = mem.AddedDate;
            ModifiedDate = mem.ModifiedDate;
            EmailConfirmed = mem.EmailConfirmed;
            SecurityStamp = mem.SecurityStamp;
            Phonenumber = mem.PhoneNumberConfirmed;
            TwoFactorEnabled = mem.TwoFactorEnabled;
            PasswordHash = mem.PasswordHash;
            LockoutDateUtc = mem.LockoutDateUtc;
            LockoutEnabled = mem.LockoutEnabled;
            AcessFailedCount = mem.AcessFailedCount;
        }
    }
}