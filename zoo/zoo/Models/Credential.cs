namespace zoo.Models

{

    using System;

    using System.Collections.Generic;

    using System.ComponentModel;

    using System.ComponentModel.DataAnnotations;



    public partial class Credential

    {

        public Credential()

        {

            this.Employees = new HashSet<Employee>();

        }

        [Required(ErrorMessage = "This field is required.")]

        [DisplayName("Username ")]

        public string username { get; set; }

        [DisplayName("Password ")]

        [DataType(DataType.Password)]

        [Required(ErrorMessage = "This field is required.")]

        public string password { get; set; }

        public System.Guid Employee_ID { get; set; }

        public string LoginErrorMessage { get; set; }

        public string RegistrationSuccessMessage { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }



    public class UserProfile

    {

        public string newUsername { get; set; }

        [DataType(DataType.Password)]

        public string oldPassword { get; set; }

        [DataType(DataType.Password)]

        [MaxLength(8)]

        [Required]

        public string newPassword { get; set; }

        public string PwdErrorMessage { get; set; }

        public string UserNameErrorMessage { get; set; }

        public string PhoneErrorMessage { get; set; }

        public string EmailErrorMessage { get; set; }

        public string newEmail { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]

        public string newPhone { get; set; }

    }

}