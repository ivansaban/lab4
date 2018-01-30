using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.UnitTests
{
    public class Employees_Should
    {
        DbContextOptions<AdventureWorks2014Context> _dbContextOptions;

        public Suppliers_Should()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StoreSampleContext>()
                            .UseInMemoryDatabase(databaseName: "Test_database")
                            .Options;
        }

        [Fact]
        public async void GetSupplier()
        {
            using (var context = new StoreSampleContext(_dbContextOptions))
            {
                var suppliersAPI = new SuppliersController(context);
                for (int i = 0; i < 10; ++i)
                {
                    Supplier tmpSupplier = new Supplier();
                    tmpSupplier.CompanyName = $"Kompanija { i + 1 }";
                    tmpSupplier.City = "Zagreb";
                    tmpSupplier.Country = "Hrvatska";
                    tmpSupplier.Phone = $"01 234 567{ i }";
                    suppliersAPI.PostSupplier(tmpSupplier).Wait();
                }
            }

            using (var context = new StoreSampleContext(_dbContextOptions))
            {
                var suppliersAPI = new SuppliersController(context);
                var result = await suppliersAPI.GetSupplier(5);
                var okResult = result as OkObjectResult;

                // Ako je rezultat Ok i status kod je 200, tada je poziv uspjesan
                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);

                // Ako je dohvacen dobavljac sa ispravnim brojem telefona, poziv je uspjesan
                Supplier supplier = okResult.Value as Supplier;
                Assert.NotNull(supplier);
                Assert.Equal("01 234 5674", supplier.Phone);
            }
        }
    }
}
