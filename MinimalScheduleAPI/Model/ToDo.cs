namespace MinimalScheduleAPI.Model
{
    public class ToDo
    {
        public Guid IdToDo { get; set; }
        public string STitulo { get; set; }
        public string? SDescricao { get; set; }
        public bool BConcluido { get; set; }
        public Guid IdCard { get; set; }

        public void Update(string titulo, bool concluido, string descricacao)
        {
            STitulo = titulo;
            BConcluido = concluido;
            SDescricao = descricacao;
        }
    }
}
