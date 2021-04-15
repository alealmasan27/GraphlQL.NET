using System.Linq;
using GraphQLServer.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQLServer
{
    public class Query
    {
        [UseDbContext(typeof(UniversityContext))]
        [HotChocolate.Data.UseFiltering]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Student> GetStudents([ScopedService] UniversityContext universityContext) => universityContext.Students.Include(x => x.Enrollments).ThenInclude(x => x.Course);

        [UseDbContext(typeof(UniversityContext))]
        [HotChocolate.Data.UseFiltering]
        [HotChocolate.Data.UseSorting]
        public IQueryable<Enrollment> GetEnrollments([ScopedService] UniversityContext universityContext) => universityContext.Enrollments.Include(x => x.Course).Include(x => x.Student);
    }
}