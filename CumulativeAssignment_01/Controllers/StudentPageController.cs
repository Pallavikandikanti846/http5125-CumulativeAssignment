using Microsoft.AspNetCore.Mvc;
using CumulativeAssignment_01.Models;
using System.Diagnostics;
namespace CumulativeAssignment_01.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly StudentAPIController _api;

        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }
        // GET: StudentPage/List -> A webpage that prints all the students data in student table
        public IActionResult List()
        {
            List<Student> studentsInfo = _api.StudentsList();
            //Directs to /Views/StudentPage/List.cshtml
            return View(studentsInfo);
        }
        [HttpGet]
        // GET: Studentpage/Show -> A webpage that prints a single student data in table
        public IActionResult Show(int id)
        {

            Student student = _api.StudentInfo(id);
            //Directs to /Views/StudentPage/Show.cshtml
            return View(student);
        }
        [HttpGet]
        // GET: StudentPage/New -> A webpage that asks user to add new student information
        public IActionResult New()
        {
            //Directs to /Views/StudentPage/New.cshtml
            return View();
        }
        [HttpPost]
        //POST: StudentPage/Create -> 
        //Header : Content-Type: application/x-www-url-encoded
        //FORM DATA : studentfname={StudentFName}&studentlname={StudentLName}&studentnumber={StudentNumber}
        //Receives information about the student and goes to the list of students.
        public IActionResult Create(string StudentFName, string StudentLName, string StudentNumber,DateTime EnrolDate)
        {
            //creating Student object
            Student newStudent = new Student();

            newStudent.StudentFName = StudentFName;
            newStudent.StudentLName = StudentLName;
            newStudent.StudentNumber = StudentNumber;
            newStudent.EnrolDate = EnrolDate;

            Debug.WriteLine(StudentFName);
            Debug.WriteLine(StudentLName);
            Debug.WriteLine(StudentNumber);
            Debug.WriteLine(EnrolDate);


            int studentId = _api.addStudent(newStudent);


            // redirects to /Views/TeacherPage/List.cshtml
            return RedirectToAction("List", new { id = studentId });
        }
        //[HttpGet]
        //GET: StudentPage/DeleteConfirm -> An article that asks a user if they want to delete student data in table
        public IActionResult DeleteConfirm(int id)
        {
            Student studentSelected = _api.StudentInfo(id);
            // redirects to /Views/StudentPage/DeleteConfirm.cshtml
            return View(studentSelected);
        }
        [HttpPost]
        //POST: StudentPage/Delete -> 
        //Header : Content-Type: application/x-www-url-encoded
        //deletes information about the student and goes to the list of students.
        public IActionResult Delete(int id)
        {
            int studentId = _api.deleteStudent(id);

            // redirects to /Views/StudentPage/List.cshtml
            return RedirectToAction("List");
        }
        [HttpGet]
        // GET: StudentPage/Edit -> A webpage where user can update existing student information
        public IActionResult Edit(int id)
        {
            Student studentToUpdate = _api.StudentInfo(id);

            // redirects to /Views/TeacherPage/Edit.cshtml
            return View(studentToUpdate);
        }
        [HttpPost]
        //POST: StudentPage/Update -> 
        //Header : Content-Type: application/x-www-url-encoded
        //FORM DATA : studentfname={StudentFName}&studentlname={StudentLName}&studentnumber={StudentNumber}
        //Updates information about the student and goes to the list of students.
        public IActionResult Update(int id,string StudentFName, string StudentLName, string StudentNumber, DateTime EnrolDate)
        {
            //creating Student object
            Student studentDetails = new Student();

            studentDetails.StudentFName = StudentFName;
            studentDetails.StudentLName = StudentLName;
            studentDetails.StudentNumber = StudentNumber;
            studentDetails.EnrolDate = EnrolDate;

            Debug.WriteLine(StudentFName);
            Debug.WriteLine(StudentLName);
            Debug.WriteLine(StudentNumber);
            Debug.WriteLine( EnrolDate.ToString());
            Debug.WriteLine("Received ID: " + id);


            _api.updateStudent(id,studentDetails);

            // redirects to /Views/StudentPage/Show.cshtml
            return RedirectToAction("Show", new { id = id });
        }
    }
}
