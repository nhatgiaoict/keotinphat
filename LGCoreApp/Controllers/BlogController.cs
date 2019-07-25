using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using LGCoreApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LGCoreApp.Controllers
{
    public class BlogController : Controller
    {
        IBlogService _blogService;
        IConfiguration _configuration;
        public BlogController(IBlogService blogService, IConfiguration configuration)
        {
            _blogService = blogService;
            _configuration = configuration;
        }
        [Route("blog.html")]
        public IActionResult Index()
        {
           // var categories = _productCategoryService.GetAll();
            return View();
        }

        //[Route("{alias}-c.{id}.html")]
        //public IActionResult Catalog(int id, int? pageSize, string sortBy, int page = 1)
        //{
        //    var catalog = new CatalogViewModel();
        //    ViewData["BodyClass"] = "shop_grid_full_width_page";
        //    if (pageSize == null)
        //        pageSize = _configuration.GetValue<int>("PageSize");

        //    catalog.PageSize = pageSize;
        //    catalog.SortType = sortBy;
        //    catalog.Data = _productService.GetAllPaging(id, string.Empty, page, pageSize.Value);
        //    catalog.Category = _productCategoryService.GetById(id);

        //    return View(catalog);
        //}


        //[Route("search.html")]
        //public IActionResult Search(string keyword, int? pageSize, string sortBy, int page = 1)
        //{
        //    var catalog = new SearchResultViewModel();
        //    ViewData["BodyClass"] = "shop_grid_full_width_page";
        //    if (pageSize == null)
        //        pageSize = _configuration.GetValue<int>("PageSize");

        //    catalog.PageSize = pageSize;
        //    catalog.SortType = sortBy;
        //    catalog.Data = _productService.GetAllPaging(null, keyword, page, pageSize.Value);
        //    catalog.Keyword = keyword;

        //    return View(catalog);
        //}

        //[Route("{alias}-b.{id}.html", Name = "BlogDetail")]
        //public IActionResult Details(int id)
        //{
        //    ViewData["BodyClass"] = "product-page";
        //    var model = new DetailViewModel();
        //    model.Product = _productService.GetById(id);
        //    model.Category = _productCategoryService.GetById(model.Product.CategoryId);
        //    model.RelatedProducts = _productService.GetRelatedProducts(id, 9);
        //    model.UpsellProducts = _productService.GetUpsellProducts(6);
        //    model.ProductImages = _productService.GetImages(id);
        //    model.Tags = _productService.GetProductTags(id);
        //    model.Colors = _billService.GetColors().Select(x => new SelectListItem()
        //    {
        //        Text = x.Name,
        //        Value = x.Id.ToString()
        //    }).ToList();
        //    model.Sizes = _billService.GetSizes().Select(x => new SelectListItem()
        //    {
        //        Text = x.Name,
        //        Value = x.Id.ToString()
        //    }).ToList();

        //    return View(model);
        //}
    }
}