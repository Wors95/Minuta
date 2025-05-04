public class Usuario{
    public int Id {get; set;}
    public string Nome {get; set;}
    public string Email {get; set;}
    public string SenhaHash {get; set;}
    public string Role {get; set;} // <-- Isso é a regra do user, se é admin ou usuário padrão
}