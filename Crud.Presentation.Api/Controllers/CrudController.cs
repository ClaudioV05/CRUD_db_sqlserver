using Crud.Application.Interfaces;
using Crud.Infraestructure.Domain.Entities;
using Crud.Presentation.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Mime;

namespace Crud.Presentation.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ServiceFilter(typeof(FilterActionContextController), Order = 1)]
    public class CrudController : ControllerBase
    {
        private readonly IServiceUsers _serviceUsers;

        public CrudController(IServiceUsers serviceUsers)
        {
            _serviceUsers = serviceUsers;
        }

        /// <summary>
        /// Create new Users.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/Create")]
        [ApiExplorerSettings(IgnoreApi = false)]
        [ServiceFilter(typeof(FilterActionContextLog), Order = 2)]
        [ServiceFilter(typeof(FilterActionContextTables<Users>), Order = 3)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([BindRequired] Users users)
        {
            try
            {
                await _serviceUsers.Create(users);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        /// <summary>
        /// Get Users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/Read")]
        [ApiExplorerSettings(IgnoreApi = false)]
        [ServiceFilter(typeof(FilterActionContextLog), Order = 2)]
        [ServiceFilter(typeof(FilterActionContextTables<Users>), Order = 3)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Read()
        {
            try
            {
                return Ok(await _serviceUsers.Read());
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update new Users.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/Update")]
        [ApiExplorerSettings(IgnoreApi = false)]
        [ServiceFilter(typeof(FilterActionContextLog), Order = 2)]
        [ServiceFilter(typeof(FilterActionContextTables<Users>), Order = 3)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([BindRequired] Users users)
        {
            try
            {
                await _serviceUsers.Update(users);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete new Users.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Produces(MediaTypeNames.Application.Json)]
        [Route("/Delete")]
        [ApiExplorerSettings(IgnoreApi = false)]
        [ServiceFilter(typeof(FilterActionContextLog), Order = 2)]
        [ServiceFilter(typeof(FilterActionContextTables<Users>), Order = 3)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([BindRequired] int id)
        {
            try
            {
                await _serviceUsers.Delete(id);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }
    }
}