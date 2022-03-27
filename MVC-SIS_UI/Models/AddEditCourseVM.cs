using System;
using MVC_SIS_Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

// Course 4a)

namespace MVC_SIS_UI.Models
{
    public class AddEditCourseVM : IValidatableObject
    {
        public Course currentCourse { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            // Courses 4b)

            // Error if name doesn't meet criteria
            // Not null or empty
            // Letters/Numbers/Spaces only
            // Up to 3 spaces maximum
            // Leader letter cannot be a space
            // 20 character limit
            // # character permitted
            int countSpaces = currentCourse.CourseName.Count(x => x.ToString() == " ");
            if (currentCourse == null || currentCourse.CourseName == "" || currentCourse.CourseName.Length > 20 
                || !Regex.IsMatch(currentCourse.CourseName, @"^[a-zA-Z0-9 #]+$")
                || currentCourse.CourseName[0].ToString() == " " || countSpaces > 3)
            {
                errors.Add(new ValidationResult("Please enter a Course name of 20 letters or less using only letters, numbers and (up to 3) spaces and hashtag symbol #",
                    new[] { "currentCourse.Name invalid" }));
            }

            return errors;
        }
    }
}