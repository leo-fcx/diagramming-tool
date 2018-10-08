using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DiagramAPI.Models;
using DiagramService;

namespace DiagramAPI.Controllers
{
    [Route("api/diagrams/{diagramId}/[controller]")]
    [ApiController]
    public class ClassifiersController : ControllerBase
    {
        private readonly DiagramAPIContext _context;

        public ClassifiersController(DiagramAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Models.Classifier>> GetAll(long diagramId)
        {
            var diagram = _context.Diagrams.Find(diagramId);

            if (diagram == null)
            {
                return NotFound();
            }

            return diagram.Classifiers.ToList();
        }

        [HttpGet("{id}", Name = "GetClassifier")]
        public ActionResult<Models.Classifier> GetById(long diagramId, long id)
        {
            var diagram = _context.Diagrams.Find(diagramId);
            
            if (diagram == null)
            {
                return NotFound();
            }
            
            var classifier = diagram.Classifiers.FirstOrDefault(_classifier => _classifier.ID == id);
            
            if (classifier == null)
            {
                return NotFound();
            }
            
            return classifier;
        }

        [HttpPost]
        public IActionResult Create(long diagramId, Models.Classifier classifier)
        {
            var diagram = _context.Diagrams.Find(diagramId);
            
            if (diagram == null)
            {
                return NotFound();
            }
            
            classifier.Relationships = new List<Models.Relationship>();
            classifier.Diagram = diagram;

            _context.Classifiers.Add(classifier);
            _context.SaveChanges();

            return CreatedAtRoute("GetClassifier", new { diagramId = diagram.ID, id = classifier.ID }, classifier);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long diagramId, long id, Models.Classifier classifier)
        {
            var diagram = _context.Diagrams.Find(diagramId);
            
            if (diagram == null)
            {
                return NotFound();
            }
            
            var currentClassifier = diagram.Classifiers.FirstOrDefault(_classifier => _classifier.ID == id);
            
            if (currentClassifier == null)
            {
                return NotFound();
            }

            currentClassifier.Name = classifier.Name;

            _context.Classifiers.Update(currentClassifier);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long diagramId, long id)
        {
            var diagram = _context.Diagrams.Find(diagramId);
            
            if (diagram == null)
            {
                return NotFound();
            }
            
            var classifier = diagram.Classifiers.FirstOrDefault(_classifier => _classifier.ID == id);
            
            if (classifier == null)
            {
                return NotFound();
            }

            _context.Classifiers.Remove(classifier);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}
