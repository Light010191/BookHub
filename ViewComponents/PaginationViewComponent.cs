using BooksHub.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BooksHub.ViewComponents
{
    public class PaginationViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(int currentPage, int totalPages, int limit, int? tagId, int? categoryId)
        {
            PaginationViewModel paginationViewModel = new PaginationViewModel()
            {
                CurrentPage = currentPage,
                TotalPages = totalPages,
                LimitItem = limit,
                TagId = tagId,
                CategoryId =categoryId
            };

            return View("Pagination", paginationViewModel);
        }
    }
}
