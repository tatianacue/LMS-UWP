﻿using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWP.CueLMS.Dialogs;
using UWP.CueLMS.Views;

namespace UWP.CueLMS.ViewModels
{
    public class InstructorViewModel : ObservableObject
    {
        public InstructorViewModel(CourseService courseservice, PersonService personservice)
        {
            courseService = courseservice;
            personService = personservice;
            Courses = courseService.SpringList;
            People = personService.personList;
            Collection = new ObservableCollection<Person>(People);
            courseCollection = new ObservableCollection<Course>(Courses);
        }
        public Person SelectedPerson { get; set; }
        public Course SelectedCourse { get; set; }
        public CourseService courseService { get; set; }
        public PersonService personService { get; set; }
        public List<Course> Courses { get; set; }
        public List<Person> People { get; set; }
        private ObservableCollection<Person> collection { get; set; } //person collection
        public ObservableCollection<Person> Collection { get { return collection; } set { collection = value; } }
        private ObservableCollection<Course> courseC { get; set; } //course collection
        public ObservableCollection<Course> courseCollection { get { return courseC; } set { courseC = value; } }
        public async void PersonDialog()
        {
            var dialog = new AddPerson(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        public async void AddStudent()
        {
            var dialog = new StudentDialog(People);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = ""; //refresh
            SearchPeople();
        }
        public async void AddTeachingAssistant()
        {
            var dialog = new TeachingAssistantDialog(People);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = "";
            SearchPeople();
        }
        public async void AddInstructor()
        {
            var dialog = new InstructorDialog(People);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = "";
            SearchPeople();
        }
        public void SearchPeople()
        {
            var searchResults = People.Where(p => p.Name.Contains(Query)).ToList();
            Collection.Clear();
            foreach (var person in searchResults)
            {
                Collection.Add(person);
            }
        }
        public string Query { get; set; }
        public string Search { get; set; }
        public string NewName { private get; set; }
        public string NewId { private get; set; }
        private string classification;
        public string NewClassification { private get { return classification; }
            set
            {
                if (value.Equals("f"))
                {
                    classification = "Freshman";
                }
                else if (value.Equals("s"))
                {
                    classification = "Sophomore";
                }
                else if (value.Equals("j"))
                {
                    classification = "Junior";
                }
                else if (value.Equals("e"))
                {
                    classification = "Senior";
                }
            }
        }
        public void DeletePerson()
        {
            People.Remove(SelectedPerson);
            Query = "";
            SearchPeople();
        }
        public void UpdateId()
        {
            if (NewId != null)
            {
                SelectedPerson.ID = NewId;
            }
            NotifyPropertyChanged(nameof(DisplayPerson));
        }
        public void UpdateName()
        {
            if (NewName != null)
            {
                SelectedPerson.Name = NewName;
            }
            NotifyPropertyChanged(nameof(DisplayPerson));
        }
        public void UpdateClassification()
        {
            if (NewClassification != null)
            {
                Student selected = (Student)SelectedPerson;
                selected.Classification = NewClassification;
            }
            NotifyPropertyChanged(nameof(DisplayPerson));
        }
        public string DisplayPerson
        {
            get
            {
                if (SelectedPerson != null)
                {
                    return SelectedPerson.Display;
                }
                else { return "No Person Selected"; }
            }
        }
        public void SemesterSelect(int choice)
        {
            if (choice == 1) //spring list
            {
                Courses = courseService.SpringList;
            }
            else if (choice == 2) //summer list
            {
                Courses = courseService.SummerList;
            }
            else if (choice == 3) //fall list
            {
                Courses = courseService.FallList;
            }
        }
        public async void SemesterDialog()
        {
            var dialog = new SemesterDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Search = ""; //refresh
            SearchCourses();
        }
        public async void AddCourse()
        {
            var dialog = new CourseDialog(Courses);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Search = ""; //refresh
            SearchCourses();
        }
        public void SearchCourses()
        {
            var searchResults = Courses.Where(p => p.Name.Contains(Search) || p.Description.Contains(Search)).ToList();
            courseCollection.Clear();
            foreach (var course in searchResults)
            {
                courseCollection.Add(course);
            }
        }
        public void DeleteCourse()
        {
            Courses.Remove(SelectedCourse);
            Search = ""; //refresh
            SearchCourses();
        }
        //course update stuff
        public string NewCourseCode { get; set; }
        public string NewCourseName { get; set; }
        public string NewCourseDescription { get; set; }
        public int NewCourseHours { private get; set; }
        public string NewCourseRoom { get; set; }
        public string CourseCode
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.Code;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string CourseName
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string CourseDescription
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.Description;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public int CourseHours
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.CreditHours;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string CourseRoom
        {
            get
            {
                if (SelectedCourse != null)
                {
                    return SelectedCourse.RoomLocation;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public void UpdateCourseCode()
        {
            if (NewCourseCode != null)
            {
                SelectedCourse.Code = NewCourseCode;
            }
            NotifyPropertyChanged(nameof(CourseCode));
        }
        public void UpdateCourseName()
        {
            if (NewCourseName != null)
            {
                SelectedCourse.Name = NewCourseName;
            }
            NotifyPropertyChanged(nameof(CourseName));
        }
        public void UpdateCourseDescription()
        {
            if (NewCourseDescription != null)
            {
                SelectedCourse.Description = NewCourseDescription;
            }
            NotifyPropertyChanged(nameof(CourseDescription));
        }
        public void UpdateCourseCreditHours()
        {
            SelectedCourse.CreditHours = NewCourseHours;
            NotifyPropertyChanged(nameof(CourseHours));
        }
        public void UpdateCourseRoom()
        {
            if (NewCourseRoom != null)
            {
                SelectedCourse.RoomLocation = NewCourseRoom;
            }
            NotifyPropertyChanged(nameof(CourseRoom));
        }

    }
}
