public class CategoriaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // Método para convertir a CategoriaRequest
    public CategoriaRequest ToRequest()
    {
        return new CategoriaRequest
        {
            Id = this.Id,
            Nombre = this.Nombre
        };
    }
}

public class CategoriaRequest
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
}