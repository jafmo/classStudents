//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace classStudents.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int Id { get; set; }
        public string firstname { get; set; }
        public int Age { get; set; }
        public string surname { get; set; }
        public decimal GPA { get; set; }
        public int schoolClassId { get; set; }
    
        public virtual schoolClass schoolClass { get; set; }
    }
}
