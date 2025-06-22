using DecoratorDemo.Application.Interfaces.UseCases;
using DecoratorDemo.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DecoratorDemo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Product>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts(IGetAllProductsUseCase useCase)
    {
        return Ok(await useCase.Execute());
    }

    [HttpPost]
    [ProducesResponseType<Product>(StatusCodes.Status201Created)]
    public ActionResult<Product> AddProduct(IAddProductUseCase useCase, Product product)
    {
        useCase.Execute(product);

        return CreatedAtAction(nameof(GetAllProducts), null, product);
    }
}