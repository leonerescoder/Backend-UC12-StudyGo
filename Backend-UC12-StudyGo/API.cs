

using System.Net;
using System.Text;
using System.Text.Json;

public class API
{
    HttpListener listener;
    
    public API()
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:5001/");
    }

    public async Task Iniciar()
    {

        listener.Start();
        while (listener.IsListening) {

            var contexto = await listener.GetContextAsync();
             await ProcessarRequisicao(contexto);
            
        
        
        
        
        
        }

    }


    public  async Task  ProcessarRequisicao(HttpListenerContext contexto){

        contexto.Response.Headers.Add("Access-Control-Allow-Origin","*");

        if (contexto.Request.HttpMethod == "GET" )
        {

            if (contexto.Request.Url.AbsolutePath == "/usuarios") { 
            
            
                
                List<User> lista =  await User.ListarTodos();

                string json = JsonSerializer.Serialize(lista);

                byte[] bytes = Encoding.UTF8.GetBytes(json);

                contexto.Response.ContentType = "application/json";
                contexto.Response.ContentLength64 = bytes.Length;

                await contexto.Response.OutputStream.WriteAsync(bytes);

                contexto.Response.Close();

                return;
            
            
            
            } ;


        }
        contexto.Response.StatusCode = 404;
        contexto.Response.Close ();

    
    
    
    }

}