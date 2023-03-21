using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodingWebsite.Data;
using CodingWebsite.Models;
using CodingWebsite.ViewModels;
using System.Text;
using System.Security.Claims;
using CodingWebsite.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodingWebsite.Controllers
{
    public class AnswersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private string userId;
        string mail;
        private string email;


        public AnswersController(ApplicationDbContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
            // userId = User.FindFirstValue(ClaimTypes.NameIdentifier);         
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
           
              return _context.Answers != null ? 
                          View(await _context.Answers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Answers'  is null.");

        }
        public async Task<IActionResult> ScoreIndex()
        {
            userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ScoreCards= _context.ScoreCard.Where(m => m.StudentId == userId);
            
            return _context.ScoreCard != null ?
                        View(ScoreCards) :
                        Problem("Entity set 'ApplicationDbContext.Answers'  is null.");

        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Answers == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // GET: Answers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuesId,StudId,SAnswer,language,versionIndex,CreatedAt,UpdatedAt")] Answers answers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(answers);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Answers == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers.FindAsync(id);
            if (answers == null)
            {
                return NotFound();
            }
            return View(answers);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuesId,StudId,SAnswer,language,versionIndex,CreatedAt,UpdatedAt")] Answers answers)
        {
            if (id != answers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswersExists(answers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(answers);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Answers == null)
            {
                return NotFound();
            }

            var answers = await _context.Answers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answers == null)
            {
                return NotFound();
            }

            return View(answers);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Answers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Answers'  is null.");
            }
            var answers = await _context.Answers.FindAsync(id);
            if (answers != null)
            {
                _context.Answers.Remove(answers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswersExists(int id)
        {
          return (_context.Answers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        public async Task<IActionResult> QidAnswer(int id)
        {
            if (id == null || _context.Questions == null)
            {
                return NotFound();
            }
            userId = _userService.GetUserId();
            var questions = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            var answer = await _context.Answers
                .FirstOrDefaultAsync(m => m.QuesId == id && m.StudId== userId);
            QuestionAns obj = new QuestionAns();
            if (questions == null)
            {
                return NotFound();
            }
            if (answer != null)
            {
                obj.SAnswer = answer.SAnswer;
            }
            else
                obj.SAnswer = "";
            
            obj.QuesId = id;
            obj.TheQuestion = questions.TheQuestion;
            obj.StudId = _userService.GetUserId(); ;



            return View(obj);
           
        }
        [HttpPost]
        public async Task<IActionResult> RunQidAnswer( QuestionAns obj)
        {
            string err="";
            Answers AnsObj = new Answers();
            int isError = 0, totalcase = 0,correct=0;
            ScoreCard ScoreObj = new ScoreCard();
            if (obj.Id == 0)//create new answer
            {
                AnsObj.SAnswer = obj.SAnswer;
                AnsObj.StudId = obj.StudId;
                AnsObj.QuesId = obj.QuesId;
                AnsObj.versionIndex = "0";
                AnsObj.language = "java";
                AnsObj.CreatedAt = DateTime.Now;
                AnsObj.UpdatedAt = DateTime.Now;
               
                    _context.Add(AnsObj);
                    await _context.SaveChangesAsync();
                
            }

            else
            {
                try
                {
                    AnsObj.Id = obj.Id;
                    AnsObj.SAnswer = obj.SAnswer;
                    AnsObj.StudId = obj.StudId;
                    AnsObj.QuesId = obj.QuesId;
                    AnsObj.versionIndex = "0";
                    AnsObj.language = "java";
                    AnsObj.CreatedAt = obj.CreatedAt;
                    AnsObj.UpdatedAt = DateTime.Now;
                    _context.Update(AnsObj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswersExists(AnsObj.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            var questions = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == obj.QuesId);
            SendingObj2 sobj2 = new SendingObj2();
            sobj2.clientId = "ada7948122fc559e0314a5f43d1c2092";
            sobj2.clientSecret = "b4fb88a3c40ed68527e748baa17be4adc8392c1538873b91f8ec4a5932950ab6";
            sobj2.script = obj.SAnswer;
            sobj2.language = "java";
            sobj2.versionIndex = "0";


            var url = "https://api.jdoodle.com/v1/execute";
            using var client = new HttpClient();

            if (questions.Input1 != null)
            {
                totalcase++;
                sobj2.stdin = questions.Input1;
                string json = JsonConvert.SerializeObject(sobj2);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, data);
                var result = await response.Content.ReadAsStringAsync();
                dynamic data2 = JObject.Parse(result);
                string sdata2=data2.ToString();
                if (sdata2.Contains("error"))
                {
                    err = data2.output;
                    isError = 1;
                }
                else if (data2.output == questions.Output1)
                {
                    correct++;
                }

            }
            else
            {
                totalcase++;
                SendingObj1 sobj1 = new SendingObj1();
                sobj1.clientId = "ada7948122fc559e0314a5f43d1c2092";
                sobj1.clientSecret = "b4fb88a3c40ed68527e748baa17be4adc8392c1538873b91f8ec4a5932950ab6";
                sobj1.script = obj.SAnswer;
                sobj1.language = "java";
                sobj1.versionIndex = "0";    
               
                string json = JsonConvert.SerializeObject(sobj1);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, data);
                var result = await response.Content.ReadAsStringAsync();
                dynamic data2 = JObject.Parse(result);
                string output=data2.output;
                if(output!=null)
                if (output.Contains("error") )
                {
                    err= data2.output;
                    isError = 1;
                }
                else if(data2.output==questions.Output1)
                {
                    correct++;
                }

            }
            if (questions.Input2 != null)
            {
                totalcase++;
                sobj2.stdin = questions.Input2;
                string json = JsonConvert.SerializeObject(sobj2);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, data);
                var result = await response.Content.ReadAsStringAsync();
                dynamic data2 = JObject.Parse(result);
                string sdata2 = data2.ToString();
                if (sdata2.Contains("error"))
                {
                    err = data2.output;
                    isError = 1;
                }
                else if (data2.output == questions.Output2)
                {
                    correct++;
                }
            }
            if (questions.Input3 != null)
            {
                totalcase++;
                sobj2.stdin = questions.Input3;
                string json = JsonConvert.SerializeObject(sobj2);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, data);
                var result = await response.Content.ReadAsStringAsync();
                dynamic data2 = JObject.Parse(result);
                string sdata2 = data2.ToString();
                if (sdata2.Contains("error"))
                {
                    err = data2.output;
                    isError = 1;
                }
                else if (data2.output == questions.Output3)
                {
                    correct++;
                }
            }
           
            if (isError == 1)
            {
                obj.error = err;                
            }
            else
            {
                obj.Success = 1;
                obj.StestCase = correct;
                obj.Ttestcase = totalcase;
            }
            if (totalcase != 0)
            {
                ScoreObj.CreatedAt = DateTime.Now;
                ScoreObj.UpdatedAt = DateTime.Now;
                ScoreObj.QuestionId = questions.Id;
                ScoreObj.StudentId = obj.StudId;
                ScoreObj.Status = 1;
                ScoreObj.Score = (correct * questions.Score) / totalcase;
                ScoreObj.QuesHead = questions.QuestionHeading;
                ScoreCard ObjScoreCard=_context.ScoreCard.FirstOrDefault(m => m.StudentId==obj.StudId && m.QuestionId==questions.Id);
                if (ObjScoreCard != null)
                {
                    ObjScoreCard.UpdatedAt = DateTime.Now;
                    ObjScoreCard.QuestionId = questions.Id;
                    ObjScoreCard.StudentId = obj.StudId;
                    ObjScoreCard.Status = 1;
                    ObjScoreCard.Score = (correct * questions.Score) / totalcase;
                    ObjScoreCard.QuesHead = questions.QuestionHeading;
                    ScoreObj.Id = ObjScoreCard.Id;
                    _context.Update(ObjScoreCard);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Add(ScoreObj);
                    await _context.SaveChangesAsync();
                }

            }

            Answers Answ = await _context.Answers.FirstAsync(m => m.StudId == obj.StudId && m.QuesId == questions.Id);
            obj.Id=Answ.Id;
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> SaveQidAnswer(QuestionAns obj)
        {
            Answers AnsObj = new Answers();
            if (obj.Id==0)//create new answer
            {
                AnsObj.SAnswer = obj.SAnswer;
                AnsObj.StudId=obj.StudId;
                AnsObj.QuesId=obj.QuesId;
                AnsObj.versionIndex = "0";
                AnsObj.language = "java";                    
                AnsObj.CreatedAt= DateTime.Now;
                AnsObj.UpdatedAt = DateTime.Now;
               
                    _context.Add(AnsObj);
                    await _context.SaveChangesAsync();
                    return View("QidAnswer", obj);

            }

            else
            {
                try
                {
                    AnsObj.Id = obj.Id;
                    AnsObj.SAnswer = obj.SAnswer;
                    AnsObj.StudId = obj.StudId;
                    AnsObj.QuesId = obj.QuesId;
                    AnsObj.versionIndex = "0";
                    AnsObj.language = "java";
                    AnsObj.CreatedAt = obj.CreatedAt;
                    AnsObj.UpdatedAt = DateTime.Now;
                    _context.Update(AnsObj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswersExists(AnsObj.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("QidAnswer",obj);
            }

            return View("QidAnswer", obj);

        }

       
    }
}
