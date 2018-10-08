using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DiagramAPI.Models;

namespace DiagramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagramsController : ControllerBase
    {
        private readonly DiagramAPIContext _context;

        public DiagramsController(DiagramAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Models.Diagram>> GetAll()
        {
            return _context.Diagrams.ToList();
        }

        [HttpGet("{id}", Name = "GetDiagram")]
        public ActionResult<Models.Diagram> GetById(long id)
        {
            var diagram = _context.Diagrams.Find(id);

            if (diagram == null)
            {
                return NotFound();
            }

            return diagram;
        }

        [HttpPost]
        public IActionResult Create(Models.Diagram diagram)
        {
            _context.Diagrams.Add(diagram);
            _context.SaveChanges();

            return CreatedAtRoute("GetDiagram", new { id = diagram.ID }, diagram);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Models.Diagram diagram)
        {
            var currentDiagram = _context.Diagrams.Find(id);

            if (currentDiagram == null)
            {
                return NotFound();
            }

            currentDiagram.Name = diagram.Name;

            _context.Diagrams.Update(currentDiagram);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var diagram = _context.Diagrams.Find(id);

            if (diagram == null)
            {
                return NotFound();
            }

            _context.Diagrams.Remove(diagram);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}
