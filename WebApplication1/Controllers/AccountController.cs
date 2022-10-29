using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {

        MyContext myContext;

        public AccountController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        // GET: /<controller>/
        public IActionResult Index(LoginResponse loginResponse)
        {
            //var idLogin = 

            return View(loginResponse);
        }


        public IActionResult Login()
        {

            return View();
        }


       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var data = myContext.Users
                .Include(x => x.Employees)
                .Include(x => x.Roles)
                .SingleOrDefault(x => x.Employees.Email.Equals(email) && x.Password.Equals(password));

            if (data != null)
            {
                User user = new User()
                {
                    Password = data.Password
                };

                LoginResponse loginResponse= new LoginResponse()
                {
                    FullName = data.Employees.FullName,
                    Email = data.Employees.Email,
                    Role = data.Roles.Name
                };

                return RedirectToAction("Index", "Account", loginResponse);


            }


            return View();

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string email, string birthDate, string password)
        {
            Employee employee = new Employee()
            {
                FullName = fullName,
                Email = email,
                BirthDate = birthDate
            };


            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                User user = new User()
                {
                    EmployeeId = id,
                    Password = password,
                    RoleId = 1,
                };

                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                    return RedirectToAction("Login", "Account");

            }
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }


        //RESET PASSWORD TO DEAFULT 123456
        [HttpPost]
        public IActionResult ResetPassword(string fullName, string email, string birthDate)
        {
            var data = myContext.Users
                          .Include(x => x.Employees)
                          .SingleOrDefault(x => x.Employees.Email
                          .Equals(email) && x.Employees.FullName.Equals(fullName) && x.Employees.BirthDate.Equals(birthDate));

            if (data != null)
            {
                Employee employee = new Employee()
                {
                    FullName = fullName,
                    Email = email,
                    BirthDate = birthDate
                };

                User user = new User()
                {
                    Password = "123456"
                };

                data.Password = user.Password;
                myContext.Entry(data).State = EntityState.Modified;
                myContext.SaveChanges();


                return RedirectToAction("Login", "Account");
            }


            return View();
        }
        //public IActionResult NewPassword()
        //{
           
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult NewPassword(string password, Employee employee, User user)
        //{
        //    var data = myContext.Users.Find(employee.Email);
        //    if (data != null)
        //    {
        //        data.Password = user.Password;
        //        myContext.Entry(data).State = EntityState.Modified;
        //        var result = myContext.SaveChanges();
        //        if (result > 0)
        //            return RedirectToAction("Login", "Account");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Account");
        //    }

        //    return View();
        //}

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(string OldPassword, User user)
        {
            var data = myContext.Users.SingleOrDefault(x => x.Password.Equals(OldPassword));

            if (data != null)
            {
                if (data.Password == OldPassword)
                {
                    data.Password = user.Password;
                    myContext.Entry(data).State = EntityState.Modified;
                    var result = myContext.SaveChanges();
                    if (result > 0)
                        return RedirectToAction("Login");
                }
            }

            return View();
        }
    }
}
