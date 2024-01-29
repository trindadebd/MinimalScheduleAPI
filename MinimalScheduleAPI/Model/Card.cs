namespace MinimalScheduleAPI.Model
{
    public class Card
    {
        public void CardToDo()
        {
            ListToDo = new List<ToDo>();
        }
        public Guid IdCard { get; set; }
        public string SNome { get; set; }
        public bool BDiaTodo { get; set; }
        public string? SLocal { get; set; }
        public string? SDescricao { get; set; }
        public List<ToDo>? ListToDo { get; set; }

        public void Update(string nome, bool diaTodo, string local, string descricacao)
        {
            SNome = nome;
            BDiaTodo = diaTodo;
            SLocal = local;
            SDescricao = descricacao;
        }


    }
}
