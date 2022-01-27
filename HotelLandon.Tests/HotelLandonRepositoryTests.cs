using HotelLandon.Data;
using HotelLandon.Models;
using HotelLandon.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HotelLandon.Tests
{
    public class CustomersRepository : HotelLandonRepositoryTests<Customer>
    {

    }

    public abstract class HotelLandonRepositoryTests<TEntity>
        where TEntity : EntityBase, new()

    {
        private readonly RepositoryBase<TEntity> repository;

        public HotelLandonRepositoryTests()
        {
            DbContextOptionsBuilder<HotelLandonContext> builder = new();
            builder.UseInMemoryDatabase("HotelLandon");

            HotelLandonContext context = new(builder.Options);

            // Ensure is a new database
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            repository = new RepositoryBase<TEntity>(context);

            context.Customers.AddRange(FakeData.Customers);

            context.Rooms.AddRange(FakeData.Rooms);

            context.SaveChanges();
        }

        [Fact]
        public async Task FindAll_ReturnsAllItems()
        {
            // Arrange
            int count = (await repository.GetAllAsync()).Count;

            // Act
            var items = await repository.GetAllAsync();

            // Assert
            Assert.Equal(count, items.Count);
        }

        [Fact]
        public async Task FindPersons_ReturnsSomeItems()
        {
            (await repository.GetAllAsync()).Where(e => e.Id == 1);
        }

        [Fact]
        public async Task DeletAsync()
        {
            //Arrange  
            var postId = 3;

            //Act 
            var data = await repository.DeleteAsync(postId);

            //Assert  
            Assert.True(data);
        }



    }
}
