namespace Demo_PRN231_SE1707.Models
{
    public class Student
    {
        public string Code {  get; set; }
        public string Name { get; set; }    
        public int Age  { get; set; } 

        public Student()
        {

        }

        public Student(string code, string name, int age)
        {
            Code = code;
            Name = name;
            Age = age;
        }
    }
}
