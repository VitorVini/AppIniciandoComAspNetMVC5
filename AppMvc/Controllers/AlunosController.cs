using AppMvc.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AppMvc.Controllers
{
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("listar-alunos")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Alunos.ToListAsync());
        }

        [HttpGet]
        [Route("alunos-detalhe/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            var aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        [HttpGet]
        [Route("novo-aluno")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("novo-aluno")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email, Descricao, CPF,Ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.DataMatricula = DateTime.Now;
                db.Alunos.Add(aluno);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        [HttpGet]
        [Route("editar-aluno/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar-aluno/{id:int}")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Email, Descricao, CPF,Ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                db.Entry(aluno).Property(a => a.DataMatricula).IsModified = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aluno);
        }

        [HttpGet]
        [Route("excluir-aluno/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            Aluno aluno = await db.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [Route("excluir-aluno/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Aluno aluno = await db.Alunos.FindAsync(id);
            db.Alunos.Remove(aluno);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
