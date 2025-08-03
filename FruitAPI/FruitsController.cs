using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class FruitsController : ControllerBase
{
    private readonly FruitDb _context;

    public FruitsController(FruitDb context)
    {
        _context = context;
    }

    // GET: api/fruits
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fruits>>> GetFruits()
    {
        return await _context.Fruits.ToListAsync();
    }

    // GET: api/fruits/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Fruits>> GetFruit(int id)
    {
        var fruit = await _context.Fruits.FindAsync(id);

        if (fruit == null)
        {
            return NotFound();
        }

        return fruit;
    }

    // POST: api/fruits
    [HttpPost]
    public async Task<ActionResult<Fruits>> PostFruit(Fruits fruit)
    {
        _context.Fruits.Add(fruit);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFruit), new { id = fruit.Id }, fruit);
    }

    // PUT: api/fruits/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFruit(int id, Fruits fruit)
    {
        if (id != fruit.Id)
        {
            return BadRequest();
        }

        _context.Entry(fruit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!FruitExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/fruits/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFruit(int id)
    {
        var fruit = await _context.Fruits.FindAsync(id);
        if (fruit == null)
        {
            return NotFound();
        }

        _context.Fruits.Remove(fruit);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FruitExists(int id)
    {
        return _context.Fruits.Any(e => e.Id == id);
    }
}
