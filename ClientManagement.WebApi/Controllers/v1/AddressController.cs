using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class AddressController : BaseApiController
    {
        /*
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        /// <summary>
        /// Retrieves a list of dishes.
        /// </summary>
        /// <returns>The list of dishes.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var dishes = await _dishService.GetAllViewModelWithInclude();

                if (dishes == null || dishes.Count == 0)
                {
                    return NotFound();
                }

                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retrieves a dish by its ID.
        /// </summary>
        /// <param name="id">The ID of the dish.</param>
        /// <returns>The dish with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveDishViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var dish = await _dishService.GetById(id);

                if (dish == null)
                {
                    return NoContent();
                }

                return Ok(dish);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Creates a new dish.
        /// </summary>
        /// <param name="vm">The view model containing the dish information.</param>
        /// <returns>The created dish.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(SaveDishViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await _dishService.Add(vm);
                return StatusCode(StatusCodes.Status201Created, vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Updates a dish by its ID.
        /// </summary>
        /// <param name="id">The ID of the dish.</param>
        /// <param name="vm">The view model containing the updated dish information.</param>
        /// <returns>The updated dish.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveDishViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, SaveDishViewModel vm)
        {
            var existingDish = await _dishService.GetByIdSaveViewModel(id);
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                vm.Name = existingDish.Name;
                vm.Price = existingDish.Price;
                vm.Category = existingDish.Category;
                vm.ServingSize = existingDish.ServingSize;

                await _dishService.Update(vm, id);
                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }*/

    }
}

