using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Student.MVC.Models;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Student.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private string url = "https://localhost:7109/api/StudentAPI/";
        private HttpClient _client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<Students> lstStudents=new List<Students>();
            HttpResponseMessage response = _client.GetAsync(url + "GetAllStudents").Result;
            if(response.IsSuccessStatusCode)
            {
                string result=response.Content.ReadAsStringAsync().Result;
                var data=JsonConvert.DeserializeObject<List<Students>>(result);

                if (data != null)
                {
                    lstStudents = data;
                }
            }
            return View(lstStudents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Students students)
        {
            string data = JsonConvert.SerializeObject(students);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response =  _client.PostAsync(url+ "AddStudent", content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["insert_Message"] = "Student added.";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Students std = new Students();
            HttpResponseMessage response = _client.GetAsync(url + "GetStudentById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Students>(result);
                if (data != null)
                {
                    std = data;
                }
            }
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Students student)
        {
            string data = JsonConvert.SerializeObject(student);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            string fullUrl = url + "UpdateStudent/" + student.Id;
            HttpResponseMessage response = _client.PutAsync(fullUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["update_Message"] = "Student updated.";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Students student = new Students();
            HttpResponseMessage response = _client.GetAsync(url + "GetStudentById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Students>(result);
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(url + "DeleteStudent/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["delete_message"] = "Student deleted.";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
