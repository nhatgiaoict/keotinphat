using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LGCoreApp.Application.Interfaces;
using LGCoreApp.Application.ViewModels.Blog;
using LGCoreApp.Utilities.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LGCoreApp.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        private IBlogService _blogService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BlogController(IBlogService blogService,
            IHostingEnvironment hostingEnvironment)
        {
            _blogService = blogService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region AJAX API

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _blogService.GetAll();
            return new OkObjectResult(model);
        }

        //[HttpGet]
        //public IActionResult GetAllCategories()
        //{
        //    var model = _productCategoryService.GetAll();
        //    return new OkObjectResult(model);
        //}

        [HttpGet]
        public IActionResult GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var model = _blogService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var model = _blogService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpPost]
        public IActionResult SaveEntity(BlogViewModel blogVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                blogVm.SeoAlias = TextHelper.ToUnsignString(blogVm.Name);
                if (blogVm.Id == 0)
                {
                    _blogService.Add(blogVm);
                }
                else
                {
                    _blogService.Update(blogVm);
                }
                _blogService.Save();
                return new OkObjectResult(blogVm);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _blogService.Delete(id);
                _blogService.Save();

                return new OkObjectResult(id);
            }
        }
        #endregion AJAX API
    }
}