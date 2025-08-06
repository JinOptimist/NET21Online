using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;
using WebPortal.Models.NotesIndex;

namespace WebPortal.Controllers;

public class NotesController : Controller
{
    public IActionResult Index()
    {
        var categories = new List<Category>
        {
            new Category 
            {
                Name="Programming"
            },
            new Category
            {
                Name="Sport"
            },
            new Category
            {
                Name="Motorcycles"
            }
        };

        var tags = new List<Tag>
        {
            new Tag
            {
                Name = "#CSharp"
            },
            new Tag
            {
                Name = "#Gym"
            },
            new Tag
            {
                Name = "#DucatiPanigale"
            },
            new Tag
            {
                Name = "#NEW"
            }
        };

        var notes = new List<Note>
        {
            new Note
            {
                Title = "Note title 01",
                ImageUrl = "images/notes/csharp.jpeg",
                Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit. Consectetur adipiscing elit" +
                              "quisque faucibus ex sapien vitae. Ex sapien vitae pellentesque sem placerat in id." +
                              "Placerat in id cursus mi pretium tellus duis. Pretium tellus duis convallis tempus leo" +
                              "eu aenean.",
                Category = categories[0],
                Tags = new List<Tag>
                {
                    tags[0],
                    tags[3]
                }
            },
            new Note
            {
                Title = "Note title 02",
                ImageUrl = "images/notes/sport.jpeg",
                Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit. Consectetur adipiscing elit" +
                              "quisque faucibus ex sapien vitae. Ex sapien vitae pellentesque sem placerat in id." +
                              "Placerat in id cursus mi pretium tellus duis. Pretium tellus duis convallis tempus leo" +
                              "eu aenean.",
                Category = categories[1],
                Tags = new List<Tag>
                {
                    tags[1],
                    tags[3]
                }
            },
            new Note
            {
                Title = "Note title 03",
                ImageUrl = "images/notes/motorcycles.jpeg",
                Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit. Consectetur adipiscing elit" +
                              "quisque faucibus ex sapien vitae. Ex sapien vitae pellentesque sem placerat in id." +
                              "Placerat in id cursus mi pretium tellus duis. Pretium tellus duis convallis tempus leo" +
                              "eu aenean.",
                Category = categories[2],
                Tags = new List<Tag>
                {
                    tags[2],
                    tags[3]
                }
            },
            new Note
            {
                Title = "Note title 04",
                ImageUrl = "images/notes/sport.jpeg",
                Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit. Consectetur adipiscing elit" +
                              "quisque faucibus ex sapien vitae. Ex sapien vitae pellentesque sem placerat in id." +
                              "Placerat in id cursus mi pretium tellus duis. Pretium tellus duis convallis tempus leo" +
                              "eu aenean.",
                Category = categories[1],
                Tags = new List<Tag>
                {
                    tags[1],
                    tags[3]
                }
            },
            new Note
            {
                Title = "Note title 05",
                ImageUrl = "images/notes/csharp.jpeg",
                Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit. Consectetur adipiscing elit" +
                              "quisque faucibus ex sapien vitae. Ex sapien vitae pellentesque sem placerat in id." +
                              "Placerat in id cursus mi pretium tellus duis. Pretium tellus duis convallis tempus leo" +
                              "eu aenean.",
                Category = categories[1],
                Tags = new List<Tag>
                {
                    tags[0],
                    tags[3]
                }
            },
            new Note
            {
                Title = "Note title 05",
                Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit. Consectetur adipiscing elit" +
                              "quisque faucibus ex sapien vitae. Ex sapien vitae pellentesque sem placerat in id." +
                              "Placerat in id cursus mi pretium tellus duis. Pretium tellus duis convallis tempus leo" +
                              "eu aenean.",
                Category = categories[1],
                Tags = new List<Tag>
                {
                    tags[1],
                    tags[3]
                }
            },
            new Note
            {
                Title = "Note title 06",
                Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit. Consectetur adipiscing elit" +
                              "quisque faucibus ex sapien vitae. Ex sapien vitae pellentesque sem placerat in id." +
                              "Placerat in id cursus mi pretium tellus duis. Pretium tellus duis convallis tempus leo" +
                              "eu aenean.",
                Category = categories[2],
                Tags = new List<Tag>
                {
                    tags[2],
                    tags[3]
                }
            }
        };

        var banners = new List<Banner>
        {
            new Banner
            {
                ImageUrl = "images/notes/ads01.jpg",
                Name = "Banner01"
            },
            new Banner
            {
                ImageUrl = "images/notes/ads02.jpg",
                Name = "Banner02"
            }
        };

        var model = new NotesIndexViewModel
        {
            Categories = categories,
            Tags = tags,
            Notes = notes,
            Banners = banners
        };
        return View(model);
    }
}