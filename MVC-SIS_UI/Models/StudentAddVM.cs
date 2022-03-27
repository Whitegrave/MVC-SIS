﻿using MVC_SIS_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_SIS_UI.Models
{
    public class StudentAddVM : IValidatableObject
    {
        public Student Student { get; set; }
        public List<SelectListItem> CourseItems { get; set; }
        public List<SelectListItem> MajorItems { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        public List<int> SelectedCourseIds { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Student.FirstName == null || Student.FirstName == ""
                || Student.LastName == null || Student.LastName == "")
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

            return errors;
        }

        public StudentAddVM()
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