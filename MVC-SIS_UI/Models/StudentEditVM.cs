using MVC_SIS_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MVC_SIS_UI.Models
{
    public class StudentEditVM : IValidatableObject
    {
        public Student Student { get; set; }
        public List<SelectListItem> CourseItems { get; set; }
        public List<SelectListItem> MajorItems { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        public List<int> SelectedCourseIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            
            if (Student.FirstName == "" || Student.LastName == "")
            {
                errors.Add(new ValidationResult("Please enter a valid Student name",
                    new[] { "Student.FirstName or Student.LastName invalid" }));
            }

            if (Student.GPA < 0)
            {
                errors.Add(new ValidationResult("Please enter a valid Student GPA",
                    new[] { "Student.GPA invalid" }));
            }

            if (Student.Major == null || SelectedCourseIds.Count <= 0)
            {
                errors.Add(new ValidationResult("Please select a valid Major and Course(s) for this Student",
                    new[] { "Student.Major or Student.Courses invalid" }));
            }

            if (Student.Address.Street1 == null)
            {
                errors.Add(new ValidationResult("Please enter a valid street, maximum 30 characters.",
                    new[] { "Student.Address.Street1 invalid" }));
                return errors;
            }

            if (Student.Address.Street1 == "" || Student.Address.Street1.Length > 30)
            {
                errors.Add(new ValidationResult("Please enter a valid street, maximum 30 characters.",
                    new[] { "Student.Address.Street1 invalid" }));
            }

            if (Student.Address.Street2 != null)
            {
                if (Student.Address.Street2.Length > 30)
                {
                    errors.Add(new ValidationResult("Please enter a valid street 2, maximum 30 characters.",
                        new[] { "Student.Address.Street2 invalid" }));
                }
            }

            if (Student.Address.City == ""|| Student.Address.City.Length > 20)
            {
                errors.Add(new ValidationResult("Please enter a valid City, maximum 20 characters.",
                    new[] { "Student.Address.City invalid" }));
            }

            if (Student.Address.State.StateAbbreviation == "")
            {
                errors.Add(new ValidationResult("Please select a valid State",
                    new[] { "Student.Address.State invalid" }));
            }

            if (Student.Address.PostalCode.Length != 5 || !Regex.IsMatch(Student.Address.PostalCode, @"^[0-9]+$"))
            {
                errors.Add(new ValidationResult("Please enter a Postal Code, numbers only, maximum 5.",
                    new[] { "Student.Address.PostalCode invalid" }));
            }
            
            return errors;
        }

        public StudentEditVM()
        {
            CourseItems = new List<SelectListItem>();
            MajorItems = new List<SelectListItem>();
            StateItems = new List<SelectListItem>();
            SelectedCourseIds = new List<int>();
            Student = new Student();
        }

        public void SetCourseItems(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                CourseItems.Add(new SelectListItem()
                {
                    Value = course.CourseId.ToString(),
                    Text = course.CourseName
                });
            }
        }

        public void SetMajorItems(IEnumerable<Major> majors)
        {
            foreach (var major in majors)
            {
                MajorItems.Add(new SelectListItem()
                {
                    Value = major.MajorId.ToString(),
                    Text = major.MajorName
                });
            }
        }

        public void SetStateItems(IEnumerable<State> states)
        {
            foreach (var state in states)
            {
                StateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }
    }
}