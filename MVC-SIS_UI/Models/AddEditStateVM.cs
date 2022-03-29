using System;
using MVC_SIS_Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

// 4a) Create the AddEditState ViewModel
namespace MVC_SIS_UI.Models
{
    public class AddEditStateVM : IValidatableObject
    {
        public State currentState { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            // 4b) We want to make sure that the StateName and StaveAbbreviation are not blank or null, so add to the Validate method to make sure this cannot happen.

            // Error if null
            if (currentState == null || currentState.StateAbbreviation == null)
            {
                errors.Add(new ValidationResult("Please enter a State Name and Abbreviation",
                    new[] { "currentState.StateName, currentState.StateAbbreviation null or empty" }));
                return errors;
            }

            // Error if not correct length
            if (currentState.StateName.Length > 20 || currentState.StateName.Length == 0 || currentState.StateAbbreviation.Length != 2)
            {
                errors.Add(new ValidationResult("State Name must be 20 letters or less, and Abbreviation must be 2 letters.",
                    new[] { "currentState.StateName length, currentState.StateAbbreviation length" }));
            }

            // Error if name has invalid characters or too many spaces
            int countSpaces = currentState.StateName.Count(x => x.ToString() == " ");
            if (!Regex.IsMatch(currentState.StateName, @"^[a-zA-Z ]+$") || currentState.StateName[0].ToString() == " " || countSpaces > 1)
            {            
                errors.Add(new ValidationResult("State Name must contain letters and a maximum of one space only.",
                    new[] { "currentState.StateName characters or spaces" }));
            }

            // Error if abbreviation has invalid characters
            if (!Regex.IsMatch(currentState.StateAbbreviation, @"^[a-zA-Z]+$"))
            {
                errors.Add(new ValidationResult("State Abbreviation must contain letters only.",
                    new[] { "currentState.StateAbbreviation characters" }));
            }

            return errors;
        }
    }
}