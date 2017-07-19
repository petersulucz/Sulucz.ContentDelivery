namespace Sulucz.ContentDelivery.ApiTestApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Sulucz.ContentDelivery.ApiTestApp.Filters;
    using Sulucz.ContentDelivery.Data;
    using Sulucz.ContentDelivery.Models;

    [Route("api/[controller]")]
    [SuluczExceptionFilter]
    public class PostController : Controller
    {
        private readonly IDataLayer dataLayer;

        public PostController(IDataLayer datalayer)
        {
            this.dataLayer = datalayer;
        }

        /// <summary>
        /// Gets all posts.
        /// </summary>
        /// <returns>All of the posts.</returns>
        [HttpGet]
        public async Task<IEnumerable<PostModel>> Get()
        {
            var result = await this.dataLayer.GetPost();
            return result.Select(r => new PostModel(r));
        }

        /// <summary>
        /// Post a post.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Does nothing.</returns>
        [HttpPost]
        [ModelValidationFilter]
        public async Task Post([FromBody]PostModel model)
        {
            await this.dataLayer.SetPost(model.ToSuluczPost());
        }

        /// <summary>
        /// Deletes a post by its id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        [Route("{id}")]
        [HttpDelete]
        public async Task Delete(int id)
        {
            await this.dataLayer.DeletePost(id);
        }
    }
}
