using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoQA.Test.DataObject
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string DateOfBirth { get; set; }
        public List<string> Subjects { get; set; }
        public List<string> Hobbies { get; set; }
        public string Picture { get; set; }
        public string CurrentAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            return 
                $"FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Gender: {Gender}, " +
                $"MobileNumber: {Mobile}, DateOfBirth: {DateOfBirth}, " +
                $"Subjects: {string.Join(", ", Subjects)}, Hobbies: {string.Join(", ", Hobbies)}, " +
                $"Picture: {Picture}, CurrentAddress: {CurrentAddress}, State: {State}, City: {City}";
        }
    }
}