using BlogPost.DataAccess.Repository.IRepository;
using BlogPost.Models;
using BlogPost.Models.ViewModels;
using BlogPost.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Areas.Users.Controllers
{
    [Area("Users")]
    public class StoryController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public StoryController(IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _env = env;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int Id)
        {
            return View();
        }

        public IActionResult AjaxMode()
        {
            ViewData["HtmlCode"] = GetHtmlFileCode();
            return View();
        }

        string GetHtmlFileCode()
        {
            string fullpath = CommonUtility.GetHtmlFilePath(ViewData["FileName"].ToString(), _env);
            if (!System.IO.File.Exists(fullpath))
                return "<b>No saved data yet</b>";
            return System.IO.File.ReadAllText(fullpath);
        }


        public IActionResult AjaxLoadHandler()
        {
            return Content(GetHtmlFileCode(), "text/html");
        }


        [HttpPost]
        public IActionResult AjaxSaveStory(StoryViewModel storyVM)
        {
            //Cutom code
            if (storyVM != null)
            {
                string FileName = string.Empty;
                var id = _userManager.GetUserId(User); // Get user id:
                if (storyVM.StoryId == 0)
                {
                    FileName = "/" + id + "_" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".html";
                }
                else
                {
                    FileName = storyVM.FileName;
                }
                string fullpath = CommonUtility.GetHtmlFilePath(FileName, _env);
                System.IO.File.WriteAllText(fullpath, storyVM.htmlStory);
                string title = !string.IsNullOrEmpty(storyVM.Title) ? storyVM.Title : "Untitled";
                var storyObj = new Story()
                {
                    Id = storyVM.StoryId,
                    Title = title,
                    StoryDetail = FileName,
                    IsPublished = false,
                    ApplicationUserId = id.ToString(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };

                if (storyVM.StoryId == 0)
                {
                    _unitOfWork.Story.Add(storyObj);
                }
                else
                {
                    _unitOfWork.Story.Update(storyObj);
                }
                _unitOfWork.Save();
                var StoryId = storyObj.Id;
                if (StoryId > 0)
                {
                    storyVM.StoryId = StoryId;
                }
                storyVM.FileName = FileName;
                return Json(new { data = storyVM, success = true, responseText = "Your story successfuly saved!" });
            }
            else 
            {
                return Json(new { data = storyVM, success = false, responseText = "Error saving story!" });
            }
            
        }

        [HttpPost]
        public IActionResult AjaxSaveHandler1(string htmlcode)
        {
            //Cutom code
            var id = _userManager.GetUserId(User); // Get user id:
            string FileName = "/" + id + "_" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".html";
            ViewData["FileName"] = FileName;
            //

            string fullpath = CommonUtility.GetHtmlFilePath(FileName, _env);
            System.IO.File.WriteAllText(fullpath, htmlcode);
            return Content("OK");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult UserStories()
        {

            return View();
        }

        public IActionResult AjaxStoryDetail(string StoryType)
        {
            var id = _userManager.GetUserId(User); // Get user id:
            bool published = false;
            if (StoryType == "Published")
                published = true;
            var StoryList = _unitOfWork.Story.GetAll(s => s.ApplicationUserId == id && s.IsPublished == published);

            List<DashBoardViewModel> lstStory = new List<DashBoardViewModel>();
            lstStory.AddRange(StoryList.Select(i => new DashBoardViewModel()
            {
                StoryId = i.Id,
                Title = i.Title,
                StoryFirstPara = CommonUtility.GetFirstParaGrapthInHTML(i.StoryDetail, _env),
                UpdatedOn = i.UpdatedDate != null ? i.UpdatedDate.ToString("MMMM dd, yyyy") : i.CreatedDate.ToString("MMMM dd, yyyy")
            }));

            var DraftCount = _unitOfWork.Story.GetAll(s => s.ApplicationUserId == id && s.IsPublished == false).Count();
            var PublishedCount = _unitOfWork.Story.GetAll(s => s.ApplicationUserId == id && s.IsPublished == true).Count();

            if (lstStory != null)
            {
                return Json(new { data = lstStory, DraftCount = DraftCount, PublishedCount = PublishedCount, success = true });
            }
            else
            {
                return Json(new { success = false, responseText = "No Record Found!!" });
            }


        }

        public IActionResult LoadUserStory(int StoryId)
        {
            var StoryObj = _unitOfWork.Story.GetFirstOrDefault(u => u.Id == StoryId);
            StoryViewModel storyVM = new StoryViewModel();
            if (StoryObj != null)
            {
                string fullpath = CommonUtility.GetHtmlFilePath(StoryObj.StoryDetail, _env);
                var HtmlCode = System.IO.File.ReadAllText(fullpath);
                storyVM.StoryId = StoryObj.Id;
                storyVM.Title = StoryObj.Title;
                storyVM.SubTitle = StoryObj.SubTitle;
                storyVM.FileName = StoryObj.StoryDetail;
                storyVM.htmlStory = HtmlCode;
            }
            return Json(new { data = storyVM, success = true });
        }


        [HttpPost]
        public IActionResult AjaxPublishStory(int StoryId)
        {
            if (StoryId > 0)
            {
                var storyObj = _unitOfWork.Story.GetFirstOrDefault(u => u.Id == StoryId);
                if (storyObj != null)
                {
                    storyObj.IsPublished = true;
                    _unitOfWork.Story.Update(storyObj);
                    _unitOfWork.Save();
                }
            }
            return Json(new { success = true, responseText = "Successfuly Published!" });
        }

    }
}
