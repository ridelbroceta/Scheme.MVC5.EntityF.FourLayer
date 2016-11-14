using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Apl.Entities.Constraints;

namespace Apl.Entities.Domain
{
    [MetadataType(typeof (UserMetaData))]
    public partial class user
    {
        public string ShortName
        {
            get
            {
                var nombre = Name;
                var length = nombre.Length;
                var pos = nombre.IndexOf(' ');
                return pos > 0 ? nombre.Substring(0, pos > 15 ? 15: pos) : nombre.Substring(0, 15 < length ? 15 : length);
            }

        }

    }


    public static class UserConstraints
    {
        public const byte StringLengthPassword = 255;
    }



    public class UserMetaData
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [Display(Name = "User", ResourceType = typeof(Resources.Names))]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [StringLength(GeneralConstraints.StringLengthNames, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [RegularExpression(GeneralConstraints.RegExpressionTrim, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "NoTrimString")]
        [Display(Name = "MyNames", ResourceType = typeof(Resources.Names))]
        public string Name { get; set; }

        [StringLength(GeneralConstraints.StringLengthLastNames, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [RegularExpression(GeneralConstraints.RegExpressionTrim, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "NoTrimString")]
        [Display(Name = "LastNames", ResourceType = typeof(Resources.Names))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [StringLength(UserConstraints.StringLengthPassword, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Names))]
        public string Pass { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [StringLength(GeneralConstraints.StringLengthEmail, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(GeneralConstraints.RegExpressionEmail, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "InvalidEmail")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Names))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [StringLength(GeneralConstraints.StringLengthPhone, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "StringLength")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(GeneralConstraints.RegExpressionPhoneCell, ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "InvalidPhoneCell")]
        [Display(Name = "Phone", ResourceType = typeof(Resources.Names))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [Display(Name = "IsLocked", ResourceType = typeof(Resources.Names))]
        public bool IsLocked { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [Display(Name = "Conectado", ShortName = "Conect")]
        public bool IsConnected { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [Display(Name = "Activo", ShortName = "Conect")]
        public bool IsActive { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [Display(Name = "C.Accesos", ShortName = "Acc")]
        public int CountAfterPassAttempt { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [Display(Name = "C.Acc.Fallos", ShortName = "Fallos")]
        public int CountFailedPassAttempt { get; set; }

        [Display(Name = "Creo", ShortName = "Creo")]
        public int UserCreated { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Errors), ErrorMessageResourceName = "Required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Creado", ShortName = "Creado")]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Logeado", ShortName = "Logeado")]
        public DateTime DateLastLogin { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Bloqueado", ShortName = "Bloq")]
        public DateTime DateLastLockout { get; set; }

        [Display(Name = "Bloqueó", ShortName = "Bloqueó")]
        public int UserLastLockout { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "CambioPass", ShortName = "Cam.Pass")]
        public DateTime DateLastPassChange { get; set; }

        [Display(Name = "Modificó", ShortName = "Modificó")]
        public int UserUpdated { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Modificado", ShortName = "Modificado")]
        public DateTime DateUpdated { get; set; }

        [Display(Name = "Reseteó", ShortName = "Reseteó")]
        public int UserResetPass { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Reseteado", ShortName = "Reseteado")]
        public DateTime DateResetPass { get; set; }

        [Display(Name = "Roles", ResourceType = typeof(Resources.Names))]
        public virtual ICollection<role> Roles { get; set; }
    }
}
