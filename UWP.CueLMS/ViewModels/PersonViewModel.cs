using Library.LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels
{
    public class PersonViewModel
    {
        private List<Person> people;
        public Dictionary<string, int> IDDictionary { get; set; } //ID checker
        public PersonViewModel(List<Person> persons)
        {
            Student = new Student();
            TeachingAssistant = new TeachingAssistant();
            Instructor = new Instructor();

            people = persons;
            IDDictionary = new Dictionary<string, int>();
        }
        private Student Student { get; set; }
        private TeachingAssistant TeachingAssistant { get; set; }
        private Instructor Instructor { get; set; }
        public string Name { 
            get { return Student.Name; } 
            set { Student.Name = value; } 
        }
        public string Id
        {
            get { return Student.ID; }
            set { Student.ID = value.ToUpper(); }
        }
        public string Classification
        {
            get { return Student.Classification; }
            set
            {
                if (value.Equals("f"))
                {
                    Student.Classification = "Freshman";
                }
                else if (value.Equals("s"))
                {
                    Student.Classification = "Sophomore";
                }
                else if (value.Equals("j"))
                {
                    Student.Classification = "Junior";
                }
                else if (value.Equals("e"))
                {
                    Student.Classification = "Senior";
                }
                else
                {
                    Student.Classification = "N/A";
                }
            }
        }
        public void AddStudent()
        {
            people.Add(Student);
        }
        public bool CheckID(string ID)
        {
            var result = new ArgumentException();
            try { IDDictionary.Add(ID, 0); }
            catch (ArgumentException r)
            {
                result = r;
            }
            if (result.Source == null) //if key already exists
            {
                return true;
            }
            else { return false; }
        }
        public string TAName
        {
            get { return TeachingAssistant.Name; }
            set { TeachingAssistant.Name = value; }
        }
        public string TAId
        {
            get { return TeachingAssistant.ID; }
            set { TeachingAssistant.ID = value.ToUpper(); }
        }
        public void AddTA()
        {
            people.Add(TeachingAssistant);
        }
        public string InstructorName
        {
            get { return Instructor.Name; }
            set { Instructor.Name = value; }
        }
        public string InstructorId
        {
            get { return Instructor.ID; }
            set { Instructor.ID = value.ToUpper(); }
        }
        public void AddInstructor()
        {
            people.Add(Instructor);
        }
    }
}
