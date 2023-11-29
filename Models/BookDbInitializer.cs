namespace BooksHub.Models
{
    public class BookDbInitializer
    {
        static public void seed(IApplicationBuilder app)
        {
            var result = app.ApplicationServices.CreateScope();
            var context = result.ServiceProvider.GetRequiredService<BookDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category() { Name = "History" });
                context.Categories.Add(new Category() { Name = "Fantazy" });
                context.Categories.Add(new Category() { Name = "Fantastic" });
                context.Categories.Add(new Category() { Name = "Sport" });
                context.Categories.Add(new Category() { Name = "Sciences" });
                context.Categories.Add(new Category() { Name = "Novel" });
                context.SaveChanges();
            }

            if (!context.Tags.Any())
            {
                context.Tags.Add(new Tag() { Name = "#classics" });
                context.Tags.Add(new Tag() { Name = "#bookcovers" });
                context.Tags.Add(new Tag() { Name = "#newbooks" });
                context.Tags.Add(new Tag() { Name = "#bestselling" });
                context.Tags.Add(new Tag() { Name = "#lifewriting" });
                context.Tags.Add(new Tag() { Name = "#shortstories" });
                context.Tags.Add(new Tag() { Name = "#bibliophile" });
                context.SaveChanges();
            }            
        }
    }
}
