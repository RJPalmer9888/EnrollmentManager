using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentManager.DATA
{
    public class EnrollmentMetadata
    {
        //public int EnrollmentID { get; set; }

        [Display(Name = "Student ID")]
        [Required(ErrorMessage = "\"Student ID\" is required")]
        public int StudentID { get; set; }

        [Display(Name = "Scheduled Class ID")]
        [Required(ErrorMessage = "\"Scheduled Class ID\" is required")]
        public int ScheduledClassID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        [Required(ErrorMessage = "\"Enrollment Date\" is required")]
        public DateTime EnrollmentDate { get; set; }
    }
    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }

    public class StudentMetadata
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "\"First Name\" is required")]
        [StringLength(20, ErrorMessage = "*First Name must be 20 characters or less")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "\"Last Name\" is required")]
        [StringLength(20, ErrorMessage = "*Last name must be 20 characters or less")]
        public string LastName { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Major not provided")]
        [StringLength(50, ErrorMessage = "*Major must be 50 characters or less")]
        public string Major { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Address not provided")]
        [StringLength(100, ErrorMessage = "*Address must be 100 characters or less")]
        [UIHint("MultilineText")]
        public string Address { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "City not provided")]
        [StringLength(25, ErrorMessage = "*City must be 25 characters or less")]
        public string City { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "State not provided")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "*Must be 2 characters")]
        public string State { get; set; }

        //[DataType(DataType.PostalCode)]
        [Display(Name = "Zip Code")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Zip Code not provided")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "*Value must be 5 characters")]
        public string ZipCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[-N/A-]")]
        [StringLength(20, ErrorMessage = "*Last name must be 20 characters or less")]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "\"Email\" is required")]
        [StringLength(50, ErrorMessage = "*Email must be 50 characters or less")]
        public string Email { get; set; }

        [Display(Name = "Image")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[-N/A-]")]
        [UIHint("MultilineText")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Student Status ID")]
        [Required(ErrorMessage = "\"Student Status ID\" is required")]
        [Range(0, int.MaxValue, ErrorMessage = "*Value must be a valid number, 0 or larger")]
        public int SSID { get; set; }
    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

    public class CoursMetadata
    {
        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "\"Course Name\" is required")]
        [StringLength(50, ErrorMessage = "*Course Name must be 50 characters or less")]
        public string CourseName { get; set; }

        [Display(Name = "Course Description")]
        [Required(ErrorMessage = "\"Course Description\" is required")]
        [UIHint("MultilineText")]
        public string CourseDescription { get; set; }

        [Display(Name = "Credit Hours")]
        [Required(ErrorMessage = "\"Credit Hours\" is required")]
        public byte CreditHours { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[-N/A-]")]
        [StringLength(250, ErrorMessage = "*Course Name must be 250 characters or less")]
        [UIHint("MultilineText")]
        public string Curriculum { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[-N/A-]")]
        [StringLength(500, ErrorMessage = "*Notes must be 500 characters or less")]
        [UIHint("MultilineText")]
        public string Notes { get; set; }

        [Display(Name = "Active?")]
        [Required(ErrorMessage = "\"Active Status\" is required")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(CoursMetadata))]
    public partial class Cours { }

    public class ScheduledClassMetadata
    {
        [Display(Name = "Course ID")]
        [Required(ErrorMessage = "\"Course ID\" is required")]
        public int CourseID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "\"Start Date\" is required")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "\"End Date\" is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Instructor Name")]
        [Required(ErrorMessage = "\"Instructor Name\" is required")]
        [StringLength(40, ErrorMessage = "*Instructor Name must be 40 characters or less")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "\"Location\" is required (usually a room number)")]
        [StringLength(20, ErrorMessage = "*Location must be 20 characters or less")]
        public string Location { get; set; }

        [Display(Name = "Scheduled Class Status ID")]
        [Range(0, int.MaxValue, ErrorMessage = "*Value must be a valid number, 0 or larger")]
        [Required(ErrorMessage = "\"Scheduled Class Status ID\" is required")]
        public int SCSID { get; set; }
    }

    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass { }

    public class ScheduledClassStatusMetadata
    {
        [Display(Name = "Scheduled Class Status Name")]
        [Required(ErrorMessage = "\"Scheduled Class Status\" is required")]
        [StringLength(50, ErrorMessage = "*Scheduled Class Status Name must be 50 characters or less")]
        public string SCSName { get; set; }
    }

    [MetadataType(typeof(ScheduledClassStatusMetadata))]
    public partial class ScheduledClassStatus {

    }

    public class StudentStatusMetadata
    {
        [Display(Name = "Student Status Name")]
        [Required(ErrorMessage = "\"Student Status\" is required")]
        [StringLength(30, ErrorMessage = "*Student Status Name must be 30 characters or less")]
        public string SSName { get; set; }

        [Display(Name = "Student Status Description")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "[-N/A-]")]
        [StringLength(250, ErrorMessage = "*Student Status Description must be 250 characters or less")]
        [UIHint("MultilineText")]
        public string SSDescription { get; set; }
    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }
}
