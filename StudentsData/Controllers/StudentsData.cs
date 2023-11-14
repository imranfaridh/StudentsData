
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using StudentsData.Data;
using StudentsData.Dto;
using StudentsData.Models;

namespace StudentsData.Controllers
{
    [Route("api/StudentsData/")]
    [ApiController]
    public class StudentsDataController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ListDTO>> GetListModels()
        {
            return Ok(ListData.listItems);
        }

        [HttpGet ("{id:int}", Name = "GetListModel")]
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ListDTO> GetListModel(int id)
        {

            if (id == 0)
            {
                return BadRequest();
            }
            var List = ListData.listItems.FirstOrDefault(u => u.Id == id);
            if (List == null)
            {
                return NotFound();
            }
            return Ok(List);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<ListDTO> CreateList([FromBody] ListDTO listDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            // CUSTOM VALIDATION
            if (ListData.listItems.FirstOrDefault(u => u.Name.ToLower() == listDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "List already exists");
                return BadRequest(ModelState);
            }

            if (listDTO == null)
            {
                return BadRequest(listDTO);
            }
            //if(listDTO.Id <= 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            //listDTO.Id = ListData.listItems.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            listDTO.Id = ListData.listItems.LastOrDefault().Id + 1;
            if (listDTO.Id == 5 || listDTO.Id == 9)
            {
                listDTO.Id = listDTO.Id + 1;
            }
            ListData.listItems.Add(listDTO);

            return CreatedAtRoute("GetListModel", new { id = listDTO.Id }, listDTO);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //[Route("api/StudentsData/DeleteList/id")]
        public IActionResult DeleteList(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var list = ListData.listItems.FirstOrDefault(u => u.Id == id);
            if (list == null)
            {
                return NotFound();
            }
            ListData.listItems.Remove(list);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateList(int id,[FromBody]ListDTO listDTO)
        {
            if (listDTO == null || id != listDTO.Id)
            {
                return BadRequest();
            }
           var list = ListData.listItems.FirstOrDefault(u=>u.Id == id);
            list.Name = listDTO.Name;
            list.Location = listDTO.Location;

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult UpdatePartialList(int id, JsonPatchDocument<ListDTO> patchDTO)
        {
            try
            {
                if (patchDTO == null || id == 0)
                {
                    return BadRequest();
                }
                var list = ListData.listItems.FirstOrDefault(u => u.Id == id);
                if (list == null)
                {
                    return BadRequest();
                }
                patchDTO.ApplyTo(list, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
            }catch(Exception ex)
            {
                if(ex.Message == "Age issue")
                {
                    return ex;
                }
                throw ex;
            }
            return NoContent();
        }
    }
}