using Microsoft.EntityFrameworkCore;
using UT2024P4LP4.Web.Data;
using UT2024P4LP4.Web.Data.Dtos;
using UT2024P4LP4.Web.Data.Entities;

namespace UT2024P4LP4.Web.Services;

public interface ICategoriaService
{
    Task<Result> Create(CategoriaRequest categoria);
    Task<Result> Delete(int Id);
    Task<ResultList<CategoriaDto>> GetAll(CancellationToken cancellationToken = default);
    Task<Result> Update(CategoriaRequest categoria);
}


public class CategoriaService(IApplicationDbContext context) : ICategoriaService
{
    public async Task<Result> Delete(int Id)
    {

        try
        {
            var entity = await context.Categorias.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return Result.Failure("Categoria no encontrada");
            }
            else
            {
                context.Categorias.Remove(entity);
                await context.SaveChangesAsync();
                return Result.Success("Categoria Eliminada");
            }
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error en eliminar {ex}");
        }

    }

    public async Task<ResultList<CategoriaDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var categorias = await context.Categorias
        .Select(x => x.ToDto())
        .ToListAsync(cancellationToken);
        return ResultList<CategoriaDto>.Success(categorias);


    }

    public async Task<Result> Create(CategoriaRequest categoria)
    {
        try
        {
            var r = new Categoria() { Nombre = categoria.Nombre ?? ""};
            context.Categorias.Add(r);
            await context.SaveChangesAsync();
            return Result.Success("Categoria Registrada");

        }
        catch (Exception ex)
        {
            return Result.Failure($"Error en crear la categoria {ex}");
        }
    }

    public async Task<Result> Update(CategoriaRequest categoria)
    {
        try
        {
            var entity = await context.Categorias.Where(x => x.Id == categoria.Id).FirstOrDefaultAsync();
            if (entity == null)
            {
                return Result.Failure("Categoria no encontrada");
            }
            else
            {
                entity.Nombre = categoria.Nombre ?? "";
                await context.SaveChangesAsync();
                return Result.Success("Categoria Actualizada");
            }
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error en actualizar la categoria {ex}");
        }
    }
}

