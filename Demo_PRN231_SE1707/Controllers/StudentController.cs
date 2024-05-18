using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Demo_PRN231_SE1707.Models;

namespace Demo_PRN231_SE1707.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //Tao danh sach Student
        static List<Student> data = new List<Student>()
        {
            new Student("SV01","Nguyen Van A",21),
            new Student("SV02","Nguyen Van B",19),
            new Student("SV03","Nguyen Van C",20),
        };

        //Tao api/get: Get all Student
        [HttpGet]
        public IActionResult GetAllStudent()
        {
            return Ok(data);
        }

        //Tao api/get/1: Get Student by Id
        //Neu khong tim thay ket qua tra ve text "Student not exist"
        [HttpGet("id")]
        public IActionResult GetStudentById(string id) 
        {
            var student = data.Where(s=>s.Code.Contains(id)).ToList();
            if(student.Count == 0) 
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        //Tao api/post: Add student to the list
        //Check empty, exist code, age>18
        [HttpPost]
        public IActionResult PostStudent(Student student)
        {
            if (student.Age <= 18)
            {
                return BadRequest("Student age must be greater than 18");
            }
            foreach (Student s in data)
            {
                if (s.Code.Equals(student.Code))
                {
                    return BadRequest("Code exist");
                }
            }
            if (String.IsNullOrEmpty(student.Code) || String.IsNullOrEmpty(student.Name) || student.Age == 0)
            {
                return BadRequest("Empty field");
            }
            data.Add(student);
            return Ok(data);
        }

        //Tao api/put: Update Student
        //Check empty, age>18, check code ton tai 
        [HttpPut]
        public IActionResult PutStudent(Student student)
        {
            if (student.Age <= 18)
            {
                return BadRequest("Student age must be greater than 18");
            }
/*            foreach (Student s in data)
            {
                if (s.Code.Contains(student.Code))
                {
                    return BadRequest("Code exist");
                }
            }*/
            if (String.IsNullOrEmpty(student.Code) || String.IsNullOrEmpty(student.Name) || student.Age == 0)
            {
                return BadRequest("Empty field");
            }
            //Student s = data.Where(x=>x.Code.Equals(student.Code)).FirstOrDefault();
            foreach (Student s in data)
            {
                if (s.Code.Equals(student.Code))
                {
                    s.Code = student.Code;
                    s.Name = student.Name;
                    s.Age = student.Age;
                }
            }
            return Ok(data);
        }

        //Tao api/delete: Delete Student theo id
        //Check code ton tai
        [HttpDelete("id")]
        public IActionResult DeleteStudent(string id)
        {
            var student = data.Where(s => s.Code.Equals(id)).FirstOrDefault();
            if (student == null)
            {
                return NotFound("Student not found");
            }
            data.Remove(student);
            return Ok(data);
        }
    }
}
