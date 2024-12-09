using System.ComponentModel.DataAnnotations;

namespace WukongDemo.Util
{
    public class ValidRoleAttribute : ValidationAttribute
    {
        private static readonly string[] AllowedRoles = { "Member", "ProjectLeader", "Teacher" };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string role && Array.Exists(AllowedRoles, r => r == role))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"The role must be one of the following: {string.Join(", ", AllowedRoles)}.");
        }
    }
}
