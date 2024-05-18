using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using MiniAPI8.Models;
namespace MiniAPI8.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course").WithTags(nameof(Course));

        group.MapGet("/", async (ContosoUniversityContext db) =>
        {
            return await db.Courses.ToListAsync();
        })
        .WithName("GetAllCourses")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Course>, NotFound>> (int courseid, ContosoUniversityContext db) =>
        {
            return await db.Courses.AsNoTracking()
                .FirstOrDefaultAsync(model => model.CourseId == courseid)
                is Course model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCourseById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int courseid, Course course, ContosoUniversityContext db) =>
        {
            var affected = await db.Courses
                .Where(model => model.CourseId == courseid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.CourseId, course.CourseId)
                    .SetProperty(m => m.Title, course.Title)
                    .SetProperty(m => m.Credits, course.Credits)
                    .SetProperty(m => m.DepartmentId, course.DepartmentId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCourse")
        .WithOpenApi();

        group.MapPost("/", async (Course course, ContosoUniversityContext db) =>
        {
            db.Courses.Add(course);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Course/{course.CourseId}",course);
        })
        .WithName("CreateCourse")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int courseid, ContosoUniversityContext db) =>
        {
            var affected = await db.Courses
                .Where(model => model.CourseId == courseid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCourse")
        .WithOpenApi();
    }
}
