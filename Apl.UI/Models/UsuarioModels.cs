using System;
using System.ComponentModel.DataAnnotations;
using Apl.Entities.Constraints;
using Apl.Entities.Domain;

namespace Apl.UI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "Required")]
        [StringLength(GeneralConstraints.StringLengthNames, ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [RegularExpression(GeneralConstraints.RegExpressionTrim, ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "NoTrimString")]
        [Display(Name = "MyNames", ResourceType = typeof(Entities.Resources.Names))]
        public string Name { get; set; }

        [StringLength(GeneralConstraints.StringLengthLastNames, ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [RegularExpression(GeneralConstraints.RegExpressionTrim, ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "NoTrimString")]
        [Display(Name = "LastNames", ResourceType = typeof(Entities.Resources.Names))]
        public string LastName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "Required")]
        [StringLength(Constraints.GeneralConstraints.StringMaxLengthPassword, MinimumLength = Constraints.GeneralConstraints.StringMinLengthPassword, 
                                                                        ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Entities.Resources.Names))]
        public string Password { get; set; }


        [Required(ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "RetypePassword", ResourceType = typeof(Resources.ModelsFieldNames))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.ModelsErrors), ErrorMessageResourceName = "RetypePasswordDiferent")]
        public string ConfirmPassword { get; set; }

        
        [Required(ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "Required")]
        [StringLength(GeneralConstraints.StringLengthEmail, ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(GeneralConstraints.RegExpressionEmail, ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "InvalidEmail")]
        [Display(Name = "Email", ResourceType = typeof(Entities.Resources.Names))]
        public string Email { get; set; }


        [Required(ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "Required")]
        [StringLength(GeneralConstraints.StringLengthPhone, ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(GeneralConstraints.RegExpressionPhoneCell, ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "InvalidPhoneCell")]
        [Display(Name = "Phone", ResourceType = typeof(Entities.Resources.Names))]
        public string Phone { get; set; }


        [Required(ErrorMessageResourceType = typeof(Entities.Resources.Errors), ErrorMessageResourceName = "Required")]
        [Display(Name = "AcceptPolices", ResourceType = typeof(Resources.ModelsFieldNames))]
        public bool AceptoPoliticas { get; set; }


    }

}